using Microsoft.AspNetCore.Identity;

namespace BikeKinnus.Models.Models
{
    public class AppUser: IdentityUser
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        
        public int? Age { get; set; }
       
    }
}
