using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTSMCC_WebApp.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: /<controller>/
        public new IActionResult Unauthorized()
        {
            return View();
        }
    }
}
