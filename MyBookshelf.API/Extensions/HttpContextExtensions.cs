using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MyBookshelf.API.Extensions
{
    public static class HttpContextExtensions
    {
        public static int? UserId(this IIdentity identityBase)
        {
            var identity = identityBase as ClaimsIdentity;
            if(identity != null)
            {
                return int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            }

            return null;
        }
    }
}
