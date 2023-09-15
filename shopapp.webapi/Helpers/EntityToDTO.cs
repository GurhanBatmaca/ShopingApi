using shopapp.entity;

namespace shopapp.webapi.DTO
{
    public static class EntityToDTO
    {
        public static ProductDTO ProductToDTO(Product p)
        {
            return new ProductDTO()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Url = p.Url
            };
        }
    }
}