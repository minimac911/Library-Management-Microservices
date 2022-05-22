using Book.Data;
using Book.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    public class BookController : Controller
    {
        private readonly BookContext _context;

        private ILogger<BookController> _logger;

        public BookController(BookContext context, ILogger<BookController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IList<BookDTO>> GetAllBooks()
        {
            _logger.LogInformation("Get all books");
            List<BookDTO> bookDTOs = await _context.Books.Select(x => BookToDTO(x)).ToListAsync();
            _logger.LogInformation(bookDTOs.Count+" books found");

            return bookDTOs;
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDTO)
        {
            _logger.LogInformation("Create book: {Book}", bookDTO);

            BookModel book = new BookModel
            {
                Title = bookDTO.Title,
                ISBN = bookDTO.ISBN,
                Author= bookDTO.Author,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Book saved");

            return CreatedAtAction(nameof(GetAllBooks), new { id = book.Id }, BookToDTO(book));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetSingleBook(long id)
        {
            _logger.LogInformation("Get book with id: {id}", id);

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                _logger.LogError("Book '{id}' does not exist", id);

                return NotFound();
            }

            return BookToDTO(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(long id, BookDTO bookDTO)
        {
            _logger.LogInformation("Update book: {id}", id);

            if (id != bookDTO.Id)
            {
                _logger.LogError("Path ID and body ID for boot does not mathc: {id} != {bookId}", id, bookDTO.Id);
                return BadRequest();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                _logger.LogError("Book with id '{id}' does not exist", id);
                return NotFound();
            }

            book.Title = bookDTO.Title;
            book.Author = bookDTO.Author;
            book.ISBN = bookDTO.ISBN;

            try
            {
                _logger.LogInformation("Saving updates for book '{id}'", id);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Updates saved for book '{id}'", id);
            }
            catch (DbUpdateConcurrencyException) when (!BookExists(id))
            {
                _logger.LogError("Book with id '{id}' does not exist", id);
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            _logger.LogInformation("Delete book '{id}'", id);

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                _logger.LogError("Book with id '{id}' does not exist", id);
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Book '{id}' has been deleted", id);

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
