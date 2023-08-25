using Microsoft.EntityFrameworkCore;
using PMA_Core.Models;
using PMA_Data;

namespace PMA_AuthAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<PMA_User> PMA_Users { get; set; }
        public DbSet<PMA_Project> PMA_Projects { get; set; }
        public DbSet<PMA_Task> PMA_Tasks { get; set; }
        public DbSet<PMA_DailyMemo> PMA_DailyMemos { get; set; }
        public DbSet<PMA_DailyProgress> PMA_DailyProgresses { get; set; }
        public DbSet<PMA_UserRole> PMA_UserRoles { get; set; }
        public DbSet<PMA_ProjectUserJunction> PMA_ProjectUserJunctions { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
