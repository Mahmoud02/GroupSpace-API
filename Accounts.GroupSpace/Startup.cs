using Accounts.GroupSpace.Configuration;
using Accounts.GroupSpace.DbContexts;
using Accounts.GroupSpace.Services;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.GroupSpace
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<IdentityDbContext>(options =>
            {
                var connetionString = Configuration.GetConnectionString("DefaultConnection");
                options             
                .UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
            });
            services.AddScoped<IPasswordHasher<Entities.User>, PasswordHasher<Entities.User>>();
            services.AddScoped<ILocalUserService, LocalUserService>();

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiResources(InMemoryConfiguration.ApiResources)
                    .AddInMemoryApiScopes(InMemoryConfiguration.GetApiScopes())
                    .AddInMemoryClients(InMemoryConfiguration.Clients)
                    .AddInMemoryIdentityResources(InMemoryConfiguration.Ids)
                    .AddProfileService<LocalUserProfileService>();

            //Add HttpClient For Accessing  Api Server
            services.AddHttpClient("GroupSpaceApiClient", configureClient => {
                configureClient.BaseAddress = new Uri("https://localhost:44306/");
                configureClient.DefaultRequestHeaders.Clear();
                configureClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });
            services.AddMvc();
                   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            //app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
