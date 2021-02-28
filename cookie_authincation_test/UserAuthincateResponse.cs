using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cookie_authincation_test
{
    public class UserAuthincateResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
