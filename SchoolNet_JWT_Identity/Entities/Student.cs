using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_JWT_Identity.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Guid CreationUserId { get; set; }

        public virtual User CreationUser { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
