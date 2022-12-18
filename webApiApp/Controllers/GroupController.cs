using Microsoft.AspNetCore.Mvc;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;
using RESTfull.Infrastructure.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webApiApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GroupController : ControllerBase
  {
    private readonly Context _context;
    private readonly GroupRepository _groupRepository;

    public GroupController(Context context)
    {
      _context = context;
      _groupRepository = new GroupRepository(_context);

    }


    // GET: api/<GroupController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
    {
      return await _groupRepository.GetAllAsync();
    }

    // GET api/<GroupController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetGroup(int id)
    {
      var group = await _groupRepository.GetByIdAsync(id);
      if (group == null)
      {
        return NotFound();
      }
      return group;
    }

    // POST api/<GroupController>
    [HttpPost]
    public async Task<ActionResult<Group>> PostGroup(Group group)
    {
      await _groupRepository.AddAsync(group);
      return CreatedAtAction("GetGroup", new { id = group.Id }, group);
    }

    // PUT api/<GroupController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGroup(int id, Group group)
    {
      if (id != group.Id)
      {
        return BadRequest();
      }
      await _groupRepository.UpdateAsync(group);
      return NoContent();
    }

    // DELETE api/<GroupController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
      //var person = await _context.Persons.FindAsync(id);
      var group = await _groupRepository.GetByIdAsync(id);
      if (group == null)
      {
        return NotFound();
      }

      //_context.Persons.Remove(person);
      //await _context.SaveChangesAsync();
      await _groupRepository.DeleteAsync(id);

      return NoContent();
    }

    private bool GroupExist(int id)
    {
      return _context.Groups.Any(e => e.Id == id);
    }
  }
}
