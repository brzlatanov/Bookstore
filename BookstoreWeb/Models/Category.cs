using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookstoreWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Please enter a genre name that's below 50 symbols.")]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid input - please use a non-negative number above zero.")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
