using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Data;

namespace PMA_Backend.TestingControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskManagementTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint to create a new task
        [HttpPost]
        public IActionResult CreateTask([FromBody] PMA_Task task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Invalid task data");
                }

                // Add the new task to the database
                _context.PMA_Tasks.Add(task);
                _context.SaveChanges();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to list tasks for the authenticated user
        [HttpGet]
        public IActionResult ListUserTasks()
        {
            try
            {
                // Retrieve a list of tasks assigned to the authenticated user
                // Assuming you have a way to identify the current user (e.g., from a JWT token)
                // Replace "userId" with the actual user identifier
                int userId = 1; // Replace with the actual user ID

                List<PMA_Task> userTasks = _context.PMA_Tasks
                    .Where(t => t.AssignedUserId == userId)
                    .ToList();

                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to retrieve task details by ID
        [HttpGet("{taskId}")]
        public IActionResult GetTaskDetails(int taskId)
        {
            try
            {
                PMA_Task task = _context.PMA_Tasks.FirstOrDefault(t => t.TaskID == taskId);

                if (task == null)
                {
                    return NotFound("Task not found");
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to update task details
        [HttpPut("{taskId}")]
        public IActionResult UpdateTask(int taskId, [FromBody] PMA_Task updatedTask)
        {
            try
            {
                PMA_Task task = _context.PMA_Tasks.FirstOrDefault(t => t.TaskID == taskId);

                if (task == null)
                {
                    return NotFound("Task not found");
                }

                // Update task properties
                task.TaskName = updatedTask.TaskName;
                task.Description = updatedTask.Description;
                task.DueDate = updatedTask.DueDate;
                task.Status = updatedTask.Status;

                _context.SaveChanges();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to assign a task to a user
        [HttpPost("{taskId}/assign")]
        public IActionResult AssignTask(int taskId, [FromBody] int assignedUserId)
        {
            try
            {
                PMA_Task task = _context.PMA_Tasks.FirstOrDefault(t => t.TaskID == taskId);

                if (task == null)
                {
                    return NotFound("Task not found");
                }

                // Update the assigned user ID for the task
                task.AssignedUserId = assignedUserId;

                _context.SaveChanges();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
