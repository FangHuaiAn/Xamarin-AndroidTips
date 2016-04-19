using System;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

using Android.OS;
using Android.App;
using Android.Util;
using Android.Widget;
using Android.Content;
using Android.Graphics;
using Android.Gms.Common;

using Java.IO;

using ZXing;
using ZXing.Mobile;

using Debug = System.Diagnostics.Debug ;

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

			MobileBarcodeScanner.Initialize (Application);

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

			var btnPlayVideo = FindViewById<Button>(Resource.Id.btnPlayVideo);
			btnPlayVideo.Click += delegate {
				StartActivity( typeof( PlayVideoActivity ));
			};

			var btnQRCode = FindViewById<Button>(Resource.Id.btnQRCode);
			btnQRCode.Click += delegate {
				StartActivity( typeof( QRCodeActivity ));
			};

			var btnCamera = FindViewById<Button>(Resource.Id.btnCamera);
			btnCamera.Click += delegate {
				StartActivity( typeof( StartCameraActivity ));
			};

			var btnGoogleSignIn = FindViewById<Button>(Resource.Id.btnGoogleSignIn);
			btnGoogleSignIn.Click += delegate {
				StartActivity( typeof( GoogleSignInActivity ));
			};



			if (IsPlayServicesAvailable()  ) {
				var intent = new Intent (this, typeof (RegistrationIntentService));
				StartService (intent);
			}
		}

		public bool IsPlayServicesAvailable ()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable (this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError (resultCode)) {
					var errorMessage = GoogleApiAvailability.Instance.GetErrorString (resultCode);

					ShowAlert( errorMessage, "確定", (sender, args)=>{}, "", null );
					return true;
				}
				else
				{
					ShowAlert( "Google Play Service 未安裝", "結束 App", (sender, args)=>{
						Finish ();
					}, "", null );

				}
				return false;
			}
			else
			{
				
				ShowAlert( "Google Play Services 已安裝", "確定", (sender, args)=>{

					Debug.WriteLine("Click positiveButton");

				}, "", null );


				return true;
			}
		}

		private void ShowAlert(string title, 
			string positiveButtonTitle, EventHandler<Android.Content.DialogClickEventArgs> positiveButtonClickHandle,
			string negativeButtonTitle, EventHandler<Android.Content.DialogClickEventArgs> negativeButtonClickHandle
			){

			AlertDialog.Builder alert = new AlertDialog.Builder (this);

			alert.SetTitle (title);

			alert.SetPositiveButton (positiveButtonTitle, positiveButtonClickHandle);

			if (string.IsNullOrEmpty (negativeButtonTitle)) {
				alert.SetNegativeButton (negativeButtonTitle, negativeButtonClickHandle);
			}

			RunOnUiThread (() => {
				alert.Show();
			} );
			//App._file	{/storage/emulated/0/Pictures/AndroidTips/Tips_5027a27b-631e-4dee-bb22-d9d313fa12ec.jpg}	Java.IO.File
		}
	}

	public static class App {
		public static File _file;
		public static File _dir;
		public static Bitmap bitmap;
	}
}
