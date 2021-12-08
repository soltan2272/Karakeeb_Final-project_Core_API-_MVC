using AdminDashboard.Models;
using Final_Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;
using PagedList;
using PagedList.Mvc;
using ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Models.Models;
using Microsoft.AspNetCore.Authorization;

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


            //[HttpPost]
         /*   public IActionResult Login()

       
        [HttpPost]
           public IActionResult Logincheck(LoginModel logininfo)
           {
            // return View()
            var jsondata = JsonConvert.SerializeObject(logininfo);
            var data = new StringContent(jsondata, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
               http.BaseAddress = new Uri(Global.API);
               var AdminLogin = http.PostAsync("Admin/login",data);
                AdminLogin.Wait();
               var resltadmin = AdminLogin.Result;
                string result = null;
               if (resltadmin.IsSuccessStatusCode)
               {
                   var tabel = resltadmin.Content.ReadAsAsync<AuthModel>();
                   tabel.Wait();
                   var ser = tabel.Result.Token;
                // string jsonString = JsonConvert.SerializeObject(ser);

                // result = JsonConvert.DeserializeObject<string>(jsonString);
                result = ser;
                HttpContext.Response.Cookies.Append("UserToken", result);
                return Redirect("/AdminDashboard/Index");
                


            }
               else
            {
                return Redirect("/AdminDashboard/Login");
            }
            
        }

      
        public IActionResult Users()
        {
            if (HttpContext.Request.Cookies["UserToken"]!= "")
            {
                IEnumerable<User> users = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var userscontroller = http.GetAsync("User/getusers");
                userscontroller.Wait();
                var resltuser = userscontroller.Result;
                if (resltuser.IsSuccessStatusCode)
                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);

                    users = JsonConvert.DeserializeObject<IList<User>>(jsonString);
                }

                return View(users);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }

        public IActionResult Suppliers()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<User> Sellers = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var sellerscontroller = http.GetAsync("Seller/getsellers");
                sellerscontroller.Wait();
                var resltuser = sellerscontroller.Result;
                if (resltuser.IsSuccessStatusCode)
                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);

                    Sellers = JsonConvert.DeserializeObject<IList<User>>(jsonString);
                }

                return View(Sellers);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }
        public IActionResult Admins()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "" )
            {
                IEnumerable<User> Admins = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var adminscontroller = http.GetAsync("Admin/getadmins");
                adminscontroller.Wait();
                var resltuser = adminscontroller.Result;
                if (resltuser.IsSuccessStatusCode)
                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);

                    Admins = JsonConvert.DeserializeObject<IList<User>>(jsonString);
                }

                return View(Admins);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
            
        }


        public IActionResult DeleteUser(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("User/deleteuser/" + id);
                response.Wait();
                return Redirect("/AdminDashboard/Users");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult DeleteSeller(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("Seller/deleteseller/" + id);
                response.Wait();
                return Redirect("/AdminDashboard/Suppliers");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpGet]

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

       
        public IActionResult Orders()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult Products()
        {

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

            var productcontroller = http.GetAsync("product/AdminProducts/"+page);
            productcontroller.Wait();
            var resltproduct = productcontroller.Result;
            if (resltproduct.IsSuccessStatusCode)

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

                    products = JsonConvert.DeserializeObject<IList<Product>>(jsonString);
                }

                return View(products);
            }

            else
            {
                return Redirect("/AdminDashboard/Login");
            }

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

            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/Delete/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/Products");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
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

            if (HttpContext.Request.Cookies["UserToken"] != "")
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
                    if (tabel.Result.ISuccessed == false)
                        return View("NotFound");

                    var data = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(data);

                    product = JsonConvert.DeserializeObject<Product>(jsonString);
                }

                return View(product);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

       /* public IActionResult Users()

        {
            return View();

        }



        public async Task<IActionResult> productMyStore()

        public IActionResult Admins()

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



        }*/

        public IActionResult AddProduct()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        public IActionResult Profile()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult ChangePasswoed()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult logout()
        {
           

            HttpContext.Response.Cookies.Append("UserToken","");
            return Redirect("/AdminDashboard/Login");
        }


        /*  public IActionResult Suppliers()
          {
              return View();
          }*/


        public IActionResult Stores()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
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
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpGet]
        public IActionResult DeletStore(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/deleteStore/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/Stores");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        
        [HttpGet]
       public IActionResult StoreDetiles(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
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
                    if (tabel.Result.ISuccessed == false)
                        return View("NotFound");

                    var data = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(data);

                    product = JsonConvert.DeserializeObject<Product>(jsonString);
                }

                return View(product);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
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
        //public async Task<IActionResult> addStore(Store s)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.Timeout = TimeSpan.FromSeconds(60);
        //    client.BaseAddress = new Uri("https://localhost:44354/");
        //        var data = new
        //        {
        //            ID = s.ID,
        //            Name = s.Name,
        //            Address = s.Address,
        //            Phone = s.Phone
        //        };

        //        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        //        using (HttpResponseMessage response = await client.PostAsync("api/Product/addStore", content))
        //        {
        //            var responseContent = response.Content.ReadAsStringAsync().Result;
        //            response.EnsureSuccessStatusCode();

        //            return Redirect("/AdminDashboard/Stores");
        //        }
        //}

        public IActionResult addstore()
        {
            return View();
        }

        public IActionResult DeleteProStore(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/Delete/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/StoreDetiles");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        //public IActionResult MyStore()
        //{
        //    if (HttpContext.Request.Cookies["UserToken"] != "")
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return Redirect("/AdminDashboard/Login");
        //    }

        //}

        public IActionResult ContactUs()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult DisplayContact(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }


        public IActionResult NotFound()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        
    }
}
