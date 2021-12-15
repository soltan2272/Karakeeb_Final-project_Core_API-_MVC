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
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Models.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

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



        //[HttpPost]
        // public IActionResult Login()



        [HttpPost]
        public IActionResult Logincheck(LoginModel logininfo)
        {
            
            bool isAdmin = false;
            var jsondata = JsonConvert.SerializeObject(logininfo);
            var data = new StringContent(jsondata, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var AdminLogin = http.PostAsync("Admin/login", data);
            AdminLogin.Wait();
            var resltadmin = AdminLogin.Result;
            string result = null;
            if (resltadmin.IsSuccessStatusCode)
            {
                var tabel = resltadmin.Content.ReadAsAsync<AuthModel>();
                tabel.Wait();
                var ser = tabel.Result.Token;
                var usrid = tabel.Result.User_ID;
                var usrname = tabel.Result.Username;
                var Roles = tabel.Result.Roles;
                foreach(string role in Roles)
                {
                    if(role=="Admin")
                    {
                        isAdmin = true;
                    }
                }
               if(isAdmin==false)
                {
                    return Redirect("/AdminDashboard/Login");
                }
                result = ser;
                HttpContext.Response.Cookies.Append("UserToken", result);
                HttpContext.Response.Cookies.Append("UserID", usrid.ToString());
                HttpContext.Response.Cookies.Append("UserName", usrname);
                ViewData["UserName"] = tabel.Result.Username;
                return Redirect("/AdminDashboard/Index");

            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }

        public IActionResult Users(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
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
                return View(users.ToPagedList((p ?? 1), 7));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }



        }

        public IActionResult Suppliers(int? p=1)
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

                return View(Sellers.ToPagedList((p ?? 1), 7));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }
        public IActionResult Admins(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
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
                return View(Admins.ToPagedList((p ?? 1), 7));
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

        public IActionResult Orders(int ? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
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

                return View(Orders.ToPagedList((p ?? 1), 7));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> editOrder(Order order)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
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
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }

        
        [HttpDelete]
        public async Task<IActionResult> deleteOrders(int id)
        {
            if (id == 0)
                id = 6;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = TimeSpan.FromSeconds(60);
            client.BaseAddress = new Uri("https://localhost:44354/");

            using (HttpResponseMessage response = await client.DeleteAsync("api/User/deleteorder/" +id))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
               // response.EnsureSuccessStatusCode();
                return Redirect("/AdminDashboard/Orders");
            }

        }

        [HttpGet]
        public IActionResult Products(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<Product> products = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("product/AdminProducts/");
                //var productcontroller = http.GetAsync("Product/AdminProducts");

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
                return View(products.ToPagedList((p ?? 1), 7));
            }

            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
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

        [HttpGet]
        public IActionResult Detiles(int id)
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

  
        [HttpPost]
        public async Task<IActionResult> AddProduct(InsertProductViewModel product)
        {
            
            
            if (HttpContext.Request.Cookies["UserToken"] != "")
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
                    imgspathes = product.imgspathes,
                    Rate = product.Rate,
                    Description = product.Description,
                    CurrentSupplierID = product.CurrentSupplierID,
                    CurrentCategoryID = product.CurrentCategoryID
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync("api/Product/addProduct", content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();

                    return Redirect("/AdminDashboard/productMyStore");
                }
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        //public async Task<IActionResult> editProduct(int id, Product product)
        //{
        public async Task<IActionResult> editProduct(Product product)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = TimeSpan.FromSeconds(60);
                client.BaseAddress = new Uri("https://localhost:44354/");
                var data = new
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    //Image = product.Image,
                    Rate = product.Rate,
                    Description = product.Description,
                    CurrentSupplierID = product.CurrentSupplierID,
                    CurrentCategoryID = product.CurrentCategoryID,
                    ID = product.ID
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PutAsync("api/Product/editproduct/"+product.ID, content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    return Redirect("/AdminDashboard/Products");

                }
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }

        public IActionResult editProduct()
        {
            return View();
        }

        public IActionResult AddAdmin()
        {
            return View();

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



            HttpContext.Response.Cookies.Append("UserToken", "");
            return Redirect("/AdminDashboard/Login");
        }

        public IActionResult Stores(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<User> Sellers = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var sellerscontroller = http.GetAsync("Seller/getsellers");
                sellerscontroller.Wait();
                var resltuser = sellerscontroller.Result;

                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);
                    Sellers = JsonConvert.DeserializeObject<IList<User>>(jsonString);

                }
                return View(ps);

                return View(Sellers.ToPagedList((p ?? 1), 7));

            }
            else
            {
                return Redirect("/AdminDashboard/Login");

               
            }
            //if (HttpContext.Request.Cookies["UserToken"] != "")
            //{
            //    IEnumerable<Store> Stores = null;
            //    HttpClient http = new HttpClient();
            //    http.BaseAddress = new Uri(Global.API);
            //    var storecontroller = http.GetAsync("product/stores");
            //    storecontroller.Wait();
            //    var resltstore = storecontroller.Result;
            //    if (resltstore.IsSuccessStatusCode)
            //    {
            //        var tabel = resltstore.Content.ReadAsAsync<ResultViewModel>();
            //        tabel.Wait();
            //        var serialiaze = tabel.Result.Data;
            //        string jsonString = JsonConvert.SerializeObject(serialiaze);



            //        Stores = JsonConvert.DeserializeObject<IList<Store>>(jsonString);
            //    }



            //    return View(Stores);
            //}
            //else
            //{
            //    return Redirect("/AdminDashboard/Login");
            //}
        }
        [HttpGet]
        public IActionResult StoreProduct(int id,int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<Product> products = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("search/Seller/" + id);
                //var productcontroller = http.GetAsync("Product/AdminProducts");
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
                return View(products.ToPagedList((p ?? 1), 7));
            }

            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }


       

        [HttpGet]
        public IActionResult StoreDetiles(int id )
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
        public IActionResult DeleteProStore(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/Delete/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/Stores");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }


        public  IActionResult MyStore()
        {
            return View();
        }
        public IActionResult ContactUs(int? p=1)

               
        






        public IActionResult ContactUs()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<ContactUs> ContactUs = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var Usercontroller = http.GetAsync("User/getContactUs");
                Usercontroller.Wait();
                var resltproduct = Usercontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);


                    ContactUs = JsonConvert.DeserializeObject<IList<ContactUs>>(jsonString);
                }
                    return View(ContactUs.ToPagedList((p ?? 1), 7));
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