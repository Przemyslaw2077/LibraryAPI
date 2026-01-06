using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class Author
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
    }
}
