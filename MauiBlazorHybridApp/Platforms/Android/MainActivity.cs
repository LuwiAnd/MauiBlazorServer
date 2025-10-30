using Android.App;
using Android.Content.PM;
using Android.OS;

using Android.Content;
using SharedRazorClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;

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

    [Activity(
        Theme = "@style/Maui.SplashTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | 
        ConfigChanges.Orientation | 
        ConfigChanges.UiMode | 
        ConfigChanges.ScreenLayout | 
        ConfigChanges.SmallestScreenSize | 
        ConfigChanges.Density
    )]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HandleIntent(Intent);
        }

        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);
            HandleIntent(intent);
        }

        void HandleIntent(Intent? intent)
        {
            try
            {
                if (intent?.Action == Intent.ActionView && intent.Data != null)
                {
                    var uri = intent.Data.ToString();

                    // Hämta PendingLinkService via MauiProgram.Services (undvik obsolete API)
                    var pending = MauiProgram.Services.GetService<PendingLinkService>();
                    if (!string.IsNullOrEmpty(uri) && pending != null)
                    {
                        pending.PendingUri = uri;
                    }
                }
            }
            catch
            {
                // Ignorera eventuella parsingfel här
            }
        }
    }
}

