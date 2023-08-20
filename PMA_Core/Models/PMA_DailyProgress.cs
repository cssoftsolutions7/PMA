using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Models
{
    public class PMA_DailyProgress
    {
        [Key]
        public int ProgressID { get; set; }

        public DateTime ProgressDate { get; set; }

        public double HoursWorked { get; set; }

        public string Description { get; set; }

        // Foreign key property to associate the progress update with a user
        [ForeignKey("User")]
        public int UserID { get; set; }

        // Foreign key property to associate the progress update with a task
        [ForeignKey("Task")]
        public int TaskID { get; set; }

        // Navigation property for the user
        public PMA_User? User { get; set; }

        // Navigation property for the task
        public PMA_Task? Task { get; set; }
    }
}
