
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InfrastructureLayer.Interface;
using InfrastructureLayer.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DomainLayer.Enums;
using System.Diagnostics;

namespace InfrastructureLayer.Service
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }


        public string GenerateToken(string firstName, string lastName,  string id, string email, Role role)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
             new Claim("id", id),
             new Claim("firstname", firstName),
             new Claim("lastname", lastName),
             new Claim(ClaimTypes.Email, email),
             new Claim(ClaimTypes.Role, role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtOptions.ExpiryMinutes)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
