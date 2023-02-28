using SmokeTester.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SmokeTester.Services;
public sealed class SmokeTestTools : ISmokeTestTools
{
    private readonly IHttpClientFactory _httpClientFactory = null!;
    public SmokeTestTools(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public async Task<string> ProcessRequest(SmokeParams smokeParams)
    {
        var accessToken = await GetTokenAsync(smokeParams);
        if (accessToken is null)
        {
            return "could not aquire access token.";
        }

        if (smokeParams.Url is null)
        {
            return accessToken;
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        client.DefaultRequestHeaders.Add("X-Correlation-ID", "00000000-0000-0000-0000-000000000062");

        var response = await client.PostAsync(smokeParams.Url,null);
        var json = await response.Content.ReadAsStringAsync();

        var jo = JsonSerializer.Deserialize<Object>(json);
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(jo, options);

        return jsonString;
    }


    private async Task<string> GetTokenAsync(SmokeParams smokeParams)
    {
        string clientId = smokeParams.ClientId;
        string tenantId = smokeParams.TenantId;
        string scope = ".default" ;

        var authority = $"https://login.microsoftonline.com/{tenantId}";
        var app = PublicClientApplicationBuilder.Create(clientId)
            .WithAuthority(new Uri(authority)).WithDefaultRedirectUri()
            .Build();
        var fullscope = $"{clientId}/{scope}";
        var result = await app.AcquireTokenInteractive(new[] { fullscope }).ExecuteAsync();
        
        return result.AccessToken;
    }

    

    
}
