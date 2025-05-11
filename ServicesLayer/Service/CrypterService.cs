
using InfrastructureLayer.Interface;
using Microsoft.AspNetCore.Identity;

namespace InfrastructureLayer.Service
{
    public class CrypterService : ICrypterService
    {
        private readonly PasswordHasher<object> _hasher = new();
        public bool CompareHash(string plainText, string hashed)
        {
            var results = _hasher.VerifyHashedPassword(null!, hashed, plainText);
            return results != PasswordVerificationResult.Failed;
        }

        public string EncryptString(string plainText)
        {
            return _hasher.HashPassword(null!, plainText);
        }
    }
}