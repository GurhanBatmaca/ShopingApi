using Microsoft.AspNetCore.Identity;

namespace shopapp.webapi.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}