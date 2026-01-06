using LibraryApi.Models;

namespace LibraryApi.DTOs
{
    public class ShowBook
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public Author Author { get; set; } = null!;
    }
}
