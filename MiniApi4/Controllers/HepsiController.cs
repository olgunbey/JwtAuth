using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiniApi4.Controllers
{
    [Route("api/[controller]")]
    public class HepsiController : Controller
    {
        [Authorize]
        [HttpGet("GetHepsi")]
        public IActionResult GetHepsi()
        {
            return View();
        }
    }
}
