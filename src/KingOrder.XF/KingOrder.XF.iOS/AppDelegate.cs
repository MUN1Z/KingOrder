using Foundation;
using UIKit;
using Xamarin.Forms.Vonage.iOS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using KingOrder.XF.Ioc;
using Xamarin.Forms;
using KingOrder.XF.Ios.Implementations;

namespace KingOrder.XF.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is dinvoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            PlatformVonage.Init(this); // Setup Vonage
            global::Xamarin.Forms.Forms.Init();
            DependencyService.Register<IImageResizer, ImageResizer>();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
