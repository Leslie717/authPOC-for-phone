using authAPIpoc.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace authAPIpoc.Utilities.token
{
    public class TokenGenerator
    {
        public static string generateToken(IEnumerable<Claim> claimData, IOptions<TokenModel> TokenSettings)
        {
            string tokenString = string.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(TokenSettings.Value.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity(claimData),

                    Issuer = TokenSettings.Value.Issuer,
                    Audience = TokenSettings.Value.Audience,
                    Expires = DateTime.UtcNow.AddMinutes(20), // changed expiry time from 20 to 1 min
                    SigningCredentials = new SigningCredentials(new
                    SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenString = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token genration failed :{ex.Message}");
            }
            return tokenString;
        }
    }
}
