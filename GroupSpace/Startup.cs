using Abp.AspNetCore.SignalR.Hubs;
using GroupSpace.BLL;
using GroupSpace.DAL.DataContext;
using GroupSpaceApi.Authorization;
using GroupSpaceApi.Hubs;
using GroupSpaceWeb.Helpers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers();
            services.AddServices();
            
            //configure strongly typed JwtSettings objects

            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);
            //To clear Default Mapping Of Claims that returned by IDP
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //Configure Ahtentication Middlware
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(configureOptions =>
                {
                    configureOptions.Authority = "https://localhost:5001/";
                    configureOptions.ApiName = "groupSpaceApi";
                    configureOptions.RequireHttpsMetadata = true;
                })
                .AddJwtBearer("IdpServerSchema",options =>
                {

                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtSettings:Audience"],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SigningKey"]))
                    };
                });
            //Configure Authorization Policy
            services.AddAuthorization(authorizationOptions =>
               authorizationOptions.AddPolicy(
                   "MustOwnGroup",
                   authorizationPolicyBuilder =>
                   {
                       authorizationPolicyBuilder.AuthenticationSchemes.Add(IdentityServerAuthenticationDefaults.AuthenticationScheme);
                       authorizationPolicyBuilder.RequireAuthenticatedUser();
                       authorizationPolicyBuilder.AddRequirements(new MustOwnGroupRequirement());
                          
                   }
               )
           );
            services.AddAuthorization(authorizationOptions =>
                authorizationOptions.AddPolicy(
                    "MarketingTeam",
                    authorizationPolicyBuilder =>
                    {
                        authorizationPolicyBuilder.RequireAuthenticatedUser();
                        authorizationPolicyBuilder.RequireClaim("secondRole", "marketing");
                    }
                )
            );

            //Configure Authorization Handler
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthorizationHandler, MustOwnGroupHandler>();
            services.AddSignalR(configure => configure.EnableDetailedErrors = true);

            //
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost")
                          .AllowCredentials()
                          .AllowAnyHeader()
                          .SetIsOriginAllowed(_ => true)
                          .AllowAnyMethod();
                      });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }


            app.UseHttpsRedirection();
            // Shows UseCors with named policy.
            app.UseCors("AllowAllHeaders");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GroupChat>("/groupchat");
                //endpoints.MapHub<AbpCommonHub>("/signalr"); // Restore this
                //endpoints.MapHub<GroupChat>("/GroupChat");

            });
        }
    }
}
