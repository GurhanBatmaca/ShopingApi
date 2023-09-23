using shopapp.entity;

namespace shopapp.webapi.Model
{
    public class ResponseAdminObject
    {
        public Product? Product { get; set; }
        public string? Message { get; set; }
    }
}