using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Services.Services;

namespace PMA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserProfileService _profileService;

        public UserProfileController(UserProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<PMA_User>> GetUserProfile(int userId)
        {
            var userProfile = await _profileService.GetUserProfileAsync(userId);

            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserProfile(int userId, PMA_UserProfileUpdate updatedProfile)
        {
            // Check if the user making the request matches the target user (authorization logic)
            //var currentUserId = int.Parse(User.FindFirst("sub").Value);

            //if (currentUserId != userId)
            //{
            //    return Forbid(); // Or return some other appropriate status code
            //}

            try
            {
                await _profileService.UpdateUserProfileAsync(userId, updatedProfile);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
