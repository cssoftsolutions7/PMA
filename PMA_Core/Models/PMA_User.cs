using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Models
{
    public class PMA_User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? JoinDate { get; set; }

        public string? ProfileImageURL { get; set; }

        public bool? IsActive { get; set; }

        public string? Gender { get; set; }

        // Navigation property for UserRole 
        [ForeignKey("UserRole")]
        public int? RoleID { get; set; }
        public PMA_UserRole UserRole { get; set; }

        // Navigation property for Daily Memos
        public ICollection<PMA_DailyMemo>? DailyMemos { get; set; }

        // Navigation property for Daily Progresses
        public ICollection<PMA_DailyProgress>? DailyProgresses { get; set; }

        public ICollection<PMA_ProjectUserJunction> UserProjects { get; set; }
    }
}
