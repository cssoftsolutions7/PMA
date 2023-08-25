using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;
using PMA_AuthAPI.Data;
using PMA_Data;

namespace PMA_AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenHandler _jwtTokenHandler;
        private readonly DatabaseContext _context;

        public AccountController(TokenHandler tokenHandler, DatabaseContext context)
        {
            _jwtTokenHandler = tokenHandler;
            _context = context;
        }
        [HttpPost]
        public ActionResult<AuthResponse> Authenticate([FromBody] AuthRequest authRequest)
        {
            var authResponse = _jwtTokenHandler.GenerateJwtToken(authRequest);
            if (authResponse == null) return Unauthorized();
            return authResponse;
        }
    }
}
