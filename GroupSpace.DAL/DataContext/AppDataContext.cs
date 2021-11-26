using GroupSpace.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.DataContext
{
    public class AppDataContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }

        public DbSet<GroupRoleType> GroupRoleTypes { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }

        public DbSet<ReportPost> ReportPosts { get; set; }
        public DbSet<PostComment> PostComment { get; set; }


        public static OptionsBuild optionsBuild = new OptionsBuild();

        public AppDataContext(DbContextOptions<AppDataContext> options)
           : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>()
              .HasIndex(u => u.SubID)
              .IsUnique();
            //Feed Type With data
            SeedData(builder);

        }
        private static void SeedData(ModelBuilder builder) {
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 1, Text = "Humour" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 2, Text = "Science" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 3, Text = "Travel" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 4, Text = "Buy & sell" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 5, Text = "Business" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 6, Text = "Style" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 7, Text = "Animals" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 8, Text = "Sport" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 9, Text = "fitness" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 10, Text = "Education" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 11, Text = "Arts" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 12, Text = "Entertainment" });
            builder.Entity<GroupType>().HasData(new GroupType { GroupTypeId = 13, Text = "Food & drink" });

            //Role Type Of Group
            builder.Entity<GroupRoleType>().HasData(new GroupRoleType { GroupRoleTypeId = 1, Text = "User" });
            builder.Entity<GroupRoleType>().HasData(new GroupRoleType { GroupRoleTypeId = 2, Text = "Moderator"});
        }


        ////
        public class OptionsBuild
        {
            private AppSetting appConfig = new AppSetting();
            public DbContextOptionsBuilder<AppDataContext> optionsBuilder { get; set; }
            public DbContextOptions<AppDataContext> dbContextOptions { get; set; }

            public OptionsBuild()
            {
                appConfig = new AppSetting();
                optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
                optionsBuilder.UseMySql(appConfig.SQLConnectionString, ServerVersion.AutoDetect(appConfig.SQLConnectionString));
                dbContextOptions = optionsBuilder.Options;
            }

        }
    }
}
