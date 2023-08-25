using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMA_Core.DTOs;
using PMA_Core.Models;
using PMA_Data;
using PMA_Services.Services;
using System.Security.Cryptography;
using System.Text;

namespace PMA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly UserRegistrationService _registrationService;

        public UserRegistrationController(UserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<ActionResult<PMA_User>> RegisterUserAsync([FromBody] PMA_UserRegistrationRequest registrationRequest)
        {
            try
            {
                var user = new PMA_User
                {
                    FirstName = registrationRequest.FirstName,
                    LastName = registrationRequest.LastName,
                    Email = registrationRequest.Email,
                    RoleID = 3
                    // Add other properties as needed
                };

                var registeredUser = await _registrationService.RegisterUserAsync(user, registrationRequest.Password);
                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
