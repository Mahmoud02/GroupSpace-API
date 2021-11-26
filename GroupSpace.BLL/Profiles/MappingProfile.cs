using AutoMapper;
using GroupSpace.BLL.Models;
using GroupSpace.BLL.Models.Group;
using GroupSpace.BLL.Models.PostComment;
using GroupSpace.BLL.Models.User;
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
            
            //GroupType
            CreateMap<GroupType, GroupTypeDto>().ReverseMap();

            //JoinRequest
            CreateMap<JoinRequest, JoinRequestDto>().ReverseMap();
            CreateMap<JoinRequest, JoinRequestInsertDto>().ReverseMap();

            //GroupMember
            CreateMap<GroupMember, GroupMemberDto>().ReverseMap();
            CreateMap<GroupMember, GroupMemberInsertDto>().ReverseMap();

            //PostComments
            CreateMap<PostComment, PostCommentDto>().ReverseMap();
            //Post Reports

            CreateMap<ReportPost, ReportPostDto>().ReverseMap();
            CreateMap<ReportPost, ReportPostInsertDto>().ReverseMap();



        }
    }
}
