using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review.Data;
using Review.DTO;
using Review.Models;

namespace Review.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    public class ReviewController : Controller
    {
        private readonly ReviewContext _context;
        private ILogger<ReviewController> _logger;

        public ReviewController(ReviewContext context, ILogger<ReviewController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{bookId}/reviews")]
        public async Task<IList<ReviewDTO>> GetAllReviewsForBook(long bookId)
        {
            _logger.LogInformation("Get all reviews for book '{id}'", bookId);
            return await _context.Reviews.Select(x => ReviewToDTO(x)).ToListAsync();
        }

        [HttpPost("{bookId}/reviews")]
        public async Task<ActionResult<ReviewDTO>> CreateNewReviewForBook(long bookId, ReviewDTO reviewDTO)
        {
            _logger.LogInformation("Create new review for book '{id}'", bookId);
            ReviewModel review = new ReviewModel
            {
                BookId = reviewDTO.BookId,
                MemberId = reviewDTO.MemberId,
                Description = reviewDTO.Description,
                Rating = reviewDTO.Rating
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Review created for book '{id}'", bookId);

            return CreatedAtAction(nameof(GetReviewForBook), new { bookId = review.BookId, reviewId = review.Id}, ReviewToDTO(review));
        }

        [HttpGet("{bookId}/reviews/{reviewId}")]
        public async Task<ActionResult<ReviewDTO>> GetReviewForBook(long bookId, long reviewId)
        {
            _logger.LogInformation("Get review '{reviewId}' for book '{bookID}'", reviewId, bookId);

            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
            {
                _logger.LogWarning("Review with id: '{reviewId}' does not exist for book '{bookID}'", reviewId, bookId);
                return NotFound();
            }

            _logger.LogInformation("Review '{reviewId}' for book '{bookID}' was found", reviewId, bookId);

            return ReviewToDTO(review);
        }

        private static ReviewDTO ReviewToDTO(ReviewModel review) => new ReviewDTO 
        {
            Id = review.Id,
            MemberId = review.MemberId,
            BookId = review.BookId,
            Rating = review.Rating, 
            Description = review.Description,
        };
    }
}
