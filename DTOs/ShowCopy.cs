using LibraryApi.Models;

namespace LibraryApi.DTOs
{
    public class ShowCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
    }
}
