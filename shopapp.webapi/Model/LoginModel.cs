using System.ComponentModel.DataAnnotations;

namespace shopapp.webapi.Model
{
    public class LoginModel
    {
        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(30,MinimumLength =6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}