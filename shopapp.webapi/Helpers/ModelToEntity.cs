using shopapp.entity;
using shopapp.webapi.Model;
using shopapp.webui.Helpers;

namespace shopapp.webapi.Helpers
{
    public static class ModelToEntity
    {
        public static Product ModelToProduct(ProductModel model)
        {
            return new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Url = UrlModifier.Modifie(model.Name!),
                IsAproved =model.IsAproved,
                IsHome = model.IsHome,
                IsPopular = model.IsPopular
            };
        }
    }
}