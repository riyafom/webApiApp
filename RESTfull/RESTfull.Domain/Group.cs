using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Domain
{
    public class Group
    {
        public int Id { get; set; }
        public string Faculty { get; set; } = String.Empty;
        public int Year { get; set; }

        //Свойства навигации
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; } = null!;

    }
}
