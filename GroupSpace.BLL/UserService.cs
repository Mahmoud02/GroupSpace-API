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
    public interface IUserService {
        IEnumerable<UserDto> All();
        Response Add(UserInsertDto entity);
        UserDto Get(int id);
        Response Update(UserInsertDto entity);
        bool CheckIfUserExist(int id);
        Response Delete(int userId);
        UserDto Authenticate(string username, string password);


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
        public Response Add(UserInsertDto userInsert)
        {          
            if (unitOfWork.UserRepository.Find(x => x.Email == userInsert.Email).Any())
                throw new AppException("Email \"" + userInsert.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userInsert.Password, out passwordHash, out passwordSalt);                  
            var user = _mapper.Map<User>(userInsert);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            unitOfWork.UserRepository.Add(user);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
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

        public Response Update(UserInsertDto entity)
        {
            var user = _mapper.Map<User>(entity);
            unitOfWork.UserRepository.Update(user);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
        public Response Delete(int userId)
        {

            var user = unitOfWork.UserRepository.Get(userId);
            unitOfWork.UserRepository.Delete(user);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
        public bool CheckIfUserExist(int id) {
            return unitOfWork.UserRepository.CheckIfEntityExist(id);
        }

        public UserDto Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = unitOfWork.UserRepository.Find(x => x.Email == email).FirstOrDefault();

            // check if username exists
            if (user == null)
                return null;
            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash,user.PasswordSalt))
                return null;
            var userDto = _mapper.Map<UserDto>(user);

            // authentication successful
            return userDto;
        }
        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
