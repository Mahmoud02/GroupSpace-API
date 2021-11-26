using GroupSpace.DAL.DataContext;
using GroupSpace.DAL.Entities;
using GroupSpace.DAL.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    public interface IUnitOfWork
    {
        IRepository<User>UserRepository { get; }
        IRepository<Group>GroupRepository { get; }
        IRepository<Post> PostRepository { get; }
        IRepository<JoinRequest> JoinRequestRepository { get; }
        IRepository<GroupMember> GroupMemberRepository { get; }
        IRepository<ReportPost> ReportPostRepository { get; }
        IRepository<GroupType> GroupTypeRepository { get; }
        IRepository<PostComment> PostCommentRepository { get; }


        CommonResult SaveChanges();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private AppDataContext context;

        public UnitOfWork(AppDataContext context)
        {
            this.context = context;
        }
        
        private IRepository<User> userRepository;
        public IRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }

                return userRepository;
            }
        }
        
        private IRepository<Group> groupRepository;
        public IRepository<Group> GroupRepository
        {
            get
            {
                if (groupRepository == null)
                {
                    groupRepository = new GroupRepository(context);
                }

                return groupRepository;
            }
        }
        
        private IRepository<Post> postRepository;
        public IRepository<Post> PostRepository
        {
            get
            {
                if (postRepository == null)
                {
                    postRepository = new PostRepository(context);
                }

                return postRepository;
            }
        }
        
        private IRepository<JoinRequest> joinRequestRepository;
        public IRepository<JoinRequest> JoinRequestRepository
        {
            get
            {
                if (joinRequestRepository == null)
                {
                    joinRequestRepository = new JoinRequestRepository(context);
                }

                return joinRequestRepository;
            }
        }
        
        private IRepository<GroupMember> groupMemberRepository;
        public IRepository<GroupMember> GroupMemberRepository
        {
            get
            {
                if (groupMemberRepository == null)
                {
                    groupMemberRepository = new GroupMemberRepository(context);
                }

                return groupMemberRepository;
            }
        }
        
        private IRepository<ReportPost> reportPostRepository;
        public IRepository<ReportPost> ReportPostRepository {
            get
            {
                if (reportPostRepository == null)
                {
                    reportPostRepository = new ReportPostRepository(context);
                }

                return reportPostRepository;
            }
        }
        private IRepository<GroupType> groupTypeRepository;
        public IRepository<GroupType> GroupTypeRepository
        {
            get
            {
                if (groupTypeRepository == null)
                {
                    groupTypeRepository = new GroupTypeRepository(context);
                }

                return groupTypeRepository;
            }
        }
        private IRepository<PostComment> postCommentRepository;

        public IRepository<PostComment> PostCommentRepository
        {
            get
            {
                if (postCommentRepository == null)
                {
                    postCommentRepository = new PostCommentRepository(context);
                }

                return postCommentRepository;
            }
        }
        public CommonResult SaveChanges()
        {
            CommonResult result = new();
            try
            {
                context.SaveChanges();
                result.OpertaionState = true;
                return result;
            }
            catch (DbUpdateException e)
            {
                result.OpertaionState = false;
                result.Message = e.InnerException.Message;
                return result;
            }
            catch (Exception e) {
                result.OpertaionState = false;
                result.Message = e.InnerException.Message;
                return result;
            }

        }
    }
}
