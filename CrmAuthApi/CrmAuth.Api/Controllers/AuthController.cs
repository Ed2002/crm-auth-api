using Microsoft.AspNetCore.Mvc;

namespace CrmAuth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpGet("Ok")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
