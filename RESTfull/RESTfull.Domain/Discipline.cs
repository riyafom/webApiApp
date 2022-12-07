namespace RESTfull.Domain
{
  public class Discipline
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Time { get; set; }


    //Свойства навигации
    public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    public List<Group> Groups { get; set; } = new List<Group>();
    public List<Lecturer> Lecturers { get; set; } = new List<Lecturer>();

    public void AddLessons(Lesson lesson)
    {
      Lessons.Add(lesson);
    }
    public void RemoveLesson(int index)
    {
      Lessons.RemoveAt(index);
    }
    public int LessonCount { get { return Lessons.Count; } }
    public void AddGroups(Group group)
    {
      Groups.Add(group);
    }
    public void RemoveGroup(int index)
    {
      Groups.RemoveAt(index);
    }
    public int GroupsCount { get { return Groups.Count; } }

    public void AddLecturer(Lecturer lecturer)
    {
      Lecturers.Add(lecturer);
    }
    public void RemoveLecturer(int index)
    {
      Lecturers.RemoveAt(index);
    }
    public int LecturerCount { get { return Lecturers.Count; } }
  }
}
