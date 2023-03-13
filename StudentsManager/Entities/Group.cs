using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace StudentsManager.Entities
{
    [Index(nameof(Name))]
    public class Group
    {     
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Student>? Students { get; set; }
        public int? StudentCount => Students?.Count;

        public override string ToString()
        {
            return Name;
        }
    }
}