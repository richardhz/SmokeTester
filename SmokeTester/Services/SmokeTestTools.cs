using Microsoft.Win32.SafeHandles;
using SmokeTester.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SmokeTester.Services;
public sealed class SmokeTestTools : ISmokeTestTools
{
    private readonly IHttpClientFactory _httpClientFactory = null!;
    public SmokeTestTools(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public async Task<string> ProcessRequest(SmokeParams smokeParams)
    {
        string json = "could not aquire access token.";

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
        //var response = await client.GetAsync(smokeParams.Url);
        var response = await client.PostAsync(smokeParams.Url,null);
        json = await response.Content.ReadAsStringAsync(); 
       
        return json;
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
