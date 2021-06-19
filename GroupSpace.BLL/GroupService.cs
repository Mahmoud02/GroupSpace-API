using AutoMapper;
using GroupSpace.BLL.Models;
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
    public interface IGroupService
    {
        IEnumerable<GroupDto> All();
        Response Add(GroupInsertDto entity);
        GroupDto Get(int id);
        Response Update(GroupInsertDto entity);
        bool CheckIfGroupExist(int id);
        Response Delete(int userId);
        IEnumerable<GroupDto> GetUserGroups(int userId);


    }
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public Response Add(GroupInsertDto entity)
        {
            var group = _mapper.Map<Group>(entity);
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

        public IEnumerable<GroupDto> GetUserGroups(int userId)
        {
            var groups = unitOfWork.GroupRepository.Find(g => g.UserId == userId);
            return _mapper.Map<List<GroupDto>>(groups);
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
    }
}
