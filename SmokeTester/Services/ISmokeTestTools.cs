using SmokeTester.Data;

namespace SmokeTester.Services;
public interface ISmokeTestTools
{
    Task<string> ProcessRequest(SmokeParams smokeParams);
    Task<IEnumerable<EndPointProfile>> GetProfiles();
    string GetBase64EncodedCredentials(string username, string password);
    Task<HttpResponseMessage> RunJob(string baseUrl, string user, string pwd);
}