using System.ComponentModel.DataAnnotations;

namespace MVCPresentationLayer.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
