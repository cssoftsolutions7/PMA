using PMA_Core.Models;
using PMA_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Services.Services
{
    public class UserRoleService
    {
        private readonly IUserRoleRepository _roleRepository;

        public UserRoleService(IUserRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<PMA_UserRole> CreateRoleAsync(PMA_UserRole role)
        {
            return await _roleRepository.CreateAsync(role);
        }

        public async Task<PMA_UserRole> GetRoleAsync(int roleID)
        {
            return await _roleRepository.GetAsync(roleID);
        }

        public async Task<IEnumerable<PMA_UserRole>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<PMA_UserRole> UpdateRoleAsync(PMA_UserRole role)
        {
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task<bool> DeleteRoleAsync(int roleID)
        {
            return await _roleRepository.DeleteAsync(roleID);
        }
    }
}
