using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTester.Data;

public class EndPointProfile
{
    public string Key { get; set; }
    public string ClientId { get; set; }
    public string TenantId { get; set; }
    public string BaseUrl { get; set; }
    public string ActiveUrl { get; set; }
    
}
