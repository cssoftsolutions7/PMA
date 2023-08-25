using Microsoft.EntityFrameworkCore;
using PMA_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Services.Services
{
    public class ProjectUserService
    {
        private readonly IProjectUserRepository _projectUserRepository;

        public ProjectUserService(IProjectUserRepository projectUserRepository)
        {
            _projectUserRepository = projectUserRepository;
        }

        public async Task<bool> AssignProjectToUserAsync(int projectId, int userId)
        {
            var assignmentExists = await _projectUserRepository.AssignmentExistsAsync(projectId, userId);

            if (assignmentExists)
            {
                return true; // Assignment already exists
            }

            await _projectUserRepository.CreateAssignmentAsync(projectId, userId);

            return false; // Assignment created successfully
        }

        public async Task<bool> RemoveProjectAssignmentAsync(int projectId, int userId)
        {
            var assignmentExists = await _projectUserRepository.AssignmentExistsAsync(projectId, userId);

            if (assignmentExists)
            {
                await _projectUserRepository.DeleteAssignmentAsync(projectId, userId);
                return true; // Assignment deleted successfully
            }

            return false; // Assignment doesn't exist
        }
    }
}
