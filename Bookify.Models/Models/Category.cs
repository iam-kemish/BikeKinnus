using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(70)]
        public string Name { get; set; }
            [Required]
        public int DisplayOrder {  get; set; }

    }
}
