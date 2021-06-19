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
