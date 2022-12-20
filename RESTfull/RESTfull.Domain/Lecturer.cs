namespace RESTfull.Domain
{
  public class Lecturer
  {
    public int Id { get; set; }
    public string Surname { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string MiddleName { get; set; } = String.Empty;

    //Свойства навигации
    public List<Lesson> Lessons { get; set; } = new List<Lesson>();

    public void AddLessons(Lesson lesson)
    {
      Lessons.Add(lesson);
    }
    public void RemoveLesson(int index)
    {
      Lessons.RemoveAt(index);
    }
    public int LessonCount { get { return Lessons.Count; } }

  }
}
