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
    class ReportPostRepository : GenericRepository<ReportPost>
    {
        public ReportPostRepository(AppDataContext context) : base(context)
        {

        }
        public override IEnumerable<ReportPost> Find(Expression<Func<ReportPost, bool>> predicate)
        {
            return context.ReportPosts
                .Include(p => p.User)
                .Include(p =>p.Post)
                .AsQueryable()
                .Where(predicate).ToList();
        }

    }
}
