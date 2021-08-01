using AutoMapper;
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
    public interface IJoinRequestService
    {
        IEnumerable<JoinRequestDto> All();
        Response Add(JoinRequestInsertDto entity);
        JoinRequestDto Get(int id);
        bool CheckIfRequestExist(int id);
        Response Delete(int joinRequestId);
        int GetJoinRequestID(int userId, int groupId);
        List<int> getGroupsIdOfJoinRequestByUserId(int userId);

    }
    class JoinRequestService : IJoinRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public JoinRequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;

        }
        public  Response Add(JoinRequestInsertDto entity)
        {
            var joinRequest = _mapper.Map<JoinRequest>(entity);
            unitOfWork.JoinRequestRepository.Add(joinRequest);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);

            return response;
        }

        public IEnumerable<JoinRequestDto> All()
        {
            var entities = unitOfWork.JoinRequestRepository.All();
            var entitiesDTO = _mapper.Map<List<JoinRequestDto>>(entities);
            return entitiesDTO;
        }

        public bool CheckIfRequestExist(int joinRequestId)
        {
            return unitOfWork.JoinRequestRepository.CheckIfEntityExist(joinRequestId);
        }

        public Response Delete(int joinRequestId)
        {
            var joinRequest = unitOfWork.JoinRequestRepository.Get(joinRequestId);
            unitOfWork.JoinRequestRepository.Delete(joinRequest);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }

        public JoinRequestDto Get(int joinRequestId)
        {
            var joinRequest = unitOfWork.JoinRequestRepository.Get(joinRequestId);
            var joinRequestDto = _mapper.Map<JoinRequestDto>(joinRequest);
            return joinRequestDto;
        }
        public int GetJoinRequestID(int userId, int groupId)
        {
            var joinRequest = unitOfWork.JoinRequestRepository.Get(e => (e.UserId == userId && e.GroupId == groupId) );
            return joinRequest.JoinRequestId;
        }
        public List<int> getGroupsIdOfJoinRequestByUserId(int userId)
        {
            var joinRequests = unitOfWork.JoinRequestRepository.Find(e => e.UserId == userId);
            var groupsIdList = joinRequests
              .Select(x => x.GroupId)
              .ToList();
            
            return groupsIdList;

        }

    }
}
