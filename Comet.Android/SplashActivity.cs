using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using System.Threading.Tasks;

namespace Comet.Android
{
    [Activity(Theme = "@style/AppTheme", NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RequestWindowFeature(WindowFeatures.NoTitle);
            Handler handler = new Handler();

            var run = new Java.Lang.Runnable(JumpActivity);
            handler.PostDelayed(run, 800);
        }

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    Task startupWork = new Task(() => { SimulateStartup(); });
        //    startupWork.Start();
        //}

        //async void SimulateStartup()
        //{
        //    await Task.Delay(800);
        //    StartActivity(new Intent(Application.Context, typeof(DashBoardActivity)));
        //    base.OverridePendingTransition(0, 0);
        //    Finish();
        //}

        void JumpActivity()
        {
            //Intent intent = new Intent(this, typeof(MainActivity));
            Intent intent = new Intent(this, typeof(DashBoardActivity));
            StartActivity(intent);
            base.OverridePendingTransition(0, 0);
            Finish();
        }

        public override void OnBackPressed()
        {

        }

        //public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        //{
        //    if ((keyCode == Keycode.Back))
        //    {
        //        return true;
        //    }
        //    return base.OnKeyDown(keyCode, e);
        //}
    }
}