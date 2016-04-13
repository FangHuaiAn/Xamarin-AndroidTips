
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


namespace AndroidTips
{
	[Activity (Label = "StartCameraActivity")]			
	public class StartCameraActivity : Activity
	{
		Button btnOpenCamera ;
		ImageView imageCamera ;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.StartCameraView);


			btnOpenCamera = FindViewById<Button> (Resource.Id.btnOpenCamera);

			btnOpenCamera.Click += (object sender, EventArgs e) => {



			};


		}
	}
}

