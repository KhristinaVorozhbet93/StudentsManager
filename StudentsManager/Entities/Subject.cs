using System;

namespace StudentsManager.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}