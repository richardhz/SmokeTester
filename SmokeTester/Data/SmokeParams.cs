using System.Globalization;

namespace SmokeTester.Data;
public class SmokeParams
{
    private string _url;
    public string Url
    {
        get { return RequestUrl(); }
        set => _url = value;
    }
    public string ClientId { get; set; }
    public string TenantId { get; set; }
    public string Token { get; set; }
    public bool TokenRequired { get; set; } = true;
    public bool UsePost { get; set; } = true;
    public bool IsHealthCheck { get; set; }

    private string RequestUrl()
    {
        //26 days my be configurable.
        if (_url is not null && _url.ToUpper().Contains("{REQUIRED-DATE-STRING}"))
        {
            var datestring = DateTime.Now.AddDays(-26).ToString("ddd, d MMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("en-GB"));
            datestring += " GMT";
            return _url.Replace("{REQUIRED-DATE-STRING}", datestring,StringComparison.CurrentCultureIgnoreCase);
        }
        else { return _url; }
    }
}
