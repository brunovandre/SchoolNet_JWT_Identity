using System;

namespace SchoolNet_JWT_Identity.ApplicationUserSetup
{
    public class ApplicationUser : IApplicationUser
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
