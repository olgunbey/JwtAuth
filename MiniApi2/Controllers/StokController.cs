using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiniApi2.Controllers
{
    [Route("api/[controller]")]
    public class StokController : Controller
    {
        [Authorize]
        [HttpGet("AddStok")]
        public IActionResult AddStok()
        {
            return Ok("ADDED STOK");
        }
    }
}
