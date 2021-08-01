using AutoMapper;
using GroupSpace.BLL.Models;
using GroupSpace.BLL.Models.Group;
using GroupSpace.BLL.Shared;
using GroupSpace.DAL.Entities;
using GroupSpace.DAL.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL
{
    public interface IGroupService
    {
        IEnumerable<GroupDto> All();
        Task<Response> Add(GroupInsertDto entity);
        GroupDto Get(int id);
        Response Update(GroupInsertDto entity);
        bool CheckIfGroupExist(int id);
        Response Delete(int userId);
        //Related Data
        GroupMetaData GetGroupMetaData(int groupId);
        IEnumerable<GroupDto> GetUserGroups(int userId);
        IEnumerable<GroupMemberDto> GetGroupUsers(int groupId);
        IEnumerable<JoinRequestDto> GetJoinRequests(int groupId);
        IEnumerable<ReportPostDto> GetReportedPosts(int groupId);

        //Find Groups For The User
        IEnumerable<GroupDto> FindGroups(int userId);

    }
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper , IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
            _config = config;
        }

        public async Task<Response> Add(GroupInsertDto entity)
        {
           
            var AccessKey = _config.GetSection("S3Settings").GetSection("AccessKey").Value;
            var SecretKey = _config.GetSection("S3Settings").GetSection("SecretKey").Value;
            var BuketName = _config.GetSection("S3Settings").GetSection("BucketName").Value;

            string coverUrl;

            using (var memoryStream = new MemoryStream())
            {
                await entity.CoverPhoto.CopyToAsync(memoryStream);

                // Upload the file if less than 4 MB
                if (memoryStream.Length < 4097152)
                {
                    coverUrl = await S3Helper.UploadFileAsync(entity.CoverPhoto.FileName, memoryStream, BuketName, AccessKey,SecretKey);
                }
                else
                {
                    throw new AppException("File \"" + "The file is too large.");
                }
            }
            var group = _mapper.Map<Group>(entity);
            group.CoverPhotoUrl = coverUrl;
            unitOfWork.GroupRepository.Add(group);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
       
        public IEnumerable<GroupDto> All()
        {
            var entities = unitOfWork.GroupRepository.All();
            var entitiesDTO = _mapper.Map<List<GroupDto>>(entities);
            return entitiesDTO;
        }

        public bool CheckIfGroupExist(int id)
        {
            return unitOfWork.GroupRepository.CheckIfEntityExist(id);
        }

        public Response Delete(int groupId)
        {
            var group = unitOfWork.GroupRepository.Get(groupId);
            unitOfWork.GroupRepository.Delete(group);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }

        public GroupDto Get(int id)
        {
            var group = unitOfWork.GroupRepository.Get(id);
            var groupDto = _mapper.Map<GroupDto>(group);
            return groupDto;
        }

        public Response Update(GroupInsertDto entity)
        {
            var group = _mapper.Map<Group>(entity);
            unitOfWork.GroupRepository.Update(group);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
        public IEnumerable<GroupDto> GetUserGroups(int userId)
        {
            var groups = unitOfWork.GroupRepository.Find(g => g.UserId == userId);
            return _mapper.Map<List<GroupDto>>(groups);
        }
        public IEnumerable<GroupMemberDto> GetGroupUsers(int groupId)
        {
            var groupMembers = unitOfWork.GroupMemberRepository.Find(g => g.GroupId == groupId);

            return _mapper.Map<List<GroupMemberDto>>(groupMembers);
        }

        public GroupMetaData GetGroupMetaData(int groupId)
        {
            var numberOfUsers = unitOfWork.GroupMemberRepository.Find(g => g.GroupId == groupId).Count();
            var numberOfJoinRequests = unitOfWork.JoinRequestRepository.Find(g => g.GroupId == groupId).Count();
            var numberOfRportedPosts = unitOfWork.ReportPostRepository.Find(g => g.GroupId == groupId).Count();

            return new GroupMetaData
            {
                NumOfUsers = numberOfUsers,
                NumOfJoinRequests = numberOfJoinRequests,
                NumOfReports = numberOfRportedPosts
            }; 

        }

        public IEnumerable<JoinRequestDto> GetJoinRequests(int groupId)
        {
            var joinRequests = unitOfWork.JoinRequestRepository.Find(g => g.GroupId == groupId);

            var entites = _mapper.Map<List<JoinRequestDto>>(joinRequests);
            return entites;
        }

        public IEnumerable<ReportPostDto> GetReportedPosts(int groupId)
        {
            var reportedPosts = unitOfWork.ReportPostRepository.Find(g => g.GroupId == groupId);

            return _mapper.Map<List<ReportPostDto>>(reportedPosts);
        }

        public IEnumerable<GroupDto> FindGroups(int userId)
        {
            var groups = unitOfWork.GroupRepository.Find(g => g.UserId != userId);
            return _mapper.Map<List<GroupDto>>(groups);
        }
    }
}
