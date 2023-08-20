using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Models
{
    public class PMA_Task
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        [MaxLength(100)]
        public string TaskName { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public string Status { get; set; }

        // Foreign key property to relate the task to a project
        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        // Navigation property for the project
        public PMA_Project? Project { get; set; }

        // Foreign key property to represent the assigned user
        [ForeignKey("AssignedUser")]
        public int? AssignedUserId { get; set; } // This field represents the assigned user's ID

        // Navigation property for the assigned user
        public PMA_User? AssignedUser { get; set; }

        // Navigation property for daily progress updates
        public ICollection<PMA_DailyProgress>? DailyProgresses { get; set; }
    }
}
