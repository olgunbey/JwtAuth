using Microsoft.AspNetCore.Mvc;

namespace MiniApi1.Controllers
{
    [Route("api/[controller]")]
    public class StockController : Controller
    {
        [HttpGet("GetStock")]
        public IActionResult GetStock()
        {
            return Ok();
        }
    }
}
