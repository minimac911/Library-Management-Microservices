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

        public ReviewController(ReviewContext context)
        {
            _context = context;
        }

        [HttpGet("{bookId}/reviews")]
        public async Task<IList<ReviewDTO>> GetAllReviewsForBook(long bookId)
        {
            return await _context.Reviews.Select(x => ReviewToDTO(x)).ToListAsync();
        }

        [HttpPost("{bookId}/reviews")]
        public async Task<ActionResult<ReviewDTO>> CreateNewReviewForBook(long bookId, ReviewDTO reviewDTO)
        {
            ReviewModel review = new ReviewModel
            {
                BookId = reviewDTO.BookId,
                MemberId = reviewDTO.MemberId,
                Description = reviewDTO.Description,
                Rating = reviewDTO.Rating
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewForBook), new { bookId = review.BookId, reviewId = review.Id}, ReviewToDTO(review));
        }

        [HttpGet("{bookId}/reviews/{reviewId}")]
        public async Task<ActionResult<ReviewDTO>> GetReviewForBook(long bookId, long reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
            {
                return NotFound();
            }

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
