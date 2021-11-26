using ControlPanel.GroupSpace.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ControlPanel.GroupSpace.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;


        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var IdToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var claims = User.Claims;
            string msg ="" ;
            foreach(var x in claims)
            {
                msg += x.Type + " : " + x.Value + "  And  "; 

            }
            TempData["IdToken"] = IdToken;
            TempData["claims"] = msg;
            var IdbClient = _httpClientFactory.CreateClient("IDPClient");
            var metaDataOfAuthoriztioServer = await IdbClient.GetDiscoveryDocumentAsync();
            if (metaDataOfAuthoriztioServer.IsError)
            {
                throw new Exception(
                    "Problem during access dicovery endpoints", 
                    metaDataOfAuthoriztioServer.Exception
                    );
            }          
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            TempData["accessToken"] = accessToken;

            var userInfoRespnse = await IdbClient.GetUserInfoAsync(
                 new UserInfoRequest
                 {
                     Address = metaDataOfAuthoriztioServer.UserInfoEndpoint,
                     Token = accessToken
                 }
                );
            if (userInfoRespnse.IsError)
            {
                throw new Exception(
                    "Problem during access userInfo endpoints",
                    metaDataOfAuthoriztioServer.Exception
                    );
            }
            var address = userInfoRespnse.Claims.FirstOrDefault(c => c.Type == "address")?.Value;
            TempData["address"] = address;
            var role = User.IsInRole("Admin");
            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            TempData["refreshToken"] = refreshToken;

            return View();
        }
        public async Task  Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // To Logout From AuthoriztionServer
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
