using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class AdminUser
    {
        public int Admin_ID { get; set; }
        public Admin Admin { get; set; }

        public int User_ID { get; set; }
        public User User { get; set; }

    }
}
