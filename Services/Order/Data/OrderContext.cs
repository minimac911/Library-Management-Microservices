using Microsoft.EntityFrameworkCore;
using Order.Models;

namespace Order.Data
{
    public class OrderContext : DbContext
    {
        public DbSet<OrderModel> Orders { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
    }
}
