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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<PMA_UserRole> CreateAsync(PMA_UserRole role)
        {
            _context.PMA_UserRoles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteAsync(int roleID)
        {
            var role = await _context.PMA_UserRoles.FindAsync(roleID);
            if (role == null)
                return false;

            _context.PMA_UserRoles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PMA_UserRole>> GetAllAsync()
        {
            return await _context.PMA_UserRoles.ToListAsync();
        }

        public async Task<PMA_UserRole> GetAsync(int roleID)
        {
            return await _context.PMA_UserRoles.FindAsync(roleID);
        }

        public async Task<PMA_UserRole> UpdateAsync(PMA_UserRole role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return role;
        }
    }
}
