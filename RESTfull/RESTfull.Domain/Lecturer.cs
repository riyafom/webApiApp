using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Domain
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Surname { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string MiddleName { get; set; } = String.Empty;

        //Свойства навигации
        public Lesson Lesson { get; set; } 

        
    }
}
