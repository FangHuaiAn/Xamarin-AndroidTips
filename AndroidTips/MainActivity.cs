using System;

using Android.OS;
using Android.App;
using Android.Widget;
using Android.Content;
using Android.Graphics;
using Android.Gms.Common;

using Java.IO;
using ZXing.Mobile;

using Debug = System.Diagnostics.Debug;

namespace AndroidTips
{
	[Activity (Label = "AndroidTips", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		
		protected override void OnCreate (Bundle savedInstanceState)
		{
			
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

			var btnTouch = FindViewById<Button> (Resource.Id.btnTouch);
			btnTouch.Click += delegate {
				StartActivity (typeof (TouchActivity));
			};

			var btnGesture = FindViewById<Button> (Resource.Id.btnGesture);
			btnGesture.Click += delegate {
				StartActivity (typeof (GestureActivity));
			};

			var btnSharedPreference = FindViewById<Button> (Resource.Id.btnSharedPreference);
			btnSharedPreference.Click += delegate {
				StartActivity (typeof (SharedPreferenceActivity));
			};

			var btnFileIO = FindViewById<Button> (Resource.Id.btnFileIO);
			btnFileIO.Click += delegate {
				StartActivity (typeof (FileActivity));
			};

			var btnSQLite = FindViewById<Button> (Resource.Id.btnSQLite);
			btnSQLite.Click += delegate {
				StartActivity (typeof (SQLiteActivity));
			};

			var btnWebClient = FindViewById<Button> (Resource.Id.btnWebClient);
			btnWebClient.Click += delegate {
				StartActivity (typeof (WebServiceActivity));
			};

			var btnDefaultLocation = FindViewById<Button> (Resource.Id.btnDefaultLocation);
			btnDefaultLocation.Click += delegate {
				StartActivity (typeof (BaseLocationActivity));
			};

			var btnEasyLocation = FindViewById<Button> (Resource.Id.btnEasyLocation);
			btnEasyLocation.Click += delegate {
				StartActivity (typeof (EasyLocationActivity));
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
			string positiveButtonTitle, EventHandler<DialogClickEventArgs> positiveButtonClickHandle,
			string negativeButtonTitle, EventHandler<DialogClickEventArgs> negativeButtonClickHandle
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
