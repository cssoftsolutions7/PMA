using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Services.Services;

namespace PMA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserLoginService _loginService;

        public LoginController(UserLoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<PMA_User>> LoginUserAsync([FromBody] PMA_LoginRequest loginRequest)
        {
            try
            {
                var user = await _loginService.AuthenticateUserAsync(loginRequest.Email, loginRequest.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
