using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Data.Implementations;
using PMA_Services.Services;

namespace PMA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenHandlerService _jwtTokenHandler;        

        public AccountController(TokenHandlerService tokenHandler)
        {
            _jwtTokenHandler = tokenHandler;
        }
        [HttpPost]
        public async Task<Object> Authenticate([FromBody] AuthRequest authRequest)
        {
            var authResponse = await _jwtTokenHandler.GenerateJwtToken(authRequest);
            if (authResponse == null) return Unauthorized();
            return authResponse;
        }
    }
}
