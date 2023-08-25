using Microsoft.EntityFrameworkCore;
using PMA_Core.DTOs;
using PMA_Core.Models;
using PMA_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Data.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateProjectAsync(PMA_Project project)
        {
            _context.PMA_Projects.Add(project);
            await SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await GetProjectByIdAsync(projectId);
            if (project != null)
            {
                _context.PMA_Projects.Remove(project);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PMA_Project>> GetAllProjectsAsync()
        {
            return await _context.PMA_Projects.ToListAsync();
            //return await _context.PMA_Projects.Include(p => p.Tasks).ToListAsync();
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjectsWithTasksAsync()
        {
            return await _context.PMA_Projects
                .Include(p => p.Tasks)
                .Select(p => new ProjectDTO
                {
                    ProjectID = p.ProjectID,
                    ProjectName = p.ProjectName,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Tasks = p.Tasks.Select(t => new TaskDTO
                    {
                        TaskID = t.TaskID,
                        TaskName = t.TaskName,
                        Description = t.Description,
                        DueDate = t.DueDate,
                        Status = t.Status
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<PMA_Project> GetProjectByIdAsync(int projectId)
        {
            return await _context.PMA_Projects.FindAsync(projectId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(PMA_Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
