using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Services.Services;

namespace PMA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMA_Task>>> GetTasks()
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<PMA_Task>> GetTaskById(int taskId)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<PMA_Task>> CreateTask(PMA_Task task)
        {
            await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { taskId = task.TaskID }, task);
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, PMA_Task updatedTask)
        {
            try
            {
                await _taskService.UpdateTaskAsync(taskId, updatedTask);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                await _taskService.DeleteTaskAsync(taskId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
