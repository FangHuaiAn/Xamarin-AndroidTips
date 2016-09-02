
using System;
using System.Net;
using System.Threading.Tasks;

using Android.OS;
using Android.App;
using Android.Widget;

using static System.Diagnostics.Debug;

namespace AndroidTips
{
	[Activity (Label = "WebServiceActivity")]
	public class WebServiceActivity : Activity
	{
		private WebWorker Worker { get; set; }

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.WebServiceView);

			Worker = new WebWorker ();

			Worker.HtmlStringReceived += (sender, e) => { 
				WriteLine ($"事件的執行結果：{ e.Html.Length }");
			};

			var btnWebClient = FindViewById<Button> (Resource.Id.data_webserviceview_btnWebClient);
			btnWebClient.Click += async (sender, e) => {

				var result = await Worker.DownloadHtmlString ("https://stackoverflow.com");

				WriteLine ($"await 的執行結果：{ result.Length }");
			};

		}
	}

	public class WebWorker
	{
		private WebClient MyWebClient { get; set; }

		public WebWorker ()
		{
			MyWebClient = new WebClient ();
		}

		public async Task<string> DownloadHtmlString (string url)
		{
			var task = MyWebClient.DownloadStringTaskAsync (url);
			var result = await task;

			EventHandler<HtmlReceivedEventArgs> handler = HtmlStringReceived;
			var args = new HtmlReceivedEventArgs { Html = result };
			if (null != handler) {
				handler (this, args);
			}

			return result;
		}
		public event EventHandler<HtmlReceivedEventArgs> HtmlStringReceived;
	}
	public class HtmlReceivedEventArgs : EventArgs
	{
		public string Html { get; set; }
	}
}

