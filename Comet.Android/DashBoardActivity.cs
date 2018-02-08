using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Webkit;
using CN.Jpush.Android.Api;
using Comet.Android.Util;
using System.Threading;
using static Android.Webkit.WebSettings;

namespace Comet.Android
{
    [Activity(Label = "盟伢医生", MainLauncher = true, Theme = "@style/TranslucentTheme", HardwareAccelerated = true)]
    public class DashBoardActivity: AppCompatActivity
    {
        WebView localWebView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DashBoard);
            Window.SetFlags(WindowManagerFlags.HardwareAccelerated, WindowManagerFlags.HardwareAccelerated);

            JPushInterface.SetDebugMode(true);
            JPushInterface.Init(this);

            localWebView = FindViewById<WebView>(Resource.Id.webView1);
            WebView.SetWebContentsDebuggingEnabled(true);
            localWebView.SetWebViewClient(new CometWebViewClient());
            localWebView.SetWebChromeClient(new WebChromeClient());

            localWebView.Settings.LoadWithOverviewMode = true;
            localWebView.Settings.JavaScriptEnabled = true;
            localWebView.Settings.DomStorageEnabled = true;
            localWebView.Settings.SaveFormData = false;
            localWebView.Settings.SavePassword = false;
            localWebView.Settings.CacheMode = CacheModes.NoCache;
            localWebView.Settings.SetAppCacheEnabled(false);
            localWebView.Settings.SetRenderPriority(RenderPriority.High);
            localWebView.Settings.UseWideViewPort = true;
            localWebView.Settings.SetSupportZoom(true);
            localWebView.Settings.BuiltInZoomControls = false;
            localWebView.Settings.SetLayoutAlgorithm(LayoutAlgorithm.SingleColumn);

            localWebView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            localWebView.ScrollbarFadingEnabled = true;
            localWebView.ClearCache(false);
            localWebView.ClearHistory();
            localWebView.ClearFormData();

            localWebView.LoadUrl("file:///android_asset/www/index.html");

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                localWebView.SetLayerType(LayerType.Hardware, null);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    View decorView = Window.DecorView;
                    decorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutFullscreen | (StatusBarVisibility)SystemUiFlags.LayoutStable;
                    Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                    Window.SetStatusBarColor(new Color(ContextCompat.GetColor(this, Resource.Color.transparent)));
                }
                else
                {
                    Window.AddFlags(WindowManagerFlags.TranslucentStatus);
                    Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
                }
            }
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if ((keyCode == Keycode.Back) && localWebView.CanGoBack())
            {
                localWebView.GoBack();
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

        protected override void OnDestroy()
        {
            if (localWebView != null)
            {
                localWebView.LoadDataWithBaseURL(null, "", "text/html", "utf-8", null);
                localWebView.ClearHistory();

                ((ViewGroup)localWebView.Parent).RemoveView(localWebView);

                localWebView.Destroy();
                localWebView = null;
            }
            base.OnDestroy();
        }
    }
}