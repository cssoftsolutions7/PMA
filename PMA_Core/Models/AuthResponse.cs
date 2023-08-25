using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.Models
{
    public class AuthResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public string Role { get; set; }
        public int ExpiresIn { get; set; }
    }
}
