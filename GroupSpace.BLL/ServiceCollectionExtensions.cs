using GroupSpace.DAL.DataContext;
using GroupSpace.DAL.Entities;
using GroupSpace.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //add
            services.AddTransient<AppDataContext>(x => new AppDataContext(AppDataContext.optionsBuild.dbContextOptions)); 
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Group>, GroupRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IGroupTypeService, GroupTypeService>();
            services.AddTransient<IJoinRequestService, JoinRequestService>();
            services.AddTransient<IGroupMemberService, GroupMemberService>();
            services.AddTransient<IPostCommentService, PostCommentService>();
            services.AddTransient<IReportPostService, ReportPostService>();

            return services.AddTransient<IUserService, UserService>();



        }
    }
}
