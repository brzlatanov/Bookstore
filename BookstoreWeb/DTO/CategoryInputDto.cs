using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookstoreWeb.DTO
{
    public class CategoryInputDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Please enter a genre name that's below 50 symbols.")]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid input - please use a non-negative number above zero.")]
        [DisplayName("Display Order")]
        public string DisplayOrder { get; set; }
    }
}
