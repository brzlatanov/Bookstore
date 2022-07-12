using System.ComponentModel.DataAnnotations;

namespace BookstoreWeb.DTO
{
    public class CategoryInputDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number too high or negative!")]
        public string DisplayOrder { get; set; }
    }
}
