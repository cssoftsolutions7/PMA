using PMA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Repositories
{
    public interface IUserRoleRepository
    {
        Task<PMA_UserRole> CreateAsync(PMA_UserRole role);
        Task<PMA_UserRole> GetAsync(int roleID);
        Task<IEnumerable<PMA_UserRole>> GetAllAsync();
        Task<PMA_UserRole> UpdateAsync(PMA_UserRole role);
        Task<bool> DeleteAsync(int roleID);
    }
}
