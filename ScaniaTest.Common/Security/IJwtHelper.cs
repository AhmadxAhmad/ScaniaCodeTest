using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ScaniaTest.Common.Security
{
    public interface IJwtHelper
    {
        bool IsAuthenticated { get; }
        object CreateToken(IEnumerable<Claim> claim);
        IEnumerable<Claim> GetAllClaims();
        object GetOneClaimValue(string claimType);
        object GetUserId();
    }
}