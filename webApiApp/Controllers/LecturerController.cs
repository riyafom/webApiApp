using Microsoft.AspNetCore.Mvc;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;
using RESTfull.Infrastructure.Repository;
using System;

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
      _lecturerRepository = new LecturerRepository(_context);
      /* снять примечание после описания репозитория
          _lecturerRepository = new LecturerRepository(_context);*/
    }



    // GET: api/<LecturerController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lecturer>>> GetPersons()
    {
      //return await _context.Persons.ToListAsync();
      return await _lecturerRepository.GetAllAsync();
    }

    // GET api/<LecturerController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Lecturer>> GetLecturer(int id)
    {
      //var person = await _context.Persons.FindAsync(id);
      var lecturer = await _lecturerRepository.GetByIdAsync(id);
      if (lecturer == null)
      {
        return NotFound();
      }
      return lecturer;
    }

    // POST api/<LecturerController>
    [HttpPost]
    public async Task<ActionResult<Lecturer>> PostPerson(Lecturer lecturer)
    {
      //_context.Persons.Add(person);
      //await _context.SaveChangesAsync();
      await _lecturerRepository.AddAsync(lecturer);
      return CreatedAtAction("GetPerson", new { id = lecturer.Id }, lecturer);
    }

    // PUT api/<LecturerController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLecturer(int id, Lecturer lecturer)
    {
      if (id != lecturer.Id)
      {
        return BadRequest();
      }
      await _lecturerRepository.UpdateAsync(lecturer);
      return NoContent();
    }

    // DELETE api/<LecturerController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLecturer(int id)
    {
      //var person = await _context.Persons.FindAsync(id);
      var lecturer = await _lecturerRepository.GetByIdAsync(id);
      if (lecturer == null)
      {
        return NotFound();
      }

      //_context.Persons.Remove(person);
      //await _context.SaveChangesAsync();
      await _lecturerRepository.DeleteAsync(id);

      return NoContent();
    }

    private bool PersonExists(int id)
    {
      return _context.Lecturers.Any(e => e.Id == id);
    }
  }
}
