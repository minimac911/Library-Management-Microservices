using Microsoft.EntityFrameworkCore;
using Review.Models;

namespace Review.Data
{
    public class ReviewContext : DbContext
    {
        public DbSet<ReviewModel> Reviews { get; set; }

        public ReviewContext(DbContextOptions<ReviewContext> options) : base(options)
        {
        }
    }
}
