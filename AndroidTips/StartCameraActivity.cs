using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Runtime;
using Android.Graphics;
using Android.Provider;
using Android.Content.PM;

using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using Debug = System.Diagnostics.Debug;

namespace AndroidTips
{
	[Activity (Label = "StartCameraActivity")]			
	public class StartCameraActivity : Activity
	{
		Button btnOpenCamera ;
		ImageView resultImageView ;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.StartCameraView);

			resultImageView = FindViewById<ImageView> (Resource.Id.imageResult);

			btnOpenCamera = FindViewById<Button> (Resource.Id.btnOpenCamera);

			btnOpenCamera.Click += (object sender, EventArgs e) => {

				if(  IsThereAnAppToTakePictures () ){
					Debug.WriteLine("It's ok to use camera.");
					CreateDirectoryForPictures ();

					Intent intent = new Intent(MediaStore.ActionImageCapture);

					App._file = new File(App._dir, String.Format("Tips_{0}.jpg", Guid.NewGuid()));

					intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));

					StartActivityForResult(intent, 0);
				}


			};


		}
			

		private void CreateDirectoryForPictures ()
		{
			App._dir = new File (
				Environment.GetExternalStoragePublicDirectory (
					Environment.DirectoryPictures), "AndroidTips");
			if (!App._dir.Exists ())
			{
				bool result =App._dir.Mkdirs( );
				Debug.WriteLine ("mkdir:" + result);
			}
		}

		private bool IsThereAnAppToTakePictures ()
		{
			Intent intent = new Intent (MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				PackageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			// Make it available in the gallery

			Intent mediaScanIntent = new Intent (Intent.ActionMediaScannerScanFile);
			Uri contentUri = Uri.FromFile (App._file);
			mediaScanIntent.SetData (contentUri);
			SendBroadcast (mediaScanIntent);

			// Display in ImageView. We will resize the bitmap to fit the display.
			// Loading the full sized image will consume to much memory
			// and cause the application to crash.

			int height = Resources.DisplayMetrics.HeightPixels;
			int width = resultImageView.Height ;
			App.bitmap = App._file.Path.LoadAndResizeBitmap (width, height);
			if (App.bitmap != null) {
				resultImageView.SetImageBitmap (App.bitmap);

				Debug.WriteLine ("contentUri:" + contentUri);

				App.bitmap = null;
			}



			// Dispose of the Java side bitmap.
			GC.Collect();
		}
	}

	public static class BitmapHelpers
	{
		public static Bitmap LoadAndResizeBitmap (this string fileName, int width, int height)
		{
			// First we get the the dimensions of the file on disk
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeFile (fileName, options);

			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			int inSampleSize = 1;

			if (outHeight > height || outWidth > width)
			{
				inSampleSize = outWidth > outHeight
					? outHeight / height
					: outWidth / width;
			}

			// Now we will load the image and have BitmapFactory resize it for us.
			options.InSampleSize = inSampleSize;
			options.InJustDecodeBounds = false;
			Bitmap resizedBitmap = BitmapFactory.DecodeFile (fileName, options);

			return resizedBitmap;
		}
	}
}

