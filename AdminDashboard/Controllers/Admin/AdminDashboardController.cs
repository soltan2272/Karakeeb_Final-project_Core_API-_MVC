using AdminDashboard.Models;
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

namespace AdminDashboard.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ILogger<AdminDashboardController> _logger;

        public AdminDashboardController(ILogger<AdminDashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("localhost:44354/");
                using (HttpResponseMessage response = await client.GetAsync("api/User"))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    return View(responseContent);
                }
            }
           
        }
        [HttpPut]
        public async Task<IActionResult> editOrder(Order order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("localhost:44354/");
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

                using (HttpResponseMessage response = await client.PutAsync("api/User/editorder",content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    //return Ok(responseContent);
                    return View(responseContent);
                }
            }
        }

        [HttpDelete]
        public async Task<IActionResult> deleteOrders(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("localhost:44354/");
                using (HttpResponseMessage response = await client.DeleteAsync("api/User/deleteorder/"+ id))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    //return Ok(responseContent);
                    return View(responseContent);
                }
            }
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Detiles()
        {
            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
