using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMA_Core.Models;
using PMA_Services.Services;

namespace PMA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMA_Project>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<PMA_Project>> GetProjectById(int projectId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(PMA_Project project)
        {
            try
            {
                await _projectService.CreateProjectAsync(project);
                return CreatedAtAction(nameof(GetProjectById), new { projectId = project.ProjectID }, project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, PMA_Project updatedProject)
        {
            try
            {
                await _projectService.UpdateProjectAsync(projectId, updatedProject);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            try
            {
                await _projectService.DeleteProjectAsync(projectId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPost("assign")]
        //public IActionResult AssignProjectToUser(int projectId, int userId)
        //{
        //    // Check if the project and user exist
        //    var project = _dbContext.Projects.Find(projectId);
        //    var user = _dbContext.Users.Find(userId);

        //    if (project == null || user == null)
        //    {
        //        return NotFound();
        //    }

        //    // Check if the assignment already exists
        //    var existingAssignment = _dbContext.ProjectUsers
        //        .FirstOrDefault(pu => pu.ProjectID == projectId && pu.UserID == userId);

        //    if (existingAssignment != null)
        //    {
        //        return Conflict("Assignment already exists.");
        //    }

        //    // Create a new assignment
        //    var projectUser = new PMA_ProjectUser
        //    {
        //        Project = project,
        //        User = user
        //    };

        //    _dbContext.ProjectUsers.Add(projectUser);
        //    _dbContext.SaveChanges();

        //    return Ok("Project assigned to user successfully.");
        //}
    }
}
