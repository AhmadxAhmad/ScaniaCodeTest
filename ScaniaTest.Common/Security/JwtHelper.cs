using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScaniaTest.Common.Security
{
    public class JwtHelper : IJwtHelper
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        private IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _httpContextAccessor = httpContextAccessor;
        }
        public object CreateToken(IEnumerable<Claim> claim)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var jwt = CreateJwtSecurityToken(_tokenOptions, signingCredentials, claim);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
            SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        public bool IsAuthenticated
        {
            get
            {
                var user = _httpContextAccessor.HttpContext.User;
                return user != null && user.Identity != null && user.Identity.IsAuthenticated;
            }
        }
        public object GetUserId()
        {
            var signedUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (signedUser == null) return null;
            return signedUser.Value;
        }

        public object GetOneClaimValue(string claimType)
        {
            var signedUser = _httpContextAccessor.HttpContext.User.FindAll(claimType);
            if (signedUser == null || !signedUser.Any())
                return null;

            var claimValues = signedUser.Select(c => c.Value).ToArray();
            return claimValues.Length == 1 ? (object)claimValues[0] : (object)claimValues;
        }

        public IEnumerable<Claim> GetAllClaims()
        {

            var claims = _httpContextAccessor.HttpContext.User.Claims;

            var result = claims.ToList();

            return result;

        }
    }
}
