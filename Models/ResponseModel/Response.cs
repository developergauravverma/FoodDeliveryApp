using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModel
{
    public class Response
    {
        public bool success { get; set; } = false;
        public string message { get; set; } = string.Empty;
        public List<object> listObject { get; set; }
        public object objectResponse { get; set; }
        public string token { get; set; } = string.Empty;
    }
}
