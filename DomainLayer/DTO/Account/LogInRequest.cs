

namespace DomainLayer.DTO.Authentication
{
    public class LogInRequest
    {
        public required string Email { get;  set; }
        public required string Password { get; set; }

        public required string CaptchaToken { get; set; }
    }
}
