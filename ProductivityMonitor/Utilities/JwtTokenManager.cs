using Microsoft.IdentityModel.Tokens;
using ProductivityMonitor.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductivityMonitor.Utilities
{
    public class JwtTokenManager : IJwtTokenManager
    {
        IConfiguration _configuration;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username,IList<string> roleNames)
        {
            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);

            //add name and roles to claims list
            List<Claim> claims = new List<Claim>();
            //add name claim
            claims.Add(new Claim(ClaimTypes.Name, username));
            //add roles claim
            foreach (var name in roleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, name));
            }

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}
