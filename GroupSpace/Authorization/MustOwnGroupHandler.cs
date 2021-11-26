using GroupSpace.BLL;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupSpaceApi.Authorization
{
    public class MustOwnGroupHandler : AuthorizationHandler<MustOwnGroupRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IGroupService groupService;


        public MustOwnGroupHandler(IHttpContextAccessor httpContextAccessor , IGroupService groupService) {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));

        }
        //Here We decide if our Requirement is met
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnGroupRequirement requirement)
        {
            var groupID = httpContextAccessor.HttpContext.GetRouteValue("Id").ToString();

            if (!Int32.TryParse(groupID, out int groupIdAsIntegar))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var ownerId = context.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject).Value;

            if(!groupService.IsGroupOwner(groupIdAsIntegar, ownerId))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            // all successed
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
