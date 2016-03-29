
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
	[Activity (Label = "NotificationProcessingActivity")]			
	public class NotificationProcessingActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.NotificationProcessingView);

			var message = Intent.Extras.GetString ("message");

			var txtReceived = FindViewById<EditText> (Resource.Id.txtReceived);
			txtReceived.Text = message;

		}
	}
}

