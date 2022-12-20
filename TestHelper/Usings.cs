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
      var discipline2 = new Discipline { Name = "Test1", };

      disciplineRepository.AddAsync(discipline2).Wait();
      disciplineRepository.ChangeTrackerClear();

      Assert.True(disciplineRepository.GetAllAsync().Result.Count == 2);
      Assert.Equal("Test1", disciplineRepository.GetDisciplineAsync("Test1").Result.Name);
      Assert.Equal("Test", disciplineRepository.GetDisciplineAsync("Test").Result.Name);
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
            discipline.AddLessons(new Lesson
            {
                LessonType = LessonType.Практика,
                Topic = "Name",
            });
      disciplineRepository.UpdateAsync(discipline).Wait();
      Assert.Equal("Test Discipline", disciplineRepository.GetDisciplineAsync("Test Discipline").Result.Name);
      Assert.Equal(2, disciplineRepository.GetDisciplineAsync("Test Discipline").Result.LessonCount);
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

      Assert.Equal(1, disciplineRepository.GetByNameAsync("Test").Result.LessonCount);
    }
  }
}