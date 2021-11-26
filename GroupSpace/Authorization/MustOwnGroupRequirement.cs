using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupSpaceApi.Authorization
{
    public class MustOwnGroupRequirement : IAuthorizationRequirement
    {
        public MustOwnGroupRequirement() { }

    }
}
