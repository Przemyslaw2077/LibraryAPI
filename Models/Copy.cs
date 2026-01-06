using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class Copy
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; } 
    }
}
