

using Contracts.InfrastructureLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace InfrastructureLayer.Service
{
    public class CrypterService : ICrypterService
    {
        private readonly PasswordHasher<object> _hasher = new();
        private readonly ILogger _logger;

        public CrypterService(ILogger<CrypterService> logger)
        {
            _logger = logger;
        }
        public bool CompareHash(string plainText, string hashed)
        {
            try
            {
                var results = _hasher.VerifyHashedPassword(null!, hashed, plainText);
                return results != PasswordVerificationResult.Failed;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occured at ${nameof(CrypterService)} in ${nameof(CompareHash)}");
                throw;
            }
        }

        public string EncryptString(string plainText)
        {
            try
            {
                return _hasher.HashPassword(null!, plainText);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occured at ${nameof(CrypterService)} in ${nameof(EncryptString)}");
                throw;
            }
        }
    }
}