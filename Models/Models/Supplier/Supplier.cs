using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Supplier: BaseModel
    {
        public int SSN { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Credit_Card { get; set; }

        public IList<SupplierStore> SupllierStores { get; set; }

        public ICollection<Product> Products { get; set; }
        public IList<AdminSupplier> AdminSuppliers { get; set; }


    }
}
