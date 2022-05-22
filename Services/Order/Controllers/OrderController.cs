using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Data;
using Order.DTO;
using Order.Models;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : Controller
    {
        private readonly OrderContext _context;
        private ILogger<OrderController> _logger;

        public OrderController(OrderContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IList<OrderDTO>> GetAllOrders()
        {
            _logger.LogInformation("Get all orders");
            return await _context.Orders.Select(x => OrderToDTO(x)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO orderDTO)
        {
            _logger.LogInformation("Create order for member '{memberID}' to get book '{bookID}'", orderDTO.MemberId, orderDTO.BookId);

            OrderModel order = new OrderModel
            {
                BookId = orderDTO.BookId,
                MemberId = orderDTO.MemberId,
                checkoutDate = (DateTime)(orderDTO.checkoutDate == null ? DateTime.Now.ToUniversalTime() : orderDTO.checkoutDate), 
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Order successfully created for member '{memberID}' to get book '{bookID}'", orderDTO.MemberId, orderDTO.BookId);

            return CreatedAtAction(nameof(GetAllOrders), new { id = order.Id }, OrderToDTO(order));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetSingleOrder(long id)
        {
            _logger.LogInformation("Get a book by id: '{id}'", id);

            var order = await _context.Orders.FindAsync(id);

            if(order == null)
            {
                _logger.LogWarning("Book with id '{id}' does not exist", id);

                return NotFound();  
            }
            
            _logger.LogWarning("Book with id '{id}' found", id);

            return OrderToDTO(order);
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
