using Microsoft.IdentityModel.Tokens;
using Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace foodAppBackEnd.GenerateJWTToken
{
    public class tokenGenerate
    {
        private readonly IConfiguration _configuration;
        public tokenGenerate(IConfiguration configuration) 
        { 
            this._configuration = configuration;
        }
        public string generateToken(tokenRequest request)
        {
            string token = string.Empty;
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
                var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sid, request.id),
                    new Claim(JwtRegisteredClaimNames.Email, request.email),
                    new Claim("Date", DateTime.Now.ToString()),
                };
                request.Role.Split(',').ToList().ForEach(it =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, it.ToString()));
                });
                var take = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                    expires: DateTime.Now.AddMinutes(20), signingCredentials: credentials);
                token = new JwtSecurityTokenHandler().WriteToken(take);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return token;
        }
    }
}
