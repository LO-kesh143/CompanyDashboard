using CompanyDashboard.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompanyDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly System.Net.Http.HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7777/");
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Dashboard/departments");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<List<Department>>();
                return View("Index", responseData);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
