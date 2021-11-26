using AutoMapper;
using GroupSpace.BLL.Models.PostComment;
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
    public interface IPostCommentService
    {
        IEnumerable<PostCommentDto> All();
        Response Add(string sub,PostCommentDto entity);
        PostCommentDto Get(int id);
        Response Update(PostCommentDto entity);
        bool CheckIfCommentExist(int id);
        Response Delete(int postId);
        IEnumerable<PostCommentDto> PostComments(int postId);

    }
    public class PostCommentService : IPostCommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;


        public PostCommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;

        }
        public Response Add(string sub, PostCommentDto entity)
        {
            var user = unitOfWork.UserRepository.Find(u => u.SubID == sub).FirstOrDefault();
            if (user == null)
                return new Response() { OpertaionState = false, Message = "Cant Added Group for that User" };
            entity.UserId = user.UserId;
            var comment = _mapper.Map<PostComment>(entity);
            unitOfWork.PostCommentRepository.Add(comment);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }

        public IEnumerable<PostCommentDto> All()
        {
            var entities = unitOfWork.PostCommentRepository.All();
            var entitiesDTO = _mapper.Map<List<PostCommentDto>>(entities);
            return entitiesDTO;
        }

        public bool CheckIfCommentExist(int id)
        {
            return unitOfWork.PostCommentRepository.CheckIfEntityExist(id);
        }

        public Response Delete(int postId)
        {
            var comment = unitOfWork.PostCommentRepository.Get(postId);
            unitOfWork.PostCommentRepository.Delete(comment);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }

        public PostCommentDto Get(int id)
        {
            var post = unitOfWork.PostCommentRepository.Get(id);
            var postDto = _mapper.Map<PostCommentDto>(post);
            return postDto;
        }

        public IEnumerable<PostCommentDto> PostComments(int postId)
        {
            var posts = unitOfWork.PostCommentRepository.Find(g => g.PostId == postId);
            var entitiesDTO = _mapper.Map<List<PostCommentDto>>(posts);
            return entitiesDTO;
        }

        public Response Update(PostCommentDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
