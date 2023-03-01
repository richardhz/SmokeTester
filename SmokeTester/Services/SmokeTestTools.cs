using SmokeTester.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SmokeTester.Services;
public sealed class SmokeTestTools : ISmokeTestTools
{
    private readonly IHttpClientFactory _httpClientFactory = null!;
    public SmokeTestTools(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public async Task<string> ProcessRequest2(SmokeParams smokeParams)
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

    public async Task<string> ProcessRequest(SmokeParams smokeParams)
    {
        string accessToken = null;
        HttpClient client = null;
        if (smokeParams.Url is not null)
        {
            client = _httpClientFactory.CreateClient();
        }

        if (smokeParams.TokenRequired)
        {
            accessToken = await GetTokenAsync(smokeParams);

            if (accessToken is null)
            {
                return "could not aquire access token.";
            }

            if (client is null)
            {
                return accessToken;
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Add("X-Correlation-ID", "00000000-0000-0000-0000-000000000062");
        }

        if (client is null)
        {
            return "No valid request has been made.";
        }

        HttpResponseMessage response;
        if (smokeParams.UsePost)
        {
           response = await client.PostAsync(smokeParams.Url, null);
        }
        else
        {
            response = await client.GetAsync(smokeParams.Url);
        }
       
        var json = await response.Content.ReadAsStringAsync();
        if (smokeParams.IsHealthCheck)
        {
            return json;
        }

        var jo = JsonSerializer.Deserialize<Object>(json);
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(jo, options);

        return jsonString;
    }




    private async Task<string> GetTokenAsync(SmokeParams smokeParams)
    {
        string scope = ".default" ;

        var authority = $"https://login.microsoftonline.com/{smokeParams.TenantId}";
        var app = PublicClientApplicationBuilder.Create(smokeParams.ClientId)
            .WithAuthority(new Uri(authority)).WithDefaultRedirectUri()
            .Build();
        var fullscope = $"{smokeParams.ClientId}/{scope}";
        var result = await app.AcquireTokenInteractive(new[] { fullscope }).ExecuteAsync();
        
        return result.AccessToken;
    }

    

    
}
