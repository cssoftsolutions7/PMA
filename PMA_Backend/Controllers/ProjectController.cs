using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMA_Core.DTOs;
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


        [HttpGet("allprojects")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjectsTasks()
        {
            var projects = await _projectService.GetProjectsWithTasksAsync();
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
        public async Task<ActionResult<PMA_Project>> CreateProject(PMA_Project project)
        {
            await _projectService.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { projectId = project.ProjectID }, project);
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
        }


        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            await _projectService.DeleteProjectAsync(projectId);
            return NoContent();
        }
    }
}
