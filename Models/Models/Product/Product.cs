using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Product: BaseModel
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Rate { get; set; }
        public int CurrentSupplierID { get; set; }
        public Supplier supplier { get; set; }
        public IList<StoreProduct> StoresProducts { get; set; }
        public IList<ProductOffer> ProductOffers { get; set; }
        public IList<ProductFeedback> productFeedbacks { get; set; }
        public int CurrentCategoryID { get; set; }
        public Category category { get; set; }
        public IList<ProductOrder> productOrders { get; set; }

        public IList<AdminProduct> AdminProducts { get; set; }

      
    }
}
