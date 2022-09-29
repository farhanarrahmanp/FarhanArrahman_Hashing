using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Context;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //public class DepartmentController : Controller
    public class DepartmentController : ControllerBase
    {
        MyContext myContext;

        public DepartmentController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // READ
        [HttpGet]
        public IActionResult Get()
        {
            var data = myContext.Departments.ToList();
            if (data.Count == 0)
                return Ok(new { message = "sukses mengambil data", statusCode = 200, data = "null" });
            return Ok(new { message = "sukses mengambil data", statusCode = 200, data = data });

            //return BadRequest(new { message = "sukses mengambil data", statusCode = 200, data = data });
            //return NotFound(new { message = "sukses mengambil data", statusCode = 200, data = data });
            //return Unauthorized(new { message = "sukses mengambil data", statusCode = 200, data = data });
            //return Forbid(new { message = "sukses mengambil data", statusCode = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = myContext.Departments.Find(id);
            if (data == null)
                return Ok(new { message = "sukses mengambil data", statusCode = 200, data = "null" });
            return Ok(new { message = "sukses mengambil data", statusCode = 200, data = data });
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Put(int id, Department department) // /api/Department/{id}
        //public IActionResult Put(Department department) // /api/Department
        {

            var data = myContext.Departments.Find(id); // /api/Department/{id}
            //var data = myContext.Departments.Find(department.Id); /api/Department
            data.Name = department.Name;
            data.DivisionId = department.DivisionId;
            myContext.Departments.Update(data);
            var result = myContext.SaveChanges();
            if (result > 0)
                return Ok(new { statusCode = 200, message = "berhasil mengupdate data" });
            return BadRequest(new { statusCode = 400, message = "gagal mengupdate data" });
        }

        // CREATE
        [HttpPost]
        public IActionResult Post(Department department)
        {
            myContext.Departments.Add(department);
            var result = myContext.SaveChanges();
            if (result > 0)
                return Ok(new { statusCode = 200, message = "berhasil menambahkan data" });
            return BadRequest(new { statusCode = 400, message = "gagal menambahkan data" });
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = myContext.Departments.Find(id);
            myContext.Departments.Remove(data);
            var result = myContext.SaveChanges();
            if (result > 0)
                return Ok(new { statusCode = 200, message = "berhasil menghapus data" });
            return BadRequest(new { statusCode = 400, message = "gagal menghapus data" });
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
