using System.ComponentModel.DataAnnotations;

namespace shopapp.webapi.Model
{
    public class ProductModel
    {
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string? Name { get; set; }

        // public string? Url { get; set; }
        [Required]
        [Range(1,100000)]
        public double? Price { get; set; }

        [Required]
        [StringLength(150,MinimumLength =10)]
        public string? Description { get; set; }

        public bool IsAproved { get; set; }
        public bool IsHome { get; set; }
        public bool IsPopular{ get; set; }
        // public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}