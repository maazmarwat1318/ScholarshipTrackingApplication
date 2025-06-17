

namespace DomainLayer.DTO.Authentication
{
    public class ResetPasswordRequest
    {
        public required string Password { get; set; }
        public required string Token { get; set; }
    }
}
