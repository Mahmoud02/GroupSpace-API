using AutoMapper;
using GroupSpace.BLL.Models;
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
    public interface IPostService
    {
        IEnumerable<PostDto> All();
        Task<Response> Add(PostInsertDto entity);
        PostDto Get(int id);
        Response Update(PostInsertDto entity);
        bool CheckIfPostExist(int id);
        Response Delete(int postId);
        IEnumerable<PostDto> GroupPosts(int groupId);

    }
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;


        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
            _config = config;

        }
        public async Task<Response> Add(PostInsertDto entity)
        {
            string photoUrl = null;
            //check if user add photo
            if(entity.Photo != null)
            {
                var AccessKey = _config.GetSection("S3Settings").GetSection("AccessKey").Value;
                var SecretKey = _config.GetSection("S3Settings").GetSection("SecretKey").Value;
                var BuketName = _config.GetSection("S3Settings").GetSection("BucketName").Value;
                using (var memoryStream = new MemoryStream())
                {
                    await entity.Photo.CopyToAsync(memoryStream);

                    // Upload the file if less than 4 MB
                    if (memoryStream.Length < 4097152)
                    {
                        photoUrl = await S3Helper.UploadFileAsync(entity.Photo.FileName, memoryStream, BuketName, AccessKey, SecretKey);
                    }
                    else
                    {
                        throw new AppException("File \"" + "The file is too large.");
                    }
                }
            }
           
            var post = _mapper.Map<Post>(entity);
            post.PhotoUrl = photoUrl;
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
        public IEnumerable<PostDto> GroupPosts(int groupId)
        {
            var posts = unitOfWork.PostRepository.Find(g => g.GroupId == groupId);
            var entitiesDTO = _mapper.Map<List<PostDto>>(posts);
            return entitiesDTO;
        }
    }
}
