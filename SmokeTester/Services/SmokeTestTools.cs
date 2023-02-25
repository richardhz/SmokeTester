using Microsoft.Win32.SafeHandles;
using SmokeTester.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTester.Services;
public sealed class SmokeTestTools : ISmokeTestTools
{
    private readonly IHttpClientFactory _httpClientFactory = null!;
    public SmokeTestTools(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public async Task<string> ProcessRequest(SmokeParams smokeParams)
    {
        var accessToken = await GetToken(smokeParams);
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetAsync(smokeParams.Url);
        var json = await response.Content.ReadAsStringAsync();
        return json;
    }

    //private async Task<string> GetToken(string clientId, string tenantId)
    //{
    //    var client = _httpClientFactory.CreateClient();
    //    var response = await client.GetAsync($"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token");
    //    var json = await response.Content.ReadAsStringAsync();
    //    return json;
    //}

    private async Task<string> GetToken(SmokeParams smokeParams)
    {
        string clientId = smokeParams.ClientId;
        string tenantId = smokeParams.TenantId;
        string[] scopes = new string[] { "user.read" };

        var app = PublicClientApplicationBuilder.Create(clientId)
            .WithAuthority($"https://login.microsoftonline.com/{tenantId}").WithDefaultRedirectUri()
            .Build();

        var result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
        return result.AccessToken;
    }
}
