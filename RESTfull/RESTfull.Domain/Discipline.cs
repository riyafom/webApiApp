using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }
}
