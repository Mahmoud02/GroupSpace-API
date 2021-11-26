using GroupSpace.DAL.DataContext;
using GroupSpace.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    public class PostRepository : GenericRepository<Post>
    {
        public PostRepository(AppDataContext context) : base(context)
        {
        }
        public override Post Update(Post entity)
        {
            var post = context.Posts
                .Single(c => c.PostId == entity.PostId);

            post.Date = entity.Date;
            post.GroupId = entity.GroupId;
            post.NumOfLikes = entity.NumOfLikes;
            post.PhotoUrl = entity.PhotoUrl;
            post.PostId = entity.PostId;
            post.Text = entity.Text;
            post.UserId = entity.UserId;
            return base.Update(post);
        }
        public override IEnumerable<Post> Find(Expression<Func<Post, bool>> predicate)
        {
            return context.Posts
                .Include(p => p.User)
                .Include(p =>p.Comments)
                .ThenInclude(c => c.User)
                .AsQueryable()
                .Where(predicate).ToList();
        }
    }
}
