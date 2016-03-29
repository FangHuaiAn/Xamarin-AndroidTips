using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidTips
{
	[Activity (Label = "AndroidTips", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		
		protected override void OnCreate (Bundle savedInstanceState)
		{
			Xamarin.Insights.Initialize (global::AndroidTips.XamarinInsights.ApiKey, this);
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.btnKeyboard);
			button.Click += delegate {
				StartActivity( typeof( KeyboardInputActivity ));
			};

			var btnImageScaleType = FindViewById<Button> (Resource.Id.btnImageScaleType);
			btnImageScaleType.Click += delegate {
				StartActivity( typeof( ImageScaleTypeActivity ));
			};

			var btnLocalNotification = FindViewById<Button>(Resource.Id.btnLocalNotification);
			btnLocalNotification.Click += delegate {
				StartActivity( typeof( LocalNotificationActivity ));
			};
		}
	}
}
