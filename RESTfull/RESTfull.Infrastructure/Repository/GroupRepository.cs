using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;

namespace RESTfull.Infrastructure.Repository
{
  public class GroupRepository
  {
    private readonly Context _context;
    public Context UnitOfWork
    {
      get
      {
        return _context;
      }
    }
    public GroupRepository(Context context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<Group?> GetGroupAsync(string name)
    {
      return await _context.Groups.Where(p => p.Faculty == name).Include(p => p.Disciplines).FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
      Group? group = await _context.Groups.FindAsync(id);
      if (group != null)
      {
        _context.Remove(group);
        await _context.SaveChangesAsync();
      }
    }
    public void ChangeTrackerClear()
    {
      _context.ChangeTracker.Clear();
    }
    public async Task<List<Group>> GetAllAsync()
    {
      return await _context.Groups.OrderBy(p => p.Faculty).ToListAsync();
    }
    public async Task<Group?> GetByIdAsync(int id)
    {
      return await _context.Groups.Where(p => p.Id == id).Include(p => p.Disciplines).FirstOrDefaultAsync();
    }
    public async Task AddAsync(Group group)
    {
      _context.Groups.Add(group);
      await _context.SaveChangesAsync();
    }
    public async Task<Group?> GetByNameAsync(string name)
    {
      return await _context.Groups.Where(p => p.Faculty == name).
                                        Include(p => p.Disciplines).
                                        FirstOrDefaultAsync();
    }
    public async Task UpdateAsync(Group group)
    {
      var existGroup = GetByIdAsync(group.Id).Result;
      if (existGroup != null)
      {
        _context.Entry(existGroup).CurrentValues.SetValues(group);
        foreach (var discipline in group.Disciplines)
        {
          var existDiscipline = existGroup.Disciplines.FirstOrDefault(l => l.Id == discipline.Id);
          if (existDiscipline == null)
          {
            existGroup.Disciplines.Add(discipline);
          }
          else
          {
            _context.Entry(existDiscipline).CurrentValues.SetValues(discipline);
          }
        }
        foreach (var existLecturer in existGroup.Disciplines)
        {
          if (!group.Disciplines.Any(pn => pn.Id == existLecturer.Id))
          {
            _context.Remove(existLecturer);
          }
        }
      }
      await _context.SaveChangesAsync();
    }
  }
}
