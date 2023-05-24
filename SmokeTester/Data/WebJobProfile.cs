using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTester.Data
{
    public class WebJobProfile
    {
        public string Key { get; set; }
        public string JobName { get; set; }
        public string TenantId { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
    }
}
