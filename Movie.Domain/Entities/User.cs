using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public bool Role { get; set; }
    }
}
