﻿using Microsoft.AspNetCore.Mvc;
using RESTfull.Infrastructure.Data;
using RESTfull.Infrastructure.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly Context _context;
        private readonly LecturerRepository _lecturerRepository;

        public LecturerController(Context context)
        {
            _context = context;
            /* снять примечание после описания репозитория
                _lecturerRepository = new LecturerRepository(_context);*/
        }



        // GET: api/<LecturerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LecturerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LecturerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LecturerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LecturerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
