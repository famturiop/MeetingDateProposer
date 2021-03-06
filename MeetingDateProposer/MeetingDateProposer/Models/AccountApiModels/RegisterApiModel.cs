using System.ComponentModel.DataAnnotations;

namespace MeetingDateProposer.Models.AccountApiModels
{
    public class RegisterApiModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(ValidationRules.PasswordMaxLength,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = ValidationRules.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}