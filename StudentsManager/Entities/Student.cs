using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace StudentsManager.Entities
{
    [Index(nameof(Name))]
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public Email? Email { get; set; }
        public List<Visit>? Visits { get; set; }
        public Group? Group { get; set; }
        public Passport? Passport { get; set; }

        public int? VisitsCount => Visits.Count;  
        public override string ToString()
        {
            return Name;
        }

    }
}