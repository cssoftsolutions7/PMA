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
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task CreateTaskAsync(PMA_Task task)
        {
            _context.PMA_Tasks.Add(task);
            await SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await GetTaskByIdAsync(taskId);
            if (task != null)
            {
                _context.PMA_Tasks.Remove(task);
                await SaveChangesAsync();
            }
        }

        public async Task<PMA_Task> GetTaskByIdAsync(int taskId)
        {
            return await _context.PMA_Tasks.FindAsync(taskId);
        }

        public async Task<IEnumerable<PMA_Task>> GetTasksAsync()
        {
            return await _context.PMA_Tasks.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(PMA_Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
