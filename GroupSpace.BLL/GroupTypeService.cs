using AutoMapper;
using GroupSpace.BLL.Models.Group;
using GroupSpace.BLL.Shared;
using GroupSpace.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL
{
    public interface IGroupTypeService
    {
        IEnumerable<GroupTypeDto> All();
        Task<Response> Add(GroupTypeDto entity);
        GroupTypeDto Get(int id);
        Response Update(GroupTypeDto entity);
        bool CheckIfPostExist(int id);
        Response Delete(int postId);

    }
    class GroupTypeService :IGroupTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public GroupTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public Task<Response> Add(GroupTypeDto entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupTypeDto> All()
        {
            var entities = unitOfWork.GroupTypeRepository.All();
            var entitiesDTO = _mapper.Map<List<GroupTypeDto>>(entities);
            return entitiesDTO;
        }

        public bool CheckIfPostExist(int id)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int postId)
        {
            throw new NotImplementedException();
        }

        public GroupTypeDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public Response Update(GroupTypeDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
