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

        public static OptionsBuild optionsBuild = new OptionsBuild();

        public AppDataContext(DbContextOptions<AppDataContext> options)
           : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

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
