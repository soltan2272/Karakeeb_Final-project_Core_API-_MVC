using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class AdminProduct
    {
        public int Admin_ID { get; set; }
        public Admin Admin { get; set; }

        public int Product_ID { get; set; }
        public Product Product { get; set; }
    }
}
