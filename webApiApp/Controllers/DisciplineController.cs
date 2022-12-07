using Microsoft.AspNetCore.Mvc;
using RESTfull.Domain;
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
    public async Task<ActionResult<IEnumerable<Discipline>>> GetDisciplines()
    {
      return await _disciplineRepository.GetAllAsync();
    }

    // GET api/<DisciplineController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Discipline>> GetDiscipline(int id)
    {
      var discipline = await _disciplineRepository.GetByIdAsync(id);
      if (discipline == null)
      {
        return NotFound();
      }
      return discipline;
    }

    // POST api/<DisciplineController>
    [HttpPost]
    public async Task<ActionResult<Discipline>> PostDiscipline(Discipline discipline)
    {
      await _disciplineRepository.AddAsync(discipline);
      return CreatedAtAction("GetDiscipline", new { id = discipline.Id }, discipline);
    }

    // PUT api/<DisciplineController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDiscipline(int id, Discipline discipline)
    {
      if (id != discipline.Id)
      {
        return BadRequest();
      }
      await _disciplineRepository.UpdateAsync(discipline);
      return NoContent();
    }

    // DELETE api/<DisciplineController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscipline(int id)
    {
      var discipline = await _disciplineRepository.GetByIdAsync(id);
      if (discipline == null)
      {
        return NotFound();
      }
      await _disciplineRepository.DeleteAsync(id);
      return NoContent();
    }
    private bool DisciplineExist(int id)
    {
      return _context.Disciplines.Any(e => e.Id == id);
    }
  }
}
