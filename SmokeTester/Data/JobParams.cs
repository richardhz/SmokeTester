using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTester.Data
{
    public class JobParams
    {
        public string Url { get; set; }
        public string JobName { get; set; }
        public string TenantId { get; set; }
        public string User { get; set;}
        public string Password { get; set; }
        public string Result { get; set; }
    }
}
