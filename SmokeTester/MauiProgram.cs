﻿using Microsoft.Extensions.Logging;
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

		builder.Services.AddHttpClient();

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<ISmokeTestTools, SmokeTestTools>();
		builder.Services.AddSingleton<ISmokeStorageTools, SmokeStorageTools>();

		return builder.Build();
	}
}
