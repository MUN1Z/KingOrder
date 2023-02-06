using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace KingOrder.XF.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            //if (platform == Platform.Android)
            //{
                //return ConfigureApp.Android.Debug().ApkFile("C:/Git/MUN1Z/KingProduct/src/KingOrder.XF/KingOrder.XF.Android/bin/Debug/com.companyname.kingorder.xf.apk").StartApp();
                return ConfigureApp.Android.Debug().InstalledApp("com.companyname.kingorder.xf").StartApp();
            //}

            //return ConfigureApp.iOS.StartApp();
        }
    }
}