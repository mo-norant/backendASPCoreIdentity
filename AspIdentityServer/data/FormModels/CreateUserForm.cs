using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIdentityServer.data.FormModels
{
    public class CreateUserForm
    {
        public string familyname { get; set; }
        public string givenname { get; set; }
        public string password { get; set; }
        public string passwordvalidate { get; set; }
        public string username { get; set; }
        public string rolename { get; set; }
        public string email { get; set; }
    }
}
