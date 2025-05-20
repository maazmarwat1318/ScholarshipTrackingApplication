using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCPresentationLayer.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        [MaxLength(15)]
        [DisplayName("New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

        [DisplayName("Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Token { get; set; } = null!;

    }
}
