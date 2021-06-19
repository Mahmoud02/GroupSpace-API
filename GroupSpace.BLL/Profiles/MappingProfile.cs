using AutoMapper;
using GroupSpace.BLL.Models;
using GroupSpace.BLL.Shared;
using GroupSpace.DAL.Entities;
using GroupSpace.DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Profiles
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserInsertDto>().ReverseMap();
            CreateMap<CommonResult, Response>().ReverseMap();
            //Group
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Group, GroupInsertDto>().ReverseMap();
            //Post
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostInsertDto>().ReverseMap();

        }
    }
}
