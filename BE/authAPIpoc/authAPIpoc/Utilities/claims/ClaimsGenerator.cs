using System.Security.Claims;

namespace authAPIpoc.Utilities.claims
{
    public class ClaimsGenerator
    {
        public static Claim[] generateClaims(dynamic user)
        {
            Claim[] claims = null;
            try
            {
                if (user != null)
                {

                    string roles = string.Join(",", user.roleList[0].roleList);
                    claims = new Claim[] {
                     new Claim("identity", user.Identity ),
                    new Claim(ClaimTypes.Name, user.userName ),
                    new Claim(ClaimTypes.Role, roles),
                    new Claim(ClaimTypes.Email, user.email ),

                };

                }
                else
                {
                    claims = new Claim[] {
                     new Claim("serviceAccount", "common user" ),
                };

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error : {ex.Message}");
            }
            return claims;
        }
    }
}
