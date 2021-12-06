using AdminDashboard.Models;
using Final_Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AdminDashboard.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ILogger<AdminDashboardController> _logger;

        public AdminDashboardController(ILogger<AdminDashboardController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();

            }
            [HttpPost]
         /*   public IActionResult Login()
            {
               // return View();
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("product/AdminProducts");
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);

                    products = JsonConvert.DeserializeObject<IList<Product>>(jsonString);
                }

                return View(products);
        }*/
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Orders()
        { 
            return View();
        }
        public IActionResult Products()
        {

            IEnumerable<Product> products = null;
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var productcontroller = http.GetAsync("product/AdminProducts");
            productcontroller.Wait();
            var resltproduct = productcontroller.Result;
            if (resltproduct.IsSuccessStatusCode)
            {
                var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                tabel.Wait();
                var ser = tabel.Result.Data;
                string jsonString = JsonConvert.SerializeObject(ser);

                products = JsonConvert.DeserializeObject<IList<Product>>(jsonString);
            }
          
            return View(products);

          
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var response = http.DeleteAsync("product/Delete/" + id);
            response.Wait();

            return Redirect("/AdminDashboard/Products");
        }

        [HttpGet]
       public IActionResult Detiles(int id)
        {
            Product product = null;
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var productcontroller = http.GetAsync("product/AdminProduct/" + id);
            productcontroller.Wait();
            var resltproduct = productcontroller.Result;
            if (resltproduct.IsSuccessStatusCode)
            {

                var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                tabel.Wait();
                if(tabel.Result.ISuccessed==false)
                    return View("NotFound");

                var data = tabel.Result.Data;
                string jsonString = JsonConvert.SerializeObject(data);
                
                product = JsonConvert.DeserializeObject<Product>(jsonString);
            }

                return View(product);
        }

        [HttpGet]
        public IActionResult StoreProduct(IList<Product> StoreProduct)
        {
            return View(StoreProduct);
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult ChangePasswoed()
        {
            return View();
        }

        public IActionResult Suppliers()
        {
            return View();
        }


        public IActionResult Stores()
        {
            IEnumerable<Store> Stores = null;
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var storecontroller = http.GetAsync("product/stores");
            storecontroller.Wait();
            var resltstore = storecontroller.Result;
            if (resltstore.IsSuccessStatusCode)
            {
                var tabel = resltstore.Content.ReadAsAsync<ResultViewModel>();
                tabel.Wait();
                var serialiaze = tabel.Result.Data;
                string jsonString = JsonConvert.SerializeObject(serialiaze);

                Stores = JsonConvert.DeserializeObject<IList<Store>>(jsonString);
            }

            return View(Stores);

        }

        [HttpGet]
        public IActionResult DeletStore(int id)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var response = http.DeleteAsync("product/deleteStore/" + id);
            response.Wait();

            return Redirect("/AdminDashboard/Stores");
        }

        
        [HttpGet]
       public IActionResult StoreDetiles(int id)
        {
            Product product = null;
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var productcontroller = http.GetAsync("product/AdminProduct/" + id);
            productcontroller.Wait();
            var resltproduct = productcontroller.Result;
            if (resltproduct.IsSuccessStatusCode)
            {

                var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                tabel.Wait();
                if(tabel.Result.ISuccessed==false)
                    return View("NotFound");

                var data = tabel.Result.Data;
                string jsonString = JsonConvert.SerializeObject(data);
                
                product = JsonConvert.DeserializeObject<Product>(jsonString);
            }

                return View(product);
        }

        [HttpGet]
        public IActionResult DeleteProStore(int id)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var response = http.DeleteAsync("product/Delete/" + id);
            response.Wait();

            return Redirect("/AdminDashboard/StoreDetiles");
        }

        public IActionResult MyStore()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult DisplayContact(int id)
        {
            return View();
        }


        public IActionResult NotFound()
        {
            return View();
        }

        
    }
}
