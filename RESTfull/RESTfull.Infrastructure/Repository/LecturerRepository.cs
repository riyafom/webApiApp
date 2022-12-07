using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;

namespace RESTfull.Infrastructure.Repository
{
  public class LecturerRepository
  {
    private readonly Context _context;
    public Context UnitOfWork
    {
      get
      {
        return _context;
      }
    }
    public LecturerRepository(Context context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<Lecturer?> GetLecturerAsync(string name)
    {
      return await _context.Lecturers.Where(p => p.Name == name).Include(p => p.Lessons).FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
      Lecturer? lecturer = await _context.Lecturers.FindAsync(id);
      if (lecturer != null)
      {
        _context.Remove(lecturer);
        await _context.SaveChangesAsync();
      }
    }
    public void ChangeTrackerClear()
    {
      _context.ChangeTracker.Clear();
    }
    public async Task<List<Lecturer>> GetAllAsync()
    {
      return await _context.Lecturers.OrderBy(p => p.Name).ToListAsync();
    }
    public async Task<Lecturer?> GetByIdAsync(int id)
    {
      return await _context.Lecturers.Where(p => p.Id == id).Include(p => p.Lessons).FirstOrDefaultAsync();
    }
    public async Task AddAsync(Lecturer lecturer)
    {
      _context.Lecturers.Add(lecturer);
      await _context.SaveChangesAsync();
    }
    public async Task<Lecturer?> GetByNameAsync(string name)
    {
      return await _context.Lecturers.Where(p => p.Name == name).Include(p => p.Lessons).FirstOrDefaultAsync();
    }
    public async Task UpdateAsync(Lecturer lecturer)
    {
      var existLecturer = GetByIdAsync(lecturer.Id).Result;
      if (existLecturer != null)
      {
        _context.Entry(existLecturer).CurrentValues.SetValues(lecturer);
        foreach (var lesson in lecturer.Lessons)
        {
          var existLesson = existLecturer.Lessons.FirstOrDefault(l => l.Id == lecturer.Id);
          if (existLecturer == null)
          {
            existLecturer.Lessons.Add(lesson);
          }
          else
          {
            _context.Entry(existLesson).CurrentValues.SetValues(lesson);
          }
        }
        foreach (var existLesson in existLecturer.Lessons)
        {
          if (!lecturer.Lessons.Any(pn => pn.Id == existLesson.Id))
          {
            _context.Remove(existLesson);
          }
        }
      }
      await _context.SaveChangesAsync();
    }
  }
}
