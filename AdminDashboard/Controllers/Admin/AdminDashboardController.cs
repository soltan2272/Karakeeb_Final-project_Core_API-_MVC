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
       

        
    }
}
