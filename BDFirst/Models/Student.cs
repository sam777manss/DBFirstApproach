using System;
using System.Collections.Generic;

namespace BDFirst.Models
{
    public partial class Student
    {
        public Student()
        {
            SCRelations = new HashSet<SCRelation>();
        }

        public string? Name { get; set; }
        public int EnrollId { get; set; }
        public string? Department { get; set; }

        public virtual ICollection<SCRelation> SCRelations { get; set; }
    }
}
