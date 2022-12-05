using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Infrastructure.Repository
{
    internal class DisciplineRepository
    {
      private readonly Context _context;
      public Context UnitOfWork
      {
        get
        {
          return _context;
        }
      }
      public DisciplineRepository(Context context)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
      }
      public async Task<Discipline?> GetDisciplineAsync(string name)
      {
        return await _context.Disciplines.
                     Where(p => p.Name == name).
                     Include(p => p.Lessons).
                     FirstOrDefaultAsync();
      }
      public async Task DeleteAsync(int id)
      {
        Lesson? lesson = await _context.Lessons.FindAsync(id);
        if (lesson != null) 
        {
          _context.Remove(lesson);
          await _context.SaveChangesAsync();
        }
      }
      public void ChangeTrackerClear()
      {
        _context.ChangeTracker.Clear();
      }
      public async Task<List<Lesson>> GetAllAsync()
      {
      return await _context.Lessons.OrderBy(p => p.Lecturer).ToListAsync();
      }
      public async Task<Lesson?> GetByIdAsync(int id)
      {
        return await _context.Lessons.Where(p => p.Id == id).
                                      Include(p => p.Discipline).
                                      FirstOrDefaultAsync();
      }
      public async Task<Lesson?> GetByTopicAsync(string name)
      {
        return await _context.Lessons.Where(p => p.Topic == name).
                                      Include(p => p.Discipline).
                                      FirstOrDefaultAsync();
      }
      public async Task UpdateAsync(Lesson lesson)
      {
        var existLesson = GetByIdAsync(lesson.Id).Result;
        if (existLesson != null)
        {
          _context.Entry(existLesson).CurrentValues.SetValues(lesson);
          foreach(var topic in lesson.Topic)
          {
            //
          }
        }
        await _context.SaveChangesAsync();
      }
  }
}
