using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Data;

namespace PMA_Backend.TestingControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyMemoTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailyMemoTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint to create a new daily memo
        [HttpPost]
        public IActionResult CreateDailyMemo([FromBody] PMA_DailyMemo dailyMemo)
        {
            try
            {
                if (dailyMemo == null)
                {
                    return BadRequest("Invalid daily memo data");
                }

                // Add the new daily memo to the database
                _context.PMA_DailyMemos.Add(dailyMemo);
                _context.SaveChanges();

                return Ok(dailyMemo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to list daily memos for the authenticated user
        [HttpGet]
        public IActionResult ListUserDailyMemos()
        {
            try
            {
                // Retrieve a list of daily memos associated with the authenticated user
                // Assuming you have a way to identify the current user (e.g., from a JWT token)
                // Replace "userId" with the actual user identifier
                int userId = 1; // Replace with the actual user ID

                List<PMA_DailyMemo> userDailyMemos = _context.PMA_DailyMemos
                    .Where(dm => dm.UserID == userId)
                    .ToList();

                return Ok(userDailyMemos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to retrieve daily memo details by ID
        [HttpGet("{memoId}")]
        public IActionResult GetDailyMemoDetails(int memoId)
        {
            try
            {
                PMA_DailyMemo dailyMemo = _context.PMA_DailyMemos.FirstOrDefault(dm => dm.MemoID == memoId);

                if (dailyMemo == null)
                {
                    return NotFound("Daily memo not found");
                }

                return Ok(dailyMemo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to update daily memo details
        [HttpPut("{memoId}")]
        public IActionResult UpdateDailyMemo(int memoId, [FromBody] PMA_DailyMemo updatedDailyMemo)
        {
            try
            {
                PMA_DailyMemo dailyMemo = _context.PMA_DailyMemos.FirstOrDefault(dm => dm.MemoID == memoId);

                if (dailyMemo == null)
                {
                    return NotFound("Daily memo not found");
                }

                // Update daily memo properties
                dailyMemo.MemoDate = updatedDailyMemo.MemoDate;
                dailyMemo.MemoText = updatedDailyMemo.MemoText;

                _context.SaveChanges();

                return Ok(dailyMemo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
