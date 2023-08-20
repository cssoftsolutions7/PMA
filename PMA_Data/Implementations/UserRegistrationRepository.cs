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
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly ApplicationDbContext _context; // Replace with your database context

        public UserRegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PMA_User> CreateUserAsync(PMA_User user)
        {
            try
            {
                _context.PMA_Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"While Saving New User Error Occured {ex.Message}");
                return null;
            }

        }

        public async Task<PMA_User> GetUserByEmailAsync(string email)
        {
            return await _context.PMA_Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
