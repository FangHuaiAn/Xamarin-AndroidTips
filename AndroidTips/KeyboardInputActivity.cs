
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Views.InputMethods;

namespace AndroidTips
{
	[Activity (Label = "KeyboardInputActivity")]			
	public class KeyboardInputActivity : Activity
	{
		private Button _btnGo ;
		private EditText _txtUrl ;
		private WebView _webView ;

		private InputMethodManager _InputMethodManager;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView( Resource.Layout.KeyboardInputView );

			_btnGo = FindViewById<Button> (Resource.Id.btnGo);
			_txtUrl = FindViewById<EditText> (Resource.Id.txtUrl);
			_webView = FindViewById<WebView> (Resource.Id.webView);


			InputMethodManager _InputMethodManager  = 
				(InputMethodManager)GetSystemService(Context.InputMethodService);

			_btnGo.Click += (object sender, EventArgs e) => {

				_InputMethodManager.HideSoftInputFromWindow( 
					_txtUrl.WindowToken, 
					HideSoftInputFlags.ImplicitOnly );
			};

			var client = new ContentWebViewClient ();
			_webView.SetWebViewClient(client);
			_webView.Settings.JavaScriptEnabled = true;
			_webView.Settings.UserAgentString = @"Android";

			_webView.LoadUrl ("http://stackoverflow.com/");

		}

		public class ContentWebViewClient : WebViewClient
		{

			public event EventHandler<WebViewLocaitonChangedEventArgs> WebViewLocaitonChanged;

			public event EventHandler<WebViewLoadCompletedEventArgs> WebViewLoadCompleted;

			public override bool ShouldOverrideUrlLoading (WebView view, string url)
			{
				EventHandler<WebViewLocaitonChangedEventArgs> handler = 
					WebViewLocaitonChanged;

				if (null != handler) {
					handler (this, 
						new WebViewLocaitonChangedEventArgs{ 
							CommandString = url });
				}

				return base.ShouldOverrideUrlLoading (view, url);

			}

			public override void OnPageFinished (WebView view, string url)
			{
				base.OnPageFinished (view, url);

				EventHandler<WebViewLoadCompletedEventArgs> handler = 
					WebViewLoadCompleted;

				if (null != handler) {
					handler (this, 
						new WebViewLoadCompletedEventArgs());
				}
			}

			public class WebViewLocaitonChangedEventArgs : EventArgs{

				public string CommandString {get;set;}
			}

			public class WebViewLoadCompletedEventArgs : EventArgs{

			}
		}
	}
}

