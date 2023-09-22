using System.ComponentModel.DataAnnotations;

namespace shopapp.entity
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAproved { get; set; }
        public bool IsHome { get; set; }
        public bool IsPopular{ get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public List<ProductCategory>? ProductCategories { get; set; }
    }
}