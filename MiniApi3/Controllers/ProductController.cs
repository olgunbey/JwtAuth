using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiniApi3.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        [Authorize]
        [HttpGet("GetProduct")]
        public IActionResult GetProduct()
        {
            return Ok("Getting product!");
        }
    }
}
