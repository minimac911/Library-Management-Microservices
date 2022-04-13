using Microsoft.EntityFrameworkCore;

namespace Book.Data
{
    public class BookContext : DbContext
    {
        public DbSet<BookModel> Books { get; set; }

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookModel>().HasData(
                new BookModel() { Id = 1, Title = "Book A", Author = "Author A", ISBN = Guid.NewGuid().ToString() },
                new BookModel() { Id = 2, Title = "Book B", Author = "Author B", ISBN = Guid.NewGuid().ToString() },
                new BookModel() { Id = 3, Title = "Book C", Author = "Author C", ISBN = Guid.NewGuid().ToString() }
            );
        }
    }
}
