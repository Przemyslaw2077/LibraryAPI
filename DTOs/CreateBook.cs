using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs
{
    public class CreateBook
    {
        [MaxLength(255)]
        [Required]
        [MinLength(1)]
        public string Title { get; set; } = null!;
        [Range(0, 2999)]
        public int Year { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
