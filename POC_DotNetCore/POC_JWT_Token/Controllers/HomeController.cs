using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace POC_JWT_Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public HomeController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Username == "test" && login.Password == "test")
            {
                var token = _tokenService.GenerateToken(login.Username, "f8a49d370fc041c9db6f2ea4bbb403cd3972124f5777c60c2d471e9e01d0f272");
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var username = User.Identity.Name;
            return Ok(new { Message = "This API is secure and username is : " + username });
        }
    }
}
