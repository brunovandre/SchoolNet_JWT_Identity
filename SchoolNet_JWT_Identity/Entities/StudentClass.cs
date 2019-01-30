using SchoolNet_JWT_Identity.Entities.Base;
using System;

namespace SchoolNet_JWT_Identity.Entities
{
    public class StudentClass
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public Audit Audit { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
    }
}
