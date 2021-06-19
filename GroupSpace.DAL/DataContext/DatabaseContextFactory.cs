using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.DataContext
{
    class DatabaseContextFactory : IDesignTimeDbContextFactory<AppDataContext>
    {
        public AppDataContext CreateDbContext(string[] args)
        {
            AppSetting appConfig = new();
            DbContextOptionsBuilder<AppDataContext> optionsBuilder = new();

            optionsBuilder.UseMySql(appConfig.SQLConnectionString, ServerVersion.AutoDetect(appConfig.SQLConnectionString));

            return new AppDataContext(optionsBuilder.Options);
        }
    }
}
