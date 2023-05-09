using System;
using System.Collections.Generic;

namespace BDFirst.Models
{
    public partial class SCRelation
    {
        public int? FEnrollId { get; set; }
        public int? FCourseId { get; set; }
        public int Id { get; set; }

        public virtual Course? FCourse { get; set; }
        public virtual Student? FEnroll { get; set; }
    }
}
