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
    
    public class GroupMemberRepository :GenericRepository<GroupMember>
    {
        public GroupMemberRepository(AppDataContext context) : base(context)
        {

        }
        public override IEnumerable<GroupMember> Find(Expression<Func<GroupMember, bool>> predicate)
        {
            return context.GroupMembers
                .Include(p => p.User)
                .Include(p => p.Group)
                .AsQueryable()
                .Where(predicate).ToList();
        }
        
    }
}
