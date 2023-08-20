using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.Models;
using PMA_Data;

namespace PMA_Backend.TestingControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagementTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectManagementTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint to create a new project
        [HttpPost]
        public IActionResult CreateProject([FromBody] PMA_Project project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest("Invalid project data");
                }

                // Add the new project to the database
                _context.PMA_Projects.Add(project);
                _context.SaveChanges();

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to list all projects for the authenticated user
        [HttpGet]
        public IActionResult ListUserProjects()
        {
            try
            {
                // Retrieve a list of projects associated with the authenticated user
                // Assuming you have a way to identify the current user (e.g., from a JWT token)
                // Replace "userId" with the actual user identifier
                int userId = 1; // Replace with the actual user ID

                List<PMA_Project> userProjects = _context.PMA_Projects
                    .Where(p => p.ProjectUsers.Any(up => up.UserID == userId))
                    .ToList();

                return Ok(userProjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to retrieve project details by ID
        [HttpGet("{projectId}")]
        public IActionResult GetProjectDetails(int projectId)
        {
            try
            {
                PMA_Project project = _context.PMA_Projects.FirstOrDefault(p => p.ProjectID == projectId);

                if (project == null)
                {
                    return NotFound("Project not found");
                }

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to update project details
        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] PMA_Project updatedProject)
        {
            try
            {
                PMA_Project project = _context.PMA_Projects.FirstOrDefault(p => p.ProjectID == projectId);

                if (project == null)
                {
                    return NotFound("Project not found");
                }

                // Update project properties
                project.ProjectName = updatedProject.ProjectName;
                project.Description = updatedProject.Description;
                project.StartDate = updatedProject.StartDate;
                project.EndDate = updatedProject.EndDate;

                _context.SaveChanges();

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
