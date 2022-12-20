using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;
using RESTfull.Infrastructure.Repository;
namespace TestHelper
{
  public class TestHelper
  {
    private readonly Context _context;
    public TestHelper()
    {
      var contextOptions = new DbContextOptionsBuilder<Context>()
        .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
        .Options;

      _context = new Context(contextOptions);
      _context.Database.EnsureDeleted();
      _context.Database.EnsureCreated();
      var discipline1 = new Discipline
      {
        Name = "Test",
      };
      _context.Disciplines.Add(discipline1);
      _context.SaveChanges();
      _context.ChangeTracker.Clear();
    }
    public DisciplineRepository DisciplineRepository
    {
      get
      {
        return new DisciplineRepository(_context);
      }
    }
    
  }
}