using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodePulse.API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            //_userManager = userManager;
            _configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser identityUser, List<string> roles)
        {
            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, identityUser.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // JWT Security Token Parameters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            // Return Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
