using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Admin: BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { set; get; }

        public int CurrentContactID { get; set; }
        public Contact Contact { get; set; }
        public IList<AdminUser> AdminUsers { get; set; }
        public IList<AdminProduct> AdminProducts { get; set; }
        public IList<AdminStore> AdminStores { get; set; }

        public IList<AdminSupplier> AdminSuppliers { get; set; }


    }
}
