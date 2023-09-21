using System.ComponentModel.DataAnnotations;

namespace shopapp.webapi.Model
{
    public class ResetPasswordModel
    {
        [Required]
        public string? Token { get; set; }

        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(30,MinimumLength =6)]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage ="NewPassword and ReNewPassword must be the same.")]
        [StringLength(30,MinimumLength =6)]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string? ReNewPassword { get; set; }
    }
}