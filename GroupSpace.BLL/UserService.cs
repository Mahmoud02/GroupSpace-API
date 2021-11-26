using AutoMapper;
using GroupSpace.BLL.Models;
using GroupSpace.BLL.Models.User;
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
    public interface IUserService {
        IEnumerable<UserDto> All();
        UserDto Get(int id);
        UserDto Find(string sub);
        bool CheckIfUserExist(int id);
        Response Add(UserInsertDto entity);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;


        }    
        public IEnumerable<UserDto> All()
        {
            var entities = unitOfWork.UserRepository.All();
            var entitiesDTO = _mapper.Map<List<UserDto>>(entities);
            return entitiesDTO;
        }
        public UserDto Get(int id)
        {
            var user = unitOfWork.UserRepository.Get(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public Response Add(UserInsertDto userInsert)
        {          
            var user = _mapper.Map<User>(userInsert);
            unitOfWork.UserRepository.Add(user);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
        public bool CheckIfUserExist(int id) {
            return unitOfWork.UserRepository.CheckIfEntityExist(id);
        }

        public UserDto Find(string sub)
        {
            var user = unitOfWork.UserRepository.Find(u => u.SubID == sub).FirstOrDefault();
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
