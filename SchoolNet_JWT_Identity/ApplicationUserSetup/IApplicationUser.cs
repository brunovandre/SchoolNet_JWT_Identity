using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_JWT_Identity.ApplicationUserSetup
{
    public interface IApplicationUser
    {
        Guid UserId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
    }
}
