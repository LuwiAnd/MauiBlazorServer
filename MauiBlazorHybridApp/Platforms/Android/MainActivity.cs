using Android.App;
using Android.Content.PM;
using Android.OS;

using Android.Content;
using SharedRazorClassLibrary.Services;

namespace MauiBlazorHybridApp.Platforms.Android
{
    [IntentFilter(
    new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "luwiapp"   // Välj ditt app-protokoll (byt om du vill)
)]
//    [IntentFilter(
//    new[] { Intent.ActionView },
//    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
//    DataScheme = "https",
//    DataHost = "dittdomän.com"   // Byt till din domän sen
//)]

    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}
