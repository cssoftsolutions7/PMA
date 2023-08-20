using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Data;

namespace PMA_Backend.TestingControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileUpdateTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserProfileUpdateTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint to retrieve the user's profile
        [HttpGet("{userId}")]
        public IActionResult GetUserProfile(int userId)
        {
            try
            {
                // Retrieve the user's profile based on the provided userId
                PMA_User user = _context.PMA_Users.FirstOrDefault(u => u.UserID == userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Remove sensitive information, such as password hash, before returning the user's profile
                user.PasswordHash = null;

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to update the user's profile
        [HttpPut("{userId}")]
        public IActionResult UpdateUserProfile(int userId, [FromBody] PMA_User updatedUser)
        {
            try
            {
                PMA_User user = _context.PMA_Users.FirstOrDefault(u => u.UserID == userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Update user profile properties
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Email = updatedUser.Email;
                user.PhoneNumber = updatedUser.PhoneNumber;
                user.Address = updatedUser.Address;
                user.BirthDate = updatedUser.BirthDate;
                user.Gender = updatedUser.Gender;

                // Check if a new profile picture URL is provided
                if (!string.IsNullOrWhiteSpace(updatedUser.ProfileImageURL))
                {
                    user.ProfileImageURL = updatedUser.ProfileImageURL;
                }

                _context.SaveChanges();

                // Remove sensitive information, such as password hash, before returning the updated user's profile
                user.PasswordHash = null;

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
