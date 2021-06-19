using GroupSpace.DAL.DataContext;
using GroupSpace.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    public class GroupRepository : GenericRepository<Group>
    {
        public GroupRepository(AppDataContext context) : base(context)
        {

        }
        public override Group Update(Group entity)
        {
            var group = context.Groups
                .Single(c => c.GroupId == entity.GroupId);
            group.CoverPhotoUrl = entity.CoverPhotoUrl;
            group.Description = entity.Description;
            group.GroupTypeId = entity.GroupTypeId;
            group.Name = entity.Name;
            group.Private = entity.Private;
            group.UserId = entity.UserId;
            return base.Update(group);
        }
    }
}
