using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("copies")]
    public class CopiesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public CopiesController(LibraryContext context)
        {
            _context = context;
        }

        // GET /copies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowCopy>>> GetCopies()
        {
            var copies = await _context.Copies
                .Include(c => c.Book)
                .Select(c => new ShowCopy
                {
                    Id = c.Id,
                    BookId = c.Book.Id,
                    BookTitle = c.Book.Title
                })
                .ToListAsync();

            return copies;
        }

        // POST /copies
        [HttpPost]
        public async Task<ActionResult<ShowCopy>> CreateCopy(CreateCopy dto)
        {
            var book = await _context.Books.FindAsync(dto.BookId);
            if (book == null)
                return BadRequest("Book not found");

            var copy = new Copy
            {
                BookId = dto.BookId
            };

            _context.Copies.Add(copy);
            await _context.SaveChangesAsync();

            var result = new ShowCopy
            {
                Id = copy.Id,
                BookId = book.Id,
                BookTitle = book.Title
            };

            return CreatedAtAction(nameof(GetCopies), result);
        }


        // PUT /copies/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCopy(int id, CreateCopy dto)
        {
            var copy = await _context.Copies.FindAsync(id);
            if (copy == null)
                return NotFound();

            var bookExists = await _context.Books.AnyAsync(b => b.Id == dto.BookId);
            if (!bookExists)
                return BadRequest("Book not found");

            copy.BookId = dto.BookId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCopy(int id)
        {
            var copy = await _context.Copies.FindAsync(id);
            if (copy == null)
                return NotFound();

            _context.Copies.Remove(copy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
