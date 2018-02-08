
using Android.Graphics;
using Android.Views;
using Android.Views.Animations;
using Android.Webkit;
using Android.Widget;

namespace Comet.Android.Util
{
    public class CometWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
        {
            view.LoadUrl(request.Url.ToString());
            return false;
        }

        public override void OnPageStarted(WebView view, string url, Bitmap favicon)
        {
            //设定加载开始的操作

            base.OnPageStarted(view, url, favicon);
        }

        public override void OnPageFinished(WebView view, string url)
        {
            //设定加载结束的操作

            base.OnPageFinished(view, url);
            var imgView = ((ViewGroup)view.Parent).FindViewById<ImageView>(Resource.Id.imageView1);

            var mHiddenAction = new TranslateAnimation(Dimension.RelativeToParent,
                    0.0f, Dimension.RelativeToSelf, 1.0f, Dimension.RelativeToSelf, 0.0f, Dimension.RelativeToSelf, 0.0f);
            mHiddenAction.Duration = 200;

            var mShowAction = new TranslateAnimation(Dimension.RelativeToSelf, 1.0f,
                   Dimension.RelativeToSelf, 0.0f, Dimension.RelativeToSelf,
                   0.0f, Dimension.RelativeToSelf, 0.0f);
            mShowAction.Duration = 200;

            view.StartAnimation(mHiddenAction);
            imgView.Visibility = ViewStates.Gone;

            view.StartAnimation(mShowAction);
            view.Visibility = ViewStates.Visible;
        }
    }
}