using Microsoft.EntityFrameworkCore;
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
