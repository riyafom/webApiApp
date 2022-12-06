using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;

namespace RESTfull.Infrastructure.Repository
{
    public class DisciplineRepository
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
                         Include(p => p.Lecturers).
                         FirstOrDefaultAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Discipline? discipline = await _context.Disciplines.FindAsync(id);
            if (discipline != null)
            {
                _context.Remove(discipline);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
        public async Task<List<Discipline>> GetAllAsync()
        {
            return await _context.Disciplines.OrderBy(p => p.Name).ToListAsync();
        }
        public async Task<Discipline?> GetByIdAsync(int id)
        {
            return await _context.Disciplines.Where(p => p.Id == id).
                                          Include(p => p.Lecturers).
                                          FirstOrDefaultAsync();
        }
        public async Task AddAsync(Discipline discipline)
        {
            _context.Disciplines.Add(discipline);
            await _context.SaveChangesAsync();
        }
        public async Task<Discipline?> GetByNameAsync(string name)
        {
            return await _context.Disciplines.Where(p => p.Name == name).
                                              Include(p => p.Lecturers).
                                              FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(Discipline discipline)
        {
            var existDiscipline = GetByIdAsync(discipline.Id).Result;
            if (existDiscipline != null)
            {
                _context.Entry(existDiscipline).CurrentValues.SetValues(discipline);
                foreach (var lecturer in discipline.Lecturers)
                {
                    var existLecturer = existDiscipline.Lecturers.FirstOrDefault(l => l.Id == lecturer.Id);
                    if (existLecturer == null)
                    {
                        existDiscipline.Lecturers.Add(lecturer);
                    }
                    else
                    {
                        _context.Entry(existLecturer).CurrentValues.SetValues(lecturer);
                    }
                }
                foreach (var existLecturer in existDiscipline.Lecturers)
                {
                    if (!discipline.Lecturers.Any(pn => pn.Id == existLecturer.Id))
                    {
                        _context.Remove(existLecturer);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
