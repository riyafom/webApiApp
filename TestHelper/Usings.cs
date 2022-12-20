global using Xunit;
using RESTfull.Domain;

namespace TestHelper
{
  public class TestDisciplineRepository
  {
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
      var discipline2 = new Discipline { Name = "Test", };

      disciplineRepository.AddAsync(discipline2).Wait();
      disciplineRepository.ChangeTrackerClear();

      Assert.True(disciplineRepository.GetAllAsync().Result.Count == 2);
      Assert.Equal("Test", disciplineRepository.GetDisciplineAsync("Test").Result.Name);
      Assert.Equal("test", disciplineRepository.GetDisciplineAsync("test").Result.Name);
      //Assert.Equal(2, disciplineRepository.GetDisciplineAsync("Test").Result.LecturerCount);
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