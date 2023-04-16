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
        Microsoft.UI.Xaml.Window nativeWindow;
        Microsoft.UI.Windowing.AppWindow appWindow;


        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, View) =>
		{
			var mauiWindow = handler.VirtualView;
			nativeWindow = handler.PlatformView;
			nativeWindow.Activate();
			IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
			WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
			appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
			appWindow.Resize(new Windows.Graphics.SizeInt32(WindowWidth, WindowHeight));
            if (appWindow != null)
            {
                appWindow.Changed += AppWindow_Changed;
            }
        });

        void AppWindow_Changed(Microsoft.UI.Windowing.AppWindow sender, Microsoft.UI.Windowing.AppWindowChangedEventArgs args)
        {
            if (args.DidPositionChange)
            {
                // Whenever the app's window moves, force the WebView2 to update, which will cause any open HTML select's to close (which is normal behavior), and thus not "float around"
                var originalSize = appWindow.Size;
                appWindow.Resize(new Windows.Graphics.SizeInt32(appWindow.Size.Width, appWindow.Size.Height + 1));
                appWindow.Resize(originalSize);

            }
        }
    }



	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

