using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Context;
using Microsoft.EntityFrameworkCore;
using API.Repositories.Data;
using API.ViewModels;
using API.Repositories.Interface;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly IAccountRepository _iAccountRepository;

        public AccountController(IAccountRepository iAccountRepository)
        {
           _iAccountRepository = iAccountRepository;
        }

        /*
         * Login
         * Register (tambah sendiri)
         * Change Password (tambah sendiri)
         * Forgot Password (tambah sendiri)
         */

        // requirement login -> email & password
        // response login -> id karyawan, fullname, email, role (ini gak masuk evaluasi (jwt -> json web token))
        // -> LoginVM -> LoginViewModels

        [HttpPost]
        public IActionResult Login(Login login)
        {
            Console.WriteLine("Masuk login API...");
            var data = _iAccountRepository.Login(login);
            if (data != null)
                return Ok(new { message = "berhasil login", statusCode = 200, data = data });
            return BadRequest(new { message = "gagal login", statusCode = 400 });
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
