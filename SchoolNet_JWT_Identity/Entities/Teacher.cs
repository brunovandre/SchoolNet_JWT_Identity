using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_JWT_Identity.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid CreationUserId { get; set; }

        public virtual User CreationUser { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
