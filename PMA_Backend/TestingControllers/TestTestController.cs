using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMA_Core.Models;
using PMA_Data;

namespace PMA_Backend.TestingControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TestTestController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("user/{userId}/project/{projectId}/tasks")]
        public async Task<ActionResult<IEnumerable<PMA_Task>>> GetTasksForUserAndProject(int userId, int projectId)
        {
            // Ensure that the user is assigned to the project
            bool userAssignedToProject = await _context.PMA_ProjectUserJunctions
                .AnyAsync(pu => pu.UserID == userId && pu.ProjectID == projectId);

            if (!userAssignedToProject)
            {
                return NotFound("User is not assigned to this project.");
            }

            // Retrieve tasks for the specified project
            var tasks = await _context.PMA_Tasks
                .Where(t => t.ProjectID == projectId)
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("project/{projectId}/tasks")]
        public async Task<ActionResult<IEnumerable<PMA_Task>>> GetTasksForProject(int projectId)
        {
            // Retrieve tasks for the specified project
            var tasks = await _context.PMA_Tasks
                .Where(t => t.ProjectID == projectId)
                .ToListAsync();

            if (tasks == null)
            {
                return NotFound("Project not found or it has no tasks.");
            }

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<PMA_DailyProgress>> SaveDailyProgress(PMA_DailyProgress dailyProgress)
        {
            // Ensure that the specified task exists and is associated with the user
            var task = await _context.PMA_Tasks.FirstOrDefaultAsync(t => t.TaskID == dailyProgress.TaskID && t.Project.ProjectUsers.Any(pu => pu.UserID == dailyProgress.UserID));

            if (task == null)
            {
                return NotFound("Task not found or it is not associated with the user.");
            }

            _context.PMA_DailyProgresses.Add(dailyProgress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailyProgress", new { id = dailyProgress.ProgressID }, dailyProgress);
        }

        //To get all names of projects [For dropdownlist]
        [HttpGet("GetProjectName")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _context.PMA_Projects
                .Select(p => new { p.ProjectID, p.ProjectName })
                .ToListAsync();

            return Ok(projects);
        }
    }
}
