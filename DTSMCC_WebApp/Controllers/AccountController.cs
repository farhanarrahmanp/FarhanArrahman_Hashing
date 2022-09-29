using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTSMCC_WebApp.Controllers
{
    public class AccountController : Controller
    {
        HttpClient HttpClient;
        string address;
        HttpClientHandler HttpClientHandler;

        public AccountController()
        {
            this.address = "https://localhost:1433/api/Account/"; // port lihat di /api/properties/launchsettings.json

            HttpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient = new HttpClient(HttpClientHandler)
            {
                BaseAddress = new Uri(address),
                Timeout = TimeSpan.FromMinutes(30)
            };
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            Console.WriteLine("Masuk halaman login...");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            Console.WriteLine("Berhasil StringContent!");
            var result = HttpClient.PostAsync(address, content).Result;
            Console.WriteLine("Berhasil var result!");
            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("result Success!");
                var data = JsonConvert.DeserializeObject<ResponseClient>(await result.Content.ReadAsStringAsync());
                Console.WriteLine("Berhasil JsonConvert!");
                HttpContext.Session.SetString("Role", data.data.Role);
                Console.WriteLine("Berhasil SetStringRole!");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
