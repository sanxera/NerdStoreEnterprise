using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace NSE.WebAPI.Core.Identity.ClaimsAuth
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilterRequirement))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}