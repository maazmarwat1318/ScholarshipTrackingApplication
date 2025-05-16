
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InfrastructureLayer.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DomainLayer.Enums;
using Contracts.InfrastructureLayer;
using Microsoft.Extensions.Logging;
using DomainLayer.Common;
using DomainLayer.Errors.AuthenticationErrors;

namespace InfrastructureLayer.Service
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ILogger _logger;

        public JwtService(IOptions<JwtOptions> jwtOptions, ILogger<JwtService> logger)
        {
            _jwtOptions = jwtOptions.Value;
            _logger = logger;
        }



        public string GenerateAccessToken(int id, string firstName, string email, Role role)
        {
            try
            {
                return GenerateToken(id, _jwtOptions.AccessTokenExpiryDays * 1440, firstName, role, email);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occured at ${nameof(JwtService)} in ${nameof(GenerateAccessToken)}");
                throw;
            }
        }

        public string GenerateResetPasswordToken(int id)
        {
            try
            {
                return GenerateToken(id, _jwtOptions.ResetPasswordTokenExpiryMinutes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occured at ${nameof(JwtService)} in ${nameof(GenerateResetPasswordToken)}");
                throw;
            }
        }

        public T? GetClaimValue<T>(ClaimsPrincipal principal, string claimKey)
        {
            if (principal == null)
            {
                return default;
            }

            Claim? claim = principal.FindFirst(claimKey);
            if (claim == null)
            {
                return default;
            }

            try
            {
                if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Type? underlyingType = Nullable.GetUnderlyingType(typeof(T));
                    if (string.IsNullOrEmpty(claim.Value) || underlyingType == null)
                    {
                        return default;
                    }
                    object? convertedValue = Convert.ChangeType(claim.Value, underlyingType);
                    return (T?)convertedValue;
                }
                else
                {
                    T convertedValue = (T)Convert.ChangeType(claim.Value, typeof(T));
                    return convertedValue;
                }
            }
            catch (Exception)
            {
                return default;
            }
        }

        private string GenerateToken(int id, int expiryInMinutes, string? firstName = null, Role? role = null, string? email = null)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
             new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            };

            if (role != null) claims.Add(new Claim(ClaimTypes.Role, role.ToString()!));
            if (email != null) claims.Add(new Claim(ClaimTypes.Email, email));
            if (firstName != null) claims.Add(new Claim(ClaimTypes.Name, firstName));

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Response<ClaimsPrincipal> VerifyToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtOptions.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtOptions.Audience,
                    ClockSkew = TimeSpan.Zero
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return Response<ClaimsPrincipal>.Success(principal);
            }
            catch (SecurityTokenExpiredException ex)
            {
                return Response<ClaimsPrincipal>.Failure(AuthenticationErrorHelper.TokenExpiredError(null, ex));
            }
            catch (SecurityTokenValidationException ex)
            {
                return Response<ClaimsPrincipal>.Failure(AuthenticationErrorHelper.TokenInvalidError(null, ex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured at ${nameof(JwtService)} in ${nameof(GenerateResetPasswordToken)}");
                throw;
            }
        }
    }
}
