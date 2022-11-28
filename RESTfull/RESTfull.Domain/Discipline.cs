﻿using System;
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
        public List<Lesson> Lessons { get; set; } 
        public List<Group> Groups { get; set; } 
    }
}
