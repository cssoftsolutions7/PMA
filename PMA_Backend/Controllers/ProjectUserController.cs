using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Services.Services;

namespace PMA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUserController : ControllerBase
    {
        private readonly ProjectUserService _projectUserService;

        public ProjectUserController(ProjectUserService projectUserService)
        {
            _projectUserService = projectUserService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignProjectToUser(int projectId, int userId)
        {
            var assignmentExists = await _projectUserService.AssignProjectToUserAsync(projectId, userId);

            if (assignmentExists)
            {
                return Conflict("Assignment already exists.");
            }

            return Ok("Project assigned to user successfully.");
        }
    }
}
