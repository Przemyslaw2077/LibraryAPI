using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        [MinLength(1)]
        public string Title { get; set; } = null!;

        [Range(0, 2999)]
        public int Year { get; set; }

        [Required]
        public int AuthorId { get; set; }   
        public virtual Author? Author { get; set; } 

        public List<Copy> Copies { get; set; } = new();
    }
}
