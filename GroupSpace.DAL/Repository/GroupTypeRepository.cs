using GroupSpace.DAL.DataContext;
using GroupSpace.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    public class GroupTypeRepository : GenericRepository<GroupType>
    {
        public GroupTypeRepository(AppDataContext context) : base(context)
        {

        }
    }
}
