using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using RESTfull.Infrastructure.Data;
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
      var lesson1 = new Lesson
      {
        Id = 1,
      };
    }
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
      Assert.Pass();
    }
  }
}