using PMA_Core.Models;
using PMA_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Services.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<PMA_Task>> GetTasksAsync()
        {
            return await _taskRepository.GetTasksAsync();
        }

        public async Task<PMA_Task> GetTaskByIdAsync(int taskId)
        {
            return await _taskRepository.GetTaskByIdAsync(taskId);
        }

        public async Task CreateTaskAsync(PMA_Task task)
        {
            await _taskRepository.CreateTaskAsync(task);
        }

        public async Task UpdateTaskAsync(int taskId, PMA_Task updatedTask)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(taskId);

            if (existingTask == null)
            {
                throw new ApplicationException("Task not found");
            }

            // Update task properties based on your requirements
            existingTask.TaskName = updatedTask.TaskName;
            existingTask.Status = updatedTask.Status;
            existingTask.Description = updatedTask.Description;
            existingTask.DueDate = updatedTask.DueDate;


            await _taskRepository.UpdateTaskAsync(existingTask);
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            await _taskRepository.DeleteTaskAsync(taskId);
        }

        public async Task SaveChangesAsync()
        {
            await _taskRepository.SaveChangesAsync();
        }
    }
}
