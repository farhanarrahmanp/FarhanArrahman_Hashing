using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using DTSMCC_WebApp.Models;
using DTSMCC_WebApp.Context;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTSMCC_WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        MyContext myContext;

        public DepartmentController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IActionResult Index()
        {
            var data = myContext.Departments.Include(x => x.Division).ToList(); // Join
            ViewData["Divisions"] = myContext.Divisions.ToList();

            return View(data);
        }

        // CREATE
        // GET
        public IActionResult Create()
        {
            ViewData["Divisions"] = myContext.Divisions.ToList();
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                myContext.Departments.Add(department); // row yang tereksekusi = executenonquery
                var result = myContext.SaveChanges(); // return int 1 = eksekusi, 0 = tidak tereksekusi
                if(result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // UPDATE
        // GET
        public IActionResult Update(int id)
        {
            var data = myContext.Departments.Include(x => x.Division).Where(x => x.Id == id).ToList(); // Join
            ViewData["Divisions"] = myContext.Divisions.ToList();

            return View(data);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Department department)
        {
            if (ModelState.IsValid)
            {
                myContext.Departments.Update(department); // row yang tereksekusi = executenonquery
                var result = myContext.SaveChanges(); // return int 1 = eksekusi, 0 = tidak tereksekusi
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // DELETE
        // GET
        public IActionResult Delete(int id)
        {
            var data = myContext.Departments.Include(x => x.Division).Where(x => x.Id == id).ToList(); // Join
            return View(data);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            if (ModelState.IsValid)
            {
                myContext.Departments.Remove(department); // row yang tereksekusi = executenonquery
                var result = myContext.SaveChanges(); // return int 1 = eksekusi, 0 = tidak tereksekusi
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { });
        }
    }
}
