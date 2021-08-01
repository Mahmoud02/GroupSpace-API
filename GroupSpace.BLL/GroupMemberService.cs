using AutoMapper;
using GroupSpace.BLL.Models;
using GroupSpace.BLL.Models.Group;
using GroupSpace.BLL.Shared;
using GroupSpace.DAL.Entities;
using GroupSpace.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL
{
    public interface IGroupMemberService
    {
        IEnumerable<GroupMemberDto> All();
        Response Add(GroupMemberInsertDto entity);
        GroupMemberDto Get(int id);
        Response Update(GroupMemberInsertDto entity);
        bool CheckIfGroupMemberExist(int id);
        Response Delete(int userId);
        
        IEnumerable<GroupDto> GetUserJoinedGroups(int userId);

    }
    public class GroupMemberService : IGroupMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public GroupMemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public Response Add(GroupMemberInsertDto entity)
        {
            var groupMember = _mapper.Map<GroupMember>(entity);
            unitOfWork.GroupMemberRepository.Add(groupMember);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }

        public IEnumerable<GroupMemberDto> All()
        {
            var entities = unitOfWork.GroupMemberRepository.All();
            var entitiesDTO = _mapper.Map<List<GroupMemberDto>>(entities);
            return entitiesDTO;
        }

        public bool CheckIfGroupMemberExist(int id)
        {
            return unitOfWork.GroupMemberRepository.CheckIfEntityExist(id);

        }

        public Response Delete(int id)
        {
            var groupMember = unitOfWork.GroupMemberRepository.Get(id);
            unitOfWork.GroupMemberRepository.Delete(groupMember);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }

        public GroupMemberDto Get(int id)
        {
            var groupMember = unitOfWork.GroupMemberRepository.Get(id);
            var groupMemberDto = _mapper.Map<GroupMemberDto>(groupMember);
            return groupMemberDto;
        }

        public Response Update(GroupMemberInsertDto entity)
        {
            var groupMember = _mapper.Map<Group>(entity);
            unitOfWork.GroupRepository.Update(groupMember);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
        public IEnumerable<GroupDto> GetUserJoinedGroups(int userId)
        {
            var groupMembers = unitOfWork.GroupMemberRepository.Find(g => g.UserId == userId);

            var groups = groupMembers.Select(a => a.Group).ToList();

            return _mapper.Map<List<GroupDto>>(groups);
        }
    }
}
