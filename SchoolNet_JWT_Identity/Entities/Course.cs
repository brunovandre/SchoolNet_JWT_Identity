using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_JWT_Identity.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorkLoad { get; set; }
        public decimal Price { get; set; }
        public bool Online { get; set; }
        public Guid CreationUserId { get; set; }

        public virtual User CreationUser { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
