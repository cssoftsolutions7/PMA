using PMA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Repositories
{
    public interface IUserProfileRepository
    {
        Task<PMA_User> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(PMA_User user);
        Task SaveChangesAsync();
    }
}
