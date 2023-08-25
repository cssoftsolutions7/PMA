using PMA_Core.DTOs;
using PMA_Core.Models;
using PMA_Core.Repositories;
using PMA_Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Services.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<IEnumerable<PMA_Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllProjectsAsync();

        }
        public async Task<IEnumerable<ProjectDTO>> GetProjectsWithTasksAsync()
        {
            return await _projectRepository.GetAllProjectsWithTasksAsync();
        }

        public async Task<PMA_Project> GetProjectByIdAsync(int projectId)
        {
            return await _projectRepository.GetProjectByIdAsync(projectId);
        }

        public async Task CreateProjectAsync(PMA_Project project)
        {
            await _projectRepository.CreateProjectAsync(project);
        }

        public async Task UpdateProjectAsync(int projectId, PMA_Project updatedProject)
        {
            var existingProject = await _projectRepository.GetProjectByIdAsync(projectId);

            if (existingProject == null)
            {
                throw new ApplicationException("Project not found");
            }

            // Update project properties based on your requirements
            existingProject.ProjectName = updatedProject.ProjectName;
            existingProject.Description = updatedProject.Description;
            existingProject.StartDate = updatedProject.StartDate;
            existingProject.EndDate = updatedProject.EndDate;

            await _projectRepository.UpdateProjectAsync(existingProject);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            await _projectRepository.DeleteProjectAsync(projectId);
        }

        public async Task SaveChangesAsync()
        {
            await _projectRepository.SaveChangesAsync();
        }
    }
}
