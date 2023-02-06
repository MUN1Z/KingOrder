using KingOrder.Application.Shared.Services;
using Xamarin.Forms;
using Xamarin.Forms.Vonage;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace KingOrder.XF
{
    public partial class App : Xamarin.Forms.Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<KingOrderApiService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            CrossVonage.Current.ApiKey = "ADD_YOUR_DATA_HERE";
            CrossVonage.Current.UserToken = "ADD_YOUR_DATA_HERE";
            CrossVonage.Current.SessionId = "ADD_YOUR_DATA_HERE";
            CrossVonage.Current.ErrorOccurred += (sender, args) => MainPage.DisplayAlert("ERROR", args.Message, "OK");

            AppCenter.Start("android=ADD_YOUR_DATA_HERE;" +
                  "uwp=ADD_YOUR_DATA_HERE;" +
                  "ios=ADD_YOUR_DATA_HERE;" +
                  "macos=ADD_YOUR_DATA_HERE;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
