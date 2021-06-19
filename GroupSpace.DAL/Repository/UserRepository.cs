using GroupSpace.DAL.DataContext;
using GroupSpace.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDataContext context) : base(context)
        {
        }
        public override User Update(User entity)
        {
            var user = context.Users
                .Single(c => c.UserId == entity.UserId);

            user.Email = entity.Email;
            user.Bio = entity.Bio;
            /*user.Password = entity.Password;*/
            user.PersonalImageUrl = entity.PersonalImageUrl;
            user.OnlineStatus = entity.OnlineStatus;
            return base.Update(user);
        }
       
    }
}
