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
    public class JoinRequestRepository :GenericRepository<JoinRequest>
    {
        public JoinRequestRepository(AppDataContext context) : base(context)
        {

        }
        public override IEnumerable<JoinRequest> Find(Expression<Func<JoinRequest, bool>> predicate)
        {
            return context.JoinRequests.Include(p => p.User)
                .AsQueryable()
                .Where(predicate).ToList();
        }
    }
}
