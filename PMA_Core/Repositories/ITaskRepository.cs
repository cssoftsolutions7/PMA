using PMA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<PMA_Task>> GetTasksAsync();
        Task<PMA_Task> GetTaskByIdAsync(int taskId);
        Task CreateTaskAsync(PMA_Task task);
        Task UpdateTaskAsync(PMA_Task task);
        Task DeleteTaskAsync(int taskId);
        Task SaveChangesAsync();
    }
}
