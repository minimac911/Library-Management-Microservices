using Microsoft.AspNetCore.Mvc;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : Controller
    {
        [HttpGet]
        public void GetAllOrders()
        {

        }

        [HttpPost]
        public void CreateOrder()
        {

        }

        [HttpGet("{id}")]
        public void GetSingleOrder(long id)
        {

        }
    }
}
