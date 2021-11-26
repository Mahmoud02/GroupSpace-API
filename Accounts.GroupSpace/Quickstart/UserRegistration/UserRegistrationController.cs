using Accounts.GroupSpace.Entities;
using Accounts.GroupSpace.Services;
using Accounts.GroupSpace.Utilities;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Accounts.GroupSpace.Quickstart.UserRegistration
{
    public class UserRegistrationController : Controller
    {
        private readonly ILocalUserService _localUserService;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;



        public UserRegistrationController(ILocalUserService localUserService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this._localUserService = localUserService ??
                throw new ArgumentNullException(nameof(localUserService));

            this._configuration = configuration;

            this._httpClientFactory = httpClientFactory ??
               throw new ArgumentNullException(nameof(httpClientFactory)); 

        }

        [HttpGet]
        public async Task<IActionResult> ActivateUser(string securityCode)
        {
            if (await _localUserService.ActivateUser(securityCode))
            {
                ViewData["Message"] = "Your account was successfully activated.  " +
                    "Navigate to your client application to log in.";
            }
            else
            {
                ViewData["Message"] = "Your account couldn't be activated, " +
                    "please contact your administrator.";
            }

            await _localUserService.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser(string returnUrl)
        {
            var vm = new RegisterUserViewModel(){ ReturnUrl = returnUrl };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userToCreate = new User
            {
                Username = model.UserName,
                Subject = Guid.NewGuid().ToString(),
                Email = model.Email,
                Active = false
            };

            userToCreate.Claims.Add(new UserClaim()
            {
                Type = JwtClaimTypes.Role,
                Value = "User"
            });
            userToCreate.Claims.Add(new UserClaim()
            {
                Type = JwtClaimTypes.Email,
                Value = model.Email
            });

            _localUserService.AddUser(userToCreate, model.Password);
            
            var Operationstatus = await _localUserService.SaveChangesAsync();
            //Save User Entity  Information at API Database
            if (Operationstatus) {

               await saveUserAtApi(userToCreate);
            }

            // create an activation link
            var link = Url.ActionLink("ActivateUser", "UserRegistration",
                new { securityCode = userToCreate.SecurityCode });

            Debug.WriteLine(link);

            return View("ActivationCodeSent");
        }
        //
        private async Task saveUserAtApi (User user) {
            var SigningKey = _configuration["JwtSettings:SigningKey"];
            var Issuer = _configuration["JwtSettings:Issuer"];
            var Audience = _configuration["JwtSettings:Audience"];
            var TokenTimeoutDays = Double.Parse(_configuration["JwtSettings:TokenTimeoutDays"]);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, user.Username)
            };

            var tokenDescriptor = JwtHelper.GetJwtToken(
                        user.Subject,
                        SigningKey,
                        Issuer,
                        Audience,
                        TimeSpan.FromDays(TokenTimeoutDays),
                        claims.ToArray());
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var GroupSpaceApiClient = _httpClientFactory.CreateClient("GroupSpaceApiClient");
            GroupSpaceApiClient.DefaultRequestHeaders.Authorization =
           new AuthenticationHeaderValue("Bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Post,"/api/users/");
            var response = await GroupSpaceApiClient.SendAsync(
               request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Here we success");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                Debug.WriteLine("un Authoriazed");
            }
        }

    }
}
