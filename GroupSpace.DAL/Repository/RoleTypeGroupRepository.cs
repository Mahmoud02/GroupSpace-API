using GroupSpace.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    public class RoleTypeGroupRepository : GenericRepository<RoleTypeGroupRepository>
    {
        public RoleTypeGroupRepository(AppDataContext context) : base(context)
        {

        }
    }
}
