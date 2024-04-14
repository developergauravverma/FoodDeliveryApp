using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class LoginUser
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class tokenRequest
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string Role { get; set; }
    }
}
