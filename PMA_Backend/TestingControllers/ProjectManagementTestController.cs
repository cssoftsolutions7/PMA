using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMA_Core.Models;
using PMA_Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using PMA_Core.DTOs.ProjectToUser;

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

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PMA_Project>>> GetProjectsByUser(int userId)
        {
            var user = await _context.PMA_Users
                .Include(u => u.UserProjects)
                .ThenInclude(up => up.Project)
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
            {
                return NotFound();
            }

            var projects = user.UserProjects.Select(up => up.Project).ToList();

            // Select only the relevant properties for the projects
            var simplifiedProjects = projects.Select(project => new
            {
                project.ProjectID,
                project.ProjectName,
                project.Description,
                project.StartDate,
                project.EndDate
            }).ToList();

            return Ok(simplifiedProjects);
        }

        [HttpGet("project/{projectId}/users")]
        public async Task<ActionResult<IEnumerable<ProjectToUserDTO>>> GetUsersAssignedToProject(int projectId)
        {
            var project = await _context.PMA_Projects
                .Include(p => p.ProjectUsers)
                .ThenInclude(j => j.User)
                .FirstOrDefaultAsync(p => p.ProjectID == projectId);

            if (project == null)
            {
                return NotFound("Project not found");
            }

            var users = project.ProjectUsers.Select(j => j.User)
                .Select(user => new ProjectToUserDTO
                {
                    UserID = user.UserID,
                    UserName = user.FirstName +" "+ user.LastName,
                    // Map other user properties here
                });

            return Ok(users);
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<ProjectToUserDTO>>> GetAllUsers()
        {
            var users = await _context.PMA_Users
                .Select(user => new ProjectToUserDTO
                {
                    UserID = user.UserID,
                    UserName = user.FirstName + " " + user.LastName,
                    // Map other user properties here
                })
                .ToListAsync();

            return Ok(users);
        }


    }
}
