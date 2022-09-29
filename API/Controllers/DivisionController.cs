using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Context;
using Microsoft.EntityFrameworkCore;
using API.Repositories.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //public class DivisionController : Controller
    public class DivisionController : ControllerBase
    {
        DivisionRepository divisionRepository;

        public DivisionController(DivisionRepository divisionRepository)
        {
            this.divisionRepository = divisionRepository;
        }

        // READ
        [HttpGet]
        public IActionResult Get()
        {
            var data = divisionRepository.Get();
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
            var data = divisionRepository.Get(id);
            if(data == null)
                return Ok(new { message = "sukses mengambil data", statusCode = 200, data = "null" });
            return Ok(new { message = "sukses mengambil data", statusCode = 200, data = data });
        }

        // CREATE
        [HttpPost]
        public IActionResult Post(Division division)
        {
            var result = divisionRepository.Post(division);
            if (result > 0)
                return Ok(new { statusCode = 200, message = "berhasil menambahkan data" });
            return BadRequest(new { statusCode = 400, message = "gagal menambahkan data" });
        }

        // UPDATE
        //[HttpPut("{id}")]
        [HttpPut]
        //public IActionResult Put(int id, Division division) // /api/Division/{id}
        public IActionResult Put(Division division) // /api/Division
        {

            //var data = myContext.Divisions.Find(id); // /api/Division/{id}
            //var data = myContext.Divisions.Find(division.Id); /api/Division
            var result = divisionRepository.Put(division);
            if (result > 0)
                return Ok(new { statusCode = 200, message = "berhasil mengupdate data" });
            return BadRequest(new { statusCode = 400, message = "gagal mengupdate data" });
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = divisionRepository.Delete(id);
            if(result > 0)
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
