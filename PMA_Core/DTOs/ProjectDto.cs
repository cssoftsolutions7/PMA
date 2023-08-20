using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.DTOs
{
    public class ProjectDto
    {
        [Key]
        public int ProjectID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
