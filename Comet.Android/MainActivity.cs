using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content;
using System.Net.Http;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Android.Util;

namespace Comet.Android
{
    [Activity(Label = "", Theme = "@style/AppTheme")]
    public class MainActivity : Activity
    {
        private AlertDialog alertDialog = null;
        private AlertDialog.Builder builder = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            init();
        }

        private void init()
        {
            EditText userName = FindViewById<EditText>(Resource.Id.et_userName);
            EditText password = FindViewById<EditText>(Resource.Id.et_password);
            ImageView unameClear = FindViewById<ImageView>(Resource.Id.iv_unameClear);
            ImageView pwdClear = FindViewById<ImageView>(Resource.Id.iv_pwdClear);
            
            userName.TextChanged += delegate {
                if (userName.Text.Length > 0)
                {
                    unameClear.Visibility = ViewStates.Visible;
                }
                else
                {
                    unameClear.Visibility = ViewStates.Invisible;
                }
            };
            unameClear.Click += delegate
            {
                userName.Text = "";
            };

            password.TextChanged += delegate {
                if (password.Text.Length > 0)
                {
                    pwdClear.Visibility = ViewStates.Visible;
                }
                else
                {
                    pwdClear.Visibility = ViewStates.Invisible;
                }
            };
            pwdClear.Click += delegate
            {
                password.Text = "";
            };

            FindViewById<Button>(Resource.Id.btn_login).Click += ((sender, e) =>
            {
                Toast.MakeText(this, "正在登陆...", ToastLength.Short).Show();

                string url = "https://api.mengya.info/";
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(url);
                    var content = new FormUrlEncodedContent(new Dictionary<string, string>() {
                        { "accountOrPhoneNumber",userName.Text },
                        { "password",password.Text }
                    });

                    try
                    {
                        Task<HttpResponseMessage> response = http.PostAsync("api/login", content);
                        string result = response.Result.Content.ReadAsStringAsync().Result;
                        //Toast.MakeText(this, "登陆成功", ToastLength.Short).Show();
                        //Log.WriteLine(LogPriority.Debug, "MyApp", response.Result.StatusCode.ToString());

                        alertDialog = null;
                        builder = new AlertDialog.Builder(this);
                        alertDialog = builder
                       .SetTitle("登录信息")
                       .SetMessage(result)
                       .SetPositiveButton("确认", (s, ae) =>
                       {
                           Toast.MakeText(this, "登录成功", ToastLength.Short).Show();
                           Intent intent = new Intent(this, typeof(DashBoardActivity));
                           StartActivity(intent);
                           Finish();
                       })
                       .Create();       //创建alertDialog对象  

                        alertDialog.Show();
                        var dialog = new AlertDialog.Builder(this);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    
                }
            });
        }
    }
}

