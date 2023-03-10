using Microsoft.UI;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmokeTester.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();

		//Set the initial window size for windows desk to app. 
		const int WindowWidth = 1400;
		const int WindowHeight = 900;

		Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, View) =>
		{
			var mauiWindow = handler.VirtualView;
			var nativeWindow = handler.PlatformView;
			nativeWindow.Activate();
			IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
			WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
			var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
			appWindow.Resize(new Windows.Graphics.SizeInt32(WindowWidth, WindowHeight));
		});
	}



	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

