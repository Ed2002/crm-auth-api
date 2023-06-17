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
        public AuthController(IConfiguration config)
        {
            MySqlConnection connection = new(config.GetConnectionString("crm"));
            ListUserIdsHandler = new ListUserIdsHandler(connection);
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
