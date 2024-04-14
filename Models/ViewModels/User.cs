using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class User
    {
        public Guid id { get; set; } = new Guid();
        public string userName { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string emailId { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string contactNo { get; set; } = string.Empty;
        public bool isActive { get; set; } = false;
        public string roleId { get; set; } = string.Empty;
        public List<string> roleIdLst { get; set; } = new List<string>();
        public List<Role> UserRoles { get; set; } = new List<Role>();
    }
}
