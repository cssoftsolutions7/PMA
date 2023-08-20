using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Data;

namespace PMA_Backend.TestingControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyProgressReportTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailyProgressReportTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint to create a new daily progress update
        [HttpPost]
        public IActionResult CreateDailyProgress([FromBody] PMA_DailyProgress dailyProgress)
        {
            try
            {
                if (dailyProgress == null)
                {
                    return BadRequest("Invalid daily progress data");
                }

                // Add the new daily progress update to the database
                _context.PMA_DailyProgresses.Add(dailyProgress);
                _context.SaveChanges();

                return Ok(dailyProgress);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to list daily progress updates for the authenticated user
        [HttpGet]
        public IActionResult ListUserDailyProgress()
        {
            try
            {
                // Retrieve a list of daily progress updates associated with the authenticated user
                // Assuming you have a way to identify the current user (e.g., from a JWT token)
                // Replace "userId" with the actual user identifier
                int userId = 1; // Replace with the actual user ID

                List<PMA_DailyProgress> userDailyProgress = _context.PMA_DailyProgresses
                    .Where(dp => dp.UserID == userId)
                    .ToList();

                return Ok(userDailyProgress);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to retrieve daily progress update details by ID
        [HttpGet("{progressId}")]
        public IActionResult GetDailyProgressDetails(int progressId)
        {
            try
            {
                PMA_DailyProgress dailyProgress = _context.PMA_DailyProgresses.FirstOrDefault(dp => dp.ProgressID == progressId);

                if (dailyProgress == null)
                {
                    return NotFound("Daily progress update not found");
                }

                return Ok(dailyProgress);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to update daily progress update details
        [HttpPut("{progressId}")]
        public IActionResult UpdateDailyProgress(int progressId, [FromBody] PMA_DailyProgress updatedDailyProgress)
        {
            try
            {
                PMA_DailyProgress dailyProgress = _context.PMA_DailyProgresses.FirstOrDefault(dp => dp.ProgressID == progressId);

                if (dailyProgress == null)
                {
                    return NotFound("Daily progress update not found");
                }

                // Update daily progress update properties
                dailyProgress.ProgressDate = updatedDailyProgress.ProgressDate;
                dailyProgress.HoursWorked = updatedDailyProgress.HoursWorked;
                dailyProgress.Description = updatedDailyProgress.Description;

                _context.SaveChanges();

                return Ok(dailyProgress);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
