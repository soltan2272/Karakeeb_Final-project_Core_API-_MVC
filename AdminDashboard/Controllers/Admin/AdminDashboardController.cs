using AdminDashboard.Models;
using Final_Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        /*    [HttpPost]
          public IActionResult Login()
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

        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            IEnumerable<Order> Orders = null;
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var ordercontroller = http.GetAsync("User");
            ordercontroller.Wait();
            var resltorder = ordercontroller.Result;
            if (resltorder.IsSuccessStatusCode)
            {
                var tabel = resltorder.Content.ReadAsAsync<ResultViewModel>();
                tabel.Wait();
                var serialiaze = tabel.Result.Data;
                string jsonString = JsonConvert.SerializeObject(serialiaze);

                Orders = JsonConvert.DeserializeObject<IList<Order>>(jsonString);
            }

            return View(Orders);


            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("localhost:44354/");
            //    using (HttpResponseMessage response = await client.GetAsync("api/User"))
            //    {
            //        var responseContent = response.Content.ReadAsStringAsync().Result;
            //        response.EnsureSuccessStatusCode();
            //        return View(responseContent);
            //    }
            //}
        }

        [HttpPut]
        public async Task<IActionResult> editOrder(Order order)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(60);
            client.BaseAddress = new Uri("https://localhost:44354/");
            var data = new
                {
                    Quantity = order.Quantity,
                    Order_Date = order.Order_Date,
                    CurrentCourierID = order.CurrentCourierID,
                    CurrentPaymentID = order.CurrentPaymentID,
                    CurrentUserID = order.CurrentUserID,
                    Delivery_Status = order.Delivery_Status
                };

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PutAsync("api/User/editorder", content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    return Redirect("/AdminDashboard/Orders");
             
                }   
        }

        [HttpDelete]
        public async Task<IActionResult> deleteOrders(int id)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(60);
            client.BaseAddress = new Uri("https://localhost:44354/");

            using (HttpResponseMessage response = await client.DeleteAsync("api/User/deleteorder/" + id))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
                //return View(responseContent);
                return Redirect("/AdminDashboard/Orders");
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> addProduct(Product product)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(60);
            client.BaseAddress = new Uri("https://localhost:44354/");
            var data = new
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                Rate = product.Rate,
                Description = product.Description,
                CurrentSupplierID = product.CurrentSupplierID,
                CurrentCategoryID = product.CurrentCategoryID
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PostAsync("api/Product/addStore", content))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();

                return Redirect("/AdminDashboard/productMyStore");
            }
        }
        public IActionResult addProduct()
        {
            return View();
        }


        public async Task<IActionResult> productMyStore()
        {
            IEnumerable<Product> products = null;
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var productcontroller = http.GetAsync("product/GetProductBySupplierID/"+2);
            productcontroller.Wait();
            var resltstore = productcontroller.Result;
            if (resltstore.IsSuccessStatusCode)
            {
                var tabel = resltstore.Content.ReadAsAsync<ResultViewModel>();
                tabel.Wait();
                var serialiaze = tabel.Result.Data;
                string jsonString = JsonConvert.SerializeObject(serialiaze);

                products = JsonConvert.DeserializeObject<IList<Product>>(jsonString);
            }

            return View(products);
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
        public IActionResult MyStore()
        {
            //IEnumerable<Store> Stores = null;
            Store Stores = null;
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var storecontroller = http.GetAsync("product/storesById/" + 3);
            storecontroller.Wait();
            var resltstore = storecontroller.Result;
            if (resltstore.IsSuccessStatusCode)
            {
                var tabel = resltstore.Content.ReadAsAsync<ResultViewModel>();
                tabel.Wait();
                var serialiaze = tabel.Result.Data;
                string jsonString = JsonConvert.SerializeObject(serialiaze);

               // Stores = JsonConvert.DeserializeObject<IList<Store>>(jsonString);
                Stores = JsonConvert.DeserializeObject<Store>(jsonString);
            }

            return View(Stores);
        }
        [HttpPost]
        public async Task<IActionResult> addStore(Store s)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(60);
            client.BaseAddress = new Uri("https://localhost:44354/");
                var data = new
                {
                    ID = s.ID,
                    Name = s.Name,
                    Address = s.Address,
                    Phone = s.Phone
                };

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync("api/Product/addStore", content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();

                    return Redirect("/AdminDashboard/Stores");
                }
        }

        public IActionResult addstore()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

       
        public IActionResult NotFound()
        {
            return View();
        }

        
    }
}
