using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTester.Data;
public class SmokeParams
{
    public string Url { get; set; }
    public string ClientId { get; set; }
    public string TenantId { get; set; }
    public string Token { get; set; }
    public bool TokenRequired { get; set; }
    public bool UsePost { get; set; }
    public bool IsHealthCheck { get; set; }
    
    
}
