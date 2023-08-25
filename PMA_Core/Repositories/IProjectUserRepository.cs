using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Repositories
{
    public interface IProjectUserRepository
    {
        Task<bool> AssignmentExistsAsync(int projectId, int userId);
        Task CreateAssignmentAsync(int projectId, int userId);
        Task DeleteAssignmentAsync(int projectId, int userId);
    }
}
