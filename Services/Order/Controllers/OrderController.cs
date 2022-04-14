using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Data;
using Order.DTO;
using Order.Models;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : Controller
    {
        private readonly OrderContext _context;

        public OrderController(OrderContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IList<OrderDTO>> GetAllOrders()
        {
            return await _context.Orders.Select(x => OrderToDTO(x)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO orderDTO)
        {
            OrderModel order = new OrderModel
            {
                BookId = orderDTO.BookId,
                MemberId = orderDTO.MemberId,
                checkoutDate = (DateTime)(orderDTO.checkoutDate == null ? DateTime.Now : orderDTO.checkoutDate), 
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllOrders), new { id = order.Id }, OrderToDTO(order));
        }

        [HttpGet("{id}")]
        public void GetSingleOrder(long id)
        {

        }

        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private static OrderDTO OrderToDTO(OrderModel order) => new OrderDTO() { 
            Id = order.Id, 
            BookId = order.BookId, 
            MemberId = order.MemberId, 
            checkoutDate = order.checkoutDate
        };    
    }
}
