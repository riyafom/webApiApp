using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;

namespace RESTfull.Infrastructure.Repository
{
 
    public class LessonRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public LessonRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _context.Lessons.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Lesson> GetByIdAsync(int id)
        {
            return await _context.Lessons.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Lesson> GetByNameAsync(string topic)
        {
            return await _context.Lessons.Where(p => p.Topic == topic).FirstOrDefaultAsync();
        }


        public async Task AddAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Lesson lesson)
        {
            var existPerson = GetByIdAsync(lesson.Id).Result;
            if (existPerson != null)
            {
                _context.Entry(existPerson).CurrentValues.SetValues(lesson);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Lesson lesson = await _context.Lessons.FindAsync(id);
            _context.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
