using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class Role
    {
        public Guid id { get; set; } = new Guid();
        public string RoleName { get; set; } = string.Empty;
        public bool isActive { get; set; } = false;
    }
}
