using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPanel.GroupSpace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //To clear Default Mapping Of Claims that returned by IDP
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Add HttpClient For Accessing  Authorazition Server
            services.AddHttpClient("IDPClient", configureClient => {
                configureClient.BaseAddress = new Uri("https://localhost:5001/");
                configureClient.DefaultRequestHeaders.Clear();
                configureClient.DefaultRequestHeaders.Add(HeaderNames.Accept , "application/json");
            }) ;
            
            //Configure Authentication Service to Use Authoristion Server
            //make requests to it
            //also handle token validation
            services.AddAuthentication(configureOptions => {
                configureOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(
                OpenIdConnectDefaults.AuthenticationScheme , 
                configureOptions => {
                    configureOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    configureOptions.Authority = "https://localhost:5001/";
                    configureOptions.ClientId = "groupSpaceAdmin";
                    configureOptions.ResponseType = "code";
                    configureOptions.UsePkce = true;

                    //Are added by default
                    //configureOptions.Scope.Add("openid");
                    //configureOptions.Scope.Add("profile");

                    configureOptions.Scope.Add("offline_access");
                    configureOptions.Scope.Add("roles");
                    configureOptions.Scope.Add("GroupSpaceApiScope");
                    configureOptions.Scope.Add("email");                
                    configureOptions.SaveTokens = true;
                    configureOptions.ClientSecret = "groupSpaceAdminKey";
                    configureOptions.SignedOutRedirectUri = "https://www.google.com/?hl=ar";
                    configureOptions.GetClaimsFromUserInfoEndpoint = true;
                    
                    // To ensure that the Default Filter will not remove "nbf"
                    //From  Cookie as user Claims
                    configureOptions.ClaimActions.Remove("nbf");
                    
                    //To Delete "Jwt" value From claims In Token
                    configureOptions.ClaimActions.DeleteClaim("sid");
                    configureOptions.ClaimActions.DeleteClaim("idp");
                    configureOptions.ClaimActions.DeleteClaim("s_hash");
                    configureOptions.ClaimActions.DeleteClaim("auth_time");

                    // if authenticaion middlwear doewsnt map value in identity toke,
                    // we caan writ that to map it manually
                    /*configureOptions.ClaimActions.MapUniqueJsonKey("key","name in identty token");*/

                    //Configure Middlware How Validation Shouid Happen
                    //also help us to specify Roles Claims
                    configureOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.GivenName,
                        RoleClaimType = JwtClaimTypes.Role
                    };
                }
            );

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
