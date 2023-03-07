using SmokeTester.Data;

namespace SmokeTester.Services;
public interface ISmokeTestTools
{
    Task<string> ProcessRequest(SmokeParams smokeParams);
    Task<IEnumerable<EndPointProfile>> GetProfiles();
}