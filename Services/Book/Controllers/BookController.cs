using Book.Data;
using Book.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : Controller
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IList<BookDTO>> GetAllBooks()
        {
            return await _context.Books.Select(x => BookToDTO(x)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDTO)
        {
            BookModel book = new BookModel
            {
                Title = bookDTO.Title,
                ISBN = bookDTO.ISBN,
                Author= bookDTO.Author,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllBooks), new { id = book.Id }, BookToDTO(book));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetSingleBook(long id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return BookToDTO(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(long id, BookDTO bookDTO)
        {
            if (id != bookDTO.Id)
            {
                return BadRequest();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = bookDTO.Title;
            book.Author = bookDTO.Author;
            book.ISBN = bookDTO.ISBN;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BookExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(long id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private static BookDTO BookToDTO(BookModel book) => new BookDTO
        {
            Id = book.Id,
            Author = book.Author,
            ISBN = book.ISBN,
            Title = book.Title
        };
    }
}
