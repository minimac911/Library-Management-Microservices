using Member.Models;
using Microsoft.EntityFrameworkCore;

namespace Member.Data
{
    public class MemberContext : DbContext
    {
        public DbSet<MemberModel> Members { get; set; }

        public MemberContext(DbContextOptions<MemberContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberModel>().HasData(
                new MemberModel() { Id = 1, Name = "Test", Email = "test@gmail.com" },
                new MemberModel() { Id = 2, Name = "John", Email = "john@gmail.com" },
                new MemberModel() { Id = 3, Name = "Meg", Email = "meg@gmail.com" }
            );
        }
    }
}
