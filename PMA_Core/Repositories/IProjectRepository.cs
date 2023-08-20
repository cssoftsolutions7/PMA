using PMA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<PMA_Project>> GetAllProjectsAsync();
        Task<PMA_Project> GetProjectByIdAsync(int projectId);
        Task CreateProjectAsync(PMA_Project project);
        Task UpdateProjectAsync(PMA_Project project);
        Task DeleteProjectAsync(int projectId);
        Task SaveChangesAsync();
    }
}
