using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class SupplierStore
    {
        public int Supllier_ID { set; get; }
        public Supplier supplier { set; get; }

        public int Store_ID { set; get; }
        public Store store { set; get; }
    }
}
