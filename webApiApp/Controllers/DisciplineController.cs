using Microsoft.AspNetCore.Mvc;
using RESTfull.Infrastructure.Data;
using RESTfull.Infrastructure.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        private readonly Context _context;
        private readonly DisciplineRepository _disciplineRepository;

        public DisciplineController(Context context)
        {
            _context = context;
            _disciplineRepository = new DisciplineRepository(_context);
        }

        // GET: api/<DisciplineController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DisciplineController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DisciplineController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DisciplineController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DisciplineController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
