using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
