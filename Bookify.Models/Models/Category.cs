using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BikeKinnus.Models.Models
{


    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } // Category name, e.g., Sports, Cruiser, Commuter

        public string? Description { get; set; }
    }
}