using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BikeKinnus.Models.Models
{
    public class AppUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string State { get; set; }

        public string PostalCode { get; set; }

    }
}
