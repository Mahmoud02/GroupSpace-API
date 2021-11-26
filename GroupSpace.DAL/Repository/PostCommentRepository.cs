using GroupSpace.DAL.DataContext;
using GroupSpace.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    class PostCommentRepository : GenericRepository<PostComment>
    {
        public PostCommentRepository(AppDataContext context) : base(context)
        {

        }
    }
}
