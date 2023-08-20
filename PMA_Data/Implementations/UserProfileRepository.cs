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
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PMA_User> GetUserByIdAsync(int userId)
        {
            return await _context.PMA_Users.FindAsync(userId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(PMA_User user)
        {
            // Attach the user to the context to mark it as modified
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserID))
                {
                    throw new ApplicationException("User not found");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool UserExists(int userId)
        {
            return _context.PMA_Users.Any(e => e.UserID == userId);
        }
    }
}
