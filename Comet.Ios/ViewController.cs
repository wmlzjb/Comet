using Foundation;
using System;
using System.IO;
using UIKit;
using WebKit;

namespace Comet.Ios
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            WKWebView webView = new WKWebView(View.Frame, new WKWebViewConfiguration());
            View.AddSubview(webView);

            //var url = new NSUrl("https://juejin.im/timeline");
            //var request = new NSUrlRequest(url);
            //webView.LoadRequest(request);

            string fileName = "Views/index.html"; 
            string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);
            webView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}