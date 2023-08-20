using PMA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Repositories
{
    public interface IUserRegistrationRepository
    {
        Task<PMA_User> GetUserByEmailAsync(string email);
        Task<PMA_User> CreateUserAsync(PMA_User user);
        // Add other user-related repository methods as needed
    }
}
