namespace Contracts.InfrastructureLayer
{
    public interface ICrypterService
    {
        public string EncryptString(string plainText);
        public bool CompareHash(string plainText, string hashed);
        
    }
}
