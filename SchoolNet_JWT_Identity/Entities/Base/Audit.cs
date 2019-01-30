using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_JWT_Identity.Entities.Base
{
    public class Audit
    {
        public Guid CreationUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid LastModifierUserId { get; set; }
        public DateTime LastModificationDate { get; set; }        
    }
}
