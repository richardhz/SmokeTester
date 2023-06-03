using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;
using SmokeTester.Data;
using SmokeTester.Services;

namespace SmokeTester;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		Environment.SetEnvironmentVariable("AZURE_CLIENT_ID", "40830045-c7e2-48db-8973-37e3f50b0f24");
		Environment.SetEnvironmentVariable("AZURE_TENANT_ID", "8fda8d81-ebbf-4c25-9b08-2453e217680f");
		
		builder.Services.AddSingleton<BlobServiceClient>(c =>
			new BlobServiceClient(
				new Uri("https://sbrholding.blob.core.windows.net"),
				new DefaultAzureCredential()
				));


		builder.Services.AddHttpClient();

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<ISmokeTestTools, SmokeTestTools>();
		builder.Services.AddSingleton<ISmokeStorageTools, SmokeStorageTools>();
		builder.Services.AddSingleton<ICloudStorageTools, CloudStorageTools>();

		return builder.Build();
	}
}
