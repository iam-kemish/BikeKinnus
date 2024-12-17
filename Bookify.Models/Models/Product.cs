using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeKinnus.Models.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; } // Name or model of the bike
        [Required]
        public string? Description { get; set; } // Bike details and specs

        [Required]
        [Display(Name = "Brand")]
        public string? Brand { get; set; } // Bike brand, e.g., Yamaha, Honda

        [Required]
        [Display(Name = "Price")]
        [Range(100, 1000000, ErrorMessage = "Price must be between 100 and 1,000,000.")]
        public double Price { get; set; } // Price of the bike

        [Required]
        [Display(Name = "Model Year")]
        public int ModelYear { get; set; } // Year of manufacturing

        [Required]
        [Display(Name = "Engine Capacity (cc)")]
        public int EngineCapacity { get; set; } // Engine size in cc

        [Required]
        [Display(Name = "Mileage (km/l)")]
        public double Mileage { get; set; } // Mileage or fuel efficiency

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; } // Relation to category table

        [ValidateNever]
        public string? ImageUrl { get; set; }   //Bike image.

    }
}
