using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowBook>>> GetBooks([FromQuery] int? authorId)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .AsQueryable();

            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }

            var books = await query
                .Select(b => new ShowBook
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Author = b.Author
                })
                .ToListAsync();

            return books;
        }


        [HttpPost]
        public async Task<ActionResult<ShowBook>> CreateBook(CreateBook dto)
        {
            var author = await _context.Authors.FindAsync(dto.AuthorId);
            if (author == null)
                return BadRequest("Author not found");

            var book = new Book
            {
                Title = dto.Title,
                Year = dto.Year,
                AuthorId = dto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            await _context.Entry(book).Reference(b => b.Author).LoadAsync();

            var result = new ShowBook
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                Author = book.Author
            };

            return CreatedAtAction(nameof(GetBooks), result);
        }

 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, CreateBook dto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId);
            if (!authorExists)
                return BadRequest("Author not found");

            book.Title = dto.Title;
            book.Year = dto.Year;
            book.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
