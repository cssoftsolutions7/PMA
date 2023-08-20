using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Models
{
    public class PMA_Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<PMA_ProjectUserJunction>? ProjectUsers { get; set; }

        // Navigation property for tasks
        public ICollection<PMA_Task>? Tasks { get; set; }
    }
}
