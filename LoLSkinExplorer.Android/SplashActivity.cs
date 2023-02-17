using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.CommunityToolkit.UI.Views;
using System;

namespace LoLSkinExplorer.Droid
{
    [Activity(Label = "LoL Skin Explorer", MainLauncher = true, Theme = "@style/MainTheme.Splash", NoHistory = true
        , Icon = "@drawable/AppIcon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        protected override void OnResume()
        {
            base.OnResume();
            //SetContentView(Resource.Layout.splash);
            SetContentView(Resource.Layout.Splash2);
            VideoView video = new VideoView(this);
            video = FindViewById<VideoView>(Resource.Id.SwainVideo);
            string videoPath = $"android.resource://{Application.PackageName}/{Resource.Raw.SwainSplash}";
            video.SetVideoPath(videoPath);
            
            video.Start();
            video.Completion += (sender, args) =>
            {
                SimulateStartup();
            };

        }
        private void SimulateStartup()
        {
            
            StartActivity(new Intent(ApplicationContext, typeof(MainActivity)));
        }
    }
}
