using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_JWT_Identity.Authentication
{
    public class TokenResult
    {
        public bool Authenticated { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string AccessToken { get; set; }
    }
}
