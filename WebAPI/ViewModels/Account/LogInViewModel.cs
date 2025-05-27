using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.Account
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Email is required",AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [MinLength(8)]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Please verify Captcha. Captcha is required", AllowEmptyStrings = false)]
        public string CaptchaToken { get; set; } = null!;
    }
}
