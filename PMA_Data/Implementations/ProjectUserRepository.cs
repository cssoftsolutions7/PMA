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
    public class ProjectUserRepository : IProjectUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AssignmentExistsAsync(int projectId, int userId)
        {
            return await _context.PMA_ProjectUserJunctions
            .AnyAsync(pu => pu.ProjectID == projectId && pu.UserID == userId);
        }

        public async Task CreateAssignmentAsync(int projectId, int userId)
        {
            var projectUser = new PMA_ProjectUserJunction
            {
                ProjectID = projectId,
                UserID = userId
            };

            _context.PMA_ProjectUserJunctions.Add(projectUser);
            await _context.SaveChangesAsync();
        }
    }
}
