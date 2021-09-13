using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NSE.WebAPI.Core.Identity.ClaimsAuth
{
    public class CustomAuthorization
    {
        public static bool ValidateUserClaim(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated && 
                   context.User.Claims.Any(x => x.Type == claimName && x.Value.Contains(claimValue));
        }
    }
}
