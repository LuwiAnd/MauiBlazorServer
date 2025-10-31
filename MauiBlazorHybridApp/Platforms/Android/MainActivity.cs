#if ANDROID
using Android.App;
using Android.Content.PM;
using Android.OS;

using Android.Content;
using SharedRazorClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;

using Android.Util;

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
        Exported = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | 
        ConfigChanges.Orientation | 
        ConfigChanges.UiMode | 
        ConfigChanges.ScreenLayout | 
        ConfigChanges.SmallestScreenSize | 
        ConfigChanges.Density
    )]
    public class MainActivity : MauiAppCompatActivity
    {
        // Sparar uri temporärt om DI ännu inte är initierat
        string? _deferredUri;

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

        protected override void OnResume()
        {
            base.OnResume();

            // Försök applicera deferred uri när Activity återupptas och MauiProgram.Services finns
            try
            {
                if (!string.IsNullOrEmpty(_deferredUri) && MauiProgram.Services != null)
                {
                    var pending = MauiProgram.Services.GetService<PendingLinkService>();
                    if (pending != null)
                    {
                        pending.PendingUri = _deferredUri;
                        Log.Info("MainActivity", $"Applied deferred deep link: {_deferredUri}");
                    }
                    _deferredUri = null;
                }
            }
            catch (Exception ex)
            {
                Log.Error("MainActivity", ex.ToString());
            }
        }


        void HandleIntent(Intent? intent)
        {
            try
            {
                if (intent?.Action == Intent.ActionView && intent.Data != null)
                {
                    var uri = intent.Data.ToString();

                    // Försök sätta PendingLinkService om DI är klar, annars spara temporärt
                    if (MauiProgram.Services != null)
                    {
                        var pending = MauiProgram.Services.GetService<PendingLinkService>();
                        if (!string.IsNullOrEmpty(uri) && pending != null)
                        {
                            pending.PendingUri = uri;
                        }
                    }
                    else
                    {
                        // DI finns inte än — spara så vi kan applicera senare i OnResume
                        _deferredUri = uri;
                        Log.Info("MainActivity", $"Deferred deep link until services ready: {uri}");
                    }

                    global::Android.Util.Log.Info("MainActivity", $"Handled deep link: {uri}");
                }
            }
            catch (Exception ex)
            {
                // Ignorera/ logga eventuella parsingfel här
                global::Android.Util.Log.Error("MainActivity", ex.ToString());
            }
        }
    }
}
#endif
