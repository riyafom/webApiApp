using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Domain
{
    public enum LessonType
    {
        Lecture, 
        Practical,
        Laboratory
    }
    public class Lesson
    {
        public int Id { get; set; }
        public string Topic { get; set; }  = string.Empty;
        public LessonType LessonType { get; set; }


        //Свойства навигации
        public Lecturer Lecturer { get; set; } 
        public int LecturerId { get; set; }
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
    }
}
