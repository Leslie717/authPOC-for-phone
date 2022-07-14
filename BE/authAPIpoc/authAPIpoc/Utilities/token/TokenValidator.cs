using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace authAPIpoc.Utilities.token
{
    public class TokenValidator
    {
        public static JwtSecurityToken ValidateAndDecode(string jwt, IEnumerable<SecurityKey> signingKeys, IConfigurationSection tokenConfig)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["Secret"])),
                ValidateIssuer = true,
                ValidIssuer = tokenConfig["Issuer"],
                ValidateAudience = true,
                ValidAudience = tokenConfig["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };

            try
            {
                var claimsPrincipal = new JwtSecurityTokenHandler()
                    .ValidateToken(jwt, validationParameters, out var rawValidatedToken);

                return (JwtSecurityToken)rawValidatedToken;
                // Or, you can return the ClaimsPrincipal
                // (which has the JWT properties automatically mapped to .NET claims)
            }
            catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                // TODO: Log it or display an error.
                throw new Exception($"Token failed validation: {stvex.Message}");
            }
            catch (ArgumentException argex)
            {
                // The token was not well-formed or was invalid for some other reason.
                // TODO: Log it or display an error.
                throw new Exception($"Token was invalid: {argex.Message}");
            }
        }



        public static string getUserId(string token)
        {
            string userId = string.Empty;
            ClaimsPrincipal principal = null;
            try
            {
                if (!string.IsNullOrEmpty(token))
                    principal = getClaimPrincipal(token);

                userId = principal.FindFirst("identity")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new SecurityTokenException($"Missing claim: identity !");
                }

            }
            catch { throw; }
            return userId;
        }

        public static string userName(string token)
        {
            string userName = string.Empty;
            ClaimsPrincipal principal = null;
            try
            {
                if (!string.IsNullOrEmpty(token))
                    principal = getClaimPrincipal(token);

                userName = principal.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(userName))
                {
                    userName = "No UserNames in the Claim";
                }

            }
            catch { throw; }
            return userName;
        }

        public static ClaimsPrincipal getClaimPrincipal(string token)
        {
            ClaimsPrincipal principal = null;
            try
            {
                var tokenValidationParamters = new TokenValidationParameters

                {

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("YnlyZWJhcm4gaXMgdGhlIGJlc3Qgb25saW5lIGFuaW1hbCBhdWN0aW9uIHBsYXRmb3Jt")),

                    ValidateIssuer = true,

                    ValidIssuer = "www.byrebarn.com",

                    ValidateAudience = true,

                    ValidAudience = "Byrebarn Clients",

                    ValidateLifetime = false,

                };

                var handler = new JwtSecurityTokenHandler();

                var tokenHandlerSec = new JwtSecurityTokenHandler();

                SecurityToken securityToken;

                principal = tokenHandlerSec.ValidateToken(token, tokenValidationParamters, out securityToken);

                var jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))

                {

                    throw new SecurityTokenException("Invalid token!");

                }

            }
            catch { throw; }

            return principal;
        }


    }
}
