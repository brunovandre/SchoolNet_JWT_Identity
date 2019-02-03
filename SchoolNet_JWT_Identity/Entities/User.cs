using Microsoft.AspNetCore.Identity;

namespace SchoolNet_JWT_Identity.Entities
{
    public class User : IdentityUser
    {
        public int Age { get; set; }
    }
}
