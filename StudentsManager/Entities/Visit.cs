using System;

namespace StudentsManager.Entities
{
    public class Visit
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Student? Student { get; set; }
        public Subject? Subject { get; set; }
    }
}