using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Models
{
    public class PMA_DailyMemo
    {
        [Key]
        public int MemoID { get; set; }

        public DateTime MemoDate { get; set; }

        public string MemoText { get; set; }

        // Foreign key property to associate the memo with a user
        [ForeignKey("User")]
        public int UserID { get; set; }

        // Navigation property for the user
        public PMA_User? User { get; set; }
    }
}
