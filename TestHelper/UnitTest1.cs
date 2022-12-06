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
            discipline1.AddGroups(new Group { Id = 1, Faculty = "test", Year = 2022 });
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
        [Fact]
        public void VoidTest()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            Assert.Equal(1, 1);
        }
        [Fact]
        public void TestAdd()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            var discipline = new Discipline { Name = "Test" };

            disciplineRepository.AddAsync(discipline).Wait();
            disciplineRepository.ChangeTrackerClear();

            Assert.True(disciplineRepository.GetAllAsync().Result.Count == 2);
            Assert.Equal("Test", disciplineRepository.GetDisciplineAsync("Test").Result.Name);
            Assert.Equal("test", disciplineRepository.GetDisciplineAsync("test").Result.Name);
            Assert.Equal(2, disciplineRepository.GetDisciplineAsync("Test").Result.LecturerCount);
        }
        [Fact]
        public void TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            var discipline = disciplineRepository.GetDisciplineAsync("Test").Result;
            disciplineRepository.ChangeTrackerClear();
            discipline.Name = "Test Discipline";
            disciplineRepository.UpdateAsync(discipline).Wait();
            Assert.Equal("Test Discipline", disciplineRepository.GetDisciplineAsync("Test Discipline").Result.Name);
            Assert.Equal(3, disciplineRepository.GetDisciplineAsync("Test Discipline").Result.LecturerCount);
        }
        [Fact]
        public void TestUpdateDelete()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            var discipline = disciplineRepository.GetByNameAsync("Test").Result;
            disciplineRepository.ChangeTrackerClear();
            discipline.RemoveLecturer(0);

            disciplineRepository.UpdateAsync(discipline).Wait();

            Assert.Equal(1, disciplineRepository.GetByNameAsync("Test").Result.LecturerCount);
        }
    }
}