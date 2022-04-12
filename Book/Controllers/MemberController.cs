using Microsoft.AspNetCore.Mvc;

namespace Book.Controllers
{
    [ApiController]
    [Route("member")]
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
