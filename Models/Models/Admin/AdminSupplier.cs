using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public class AdminSupplier
    {
        public int Admin_ID { get; set; }
        public Admin Admin { get; set; }
        public int Supplier_ID { get; set; }
        public Supplier Supplier { get; set; }
    }
}
