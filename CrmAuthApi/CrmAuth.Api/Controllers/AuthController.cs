using CrmAuth.Application.Handlers;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace CrmAuth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private ListUserIdsHandler ListUserIdsHandler;
        private LoginHandler LoginHandler;
        public AuthController(IConfiguration config)
        {
            MySqlConnection connection = new(config.GetConnectionString("crm"));
            ListUserIdsHandler = new ListUserIdsHandler(connection);
            LoginHandler = new LoginHandler(connection);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] string Email)
        {
            try
            {
                bool r = LoginHandler.Handle(Email);
                return Ok(r);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("ListIds")]
        public IActionResult Index()
        {
            try
            {
                var list = ListUserIdsHandler.Handle();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
