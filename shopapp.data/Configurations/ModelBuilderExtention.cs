using Microsoft.EntityFrameworkCore;
using shopapp.entity;


namespace shopapp.data.Configurations
{
    public static class ModelBuilderExtention
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product(){Id=1,Name="Yeni Kaşar",Url="yeni-kasar",Price=250,ImageUrl="1.jpg",Description="Yeni Kaşar",IsAproved=true,IsHome=true,IsPopular=true},
                new Product(){Id=2,Name="Eski Kaşar",Url="eski-kasar",Price=280,ImageUrl="2.jpg",Description="Eski Kaşar",IsAproved=true,IsHome=true},

                new Product(){Id=3,Name="Kara Kovan Balı",Url="kara-kovan-bali",Price=280,ImageUrl="3.jpg",Description="Kara Kovan Balı",IsAproved=true,IsHome=true,IsPopular=true},
                new Product(){Id=4,Name="Petek Çiçek Balı",Url="petek-cicek-bali",Price=280,ImageUrl="4.jpg",Description="Petek Çiçek Balı",IsAproved=true,IsHome=true},
                new Product(){Id=5,Name="Süzme Çiçek Balı",Url="suzme-cicek-bali",Price=280,ImageUrl="5.jpg",Description="Süzme Çiçek Balı",IsAproved=true,IsHome=true,IsPopular=true}
            );

            modelBuilder.Entity<Category>().HasData(
                new Category(){Id=1,Name="Kaşar",Url="kasar"},
                new Category(){Id=2,Name="Eski Kaşar",Url="eski-kasar"},
                new Category(){Id=3,Name="Yeni Kaşar",Url="yeni-kasar"},

                new Category(){Id=4,Name="Süzme Bal",Url="suzme-bal"},
                new Category(){Id=5,Name="Petek Bal",Url="petek-bal"},
                new Category(){Id=6,Name="Kara Kovan Bal",Url="kara-kovan-bal"},
                new Category(){Id=7,Name="Çiçek Bal",Url="cicek-bal"}
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory(){ProductId=1,CategoryId=1},
                new ProductCategory(){ProductId=1,CategoryId=3},
                new ProductCategory(){ProductId=2,CategoryId=1},
                new ProductCategory(){ProductId=2,CategoryId=2},

                new ProductCategory(){ProductId=3,CategoryId=5},
                new ProductCategory(){ProductId=4,CategoryId=4},
                new ProductCategory(){ProductId=5,CategoryId=4}
            );
        }
    }
}