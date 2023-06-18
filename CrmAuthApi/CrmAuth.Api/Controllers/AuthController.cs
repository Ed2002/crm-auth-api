using CrmAuth.Application.Commands;
using CrmAuth.Application.Handlers;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CrmAuth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private LoginHandler LoginHandler;
        private RegisterHandler RegisterHandler;
        public AuthController(IConfiguration config)
        {
            MySqlConnection connection = new(config.GetConnectionString("crm"));
            LoginHandler = new LoginHandler(connection);
            RegisterHandler = new RegisterHandler(connection);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterCommand request)
        {
            try
            {
                var r = RegisterHandler.Handle(request);
                return Ok(r);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginCommand request)
        {
            try
            {
                string result = LoginHandler.Handle(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
