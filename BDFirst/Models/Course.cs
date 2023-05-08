using System;
using System.Collections.Generic;

namespace BDFirst.Models
{
    public partial class Course
    {
        public Course()
        {
            SCRelations = new HashSet<SCRelation>();
        }

        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        public virtual ICollection<SCRelation> SCRelations { get; set; }
    }
}
