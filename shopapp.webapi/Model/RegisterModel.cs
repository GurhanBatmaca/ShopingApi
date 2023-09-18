using System.ComponentModel.DataAnnotations;

namespace shopapp.webapi.Model
{
    public class RegisterModel
    {
        [Required]
        [StringLength(15,MinimumLength =3)]
        public string? UserName { get; set; }

        [Required]
        [StringLength(15,MinimumLength =2)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(15,MinimumLength =2)]
        public string? LastName { get; set; }

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