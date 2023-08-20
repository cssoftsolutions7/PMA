using Microsoft.EntityFrameworkCore;
using PMA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PMA_User> PMA_Users { get; set; }
        public DbSet<PMA_Project> PMA_Projects { get; set; }
        public DbSet<PMA_Task> PMA_Tasks { get; set; }
        public DbSet<PMA_DailyMemo> PMA_DailyMemos { get; set; }
        public DbSet<PMA_DailyProgress> PMA_DailyProgresses { get; set; }
        public DbSet<PMA_UserRole> PMA_UserRoles { get; set; }
        public DbSet<PMA_ProjectUserJunction> PMA_ProjectUserJunctions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
