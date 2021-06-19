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
    public interface IPostService
    {
        IEnumerable<PostDto> All();
        Response Add(PostInsertDto entity);
        PostDto Get(int id);
        Response Update(PostInsertDto entity);
        bool CheckIfPostExist(int id);
        Response Delete(int postId);
        IEnumerable<PostDto> GeGroupPosts(int groupId);

    }
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;

        }
        public Response Add(PostInsertDto entity)
        {
            var post = _mapper.Map<Post>(entity);
            unitOfWork.PostRepository.Add(post);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
        public IEnumerable<PostDto> All()
        {
            var entities = unitOfWork.PostRepository.All();
            var entitiesDTO = _mapper.Map<List<PostDto>>(entities);
            return entitiesDTO;
        }
        public bool CheckIfPostExist(int id)
        {
            throw new NotImplementedException();
        }
        public Response Delete(int postId)
        {
            var post = unitOfWork.PostRepository.Get(postId);
            unitOfWork.PostRepository.Delete(post);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
        public PostDto Get(int id)
        {
            var post = unitOfWork.PostRepository.Get(id);
            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }
        public Response Update(PostInsertDto entity)
        {
            var post = _mapper.Map<Post>(entity);
            unitOfWork.PostRepository.Update(post);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }
        //Group Services related to the Post
        public IEnumerable<PostDto> GeGroupPosts(int groupId)
        {
            var posts = unitOfWork.PostRepository.Find(g => g.GroupId == groupId);
            var entitiesDTO = _mapper.Map<List<PostDto>>(posts);
            return entitiesDTO;
        }
    }
}
