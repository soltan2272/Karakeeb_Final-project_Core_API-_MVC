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

            return View("orders");
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

        public IActionResult Users()
        {
            return View();
        }
       
        public IActionResult Suppliers()
        {
            return View();
        }
        public IActionResult Reports()
        {
            return View();
        }
        public IActionResult Integrations()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
