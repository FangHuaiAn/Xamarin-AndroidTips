using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Gcm;
using Android.Util;
using Android.Support.V4.App;

using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace AndroidTips
{
	[Service (Exported = false), IntentFilter (new [] { "com.google.android.c2dm.intent.RECEIVE" })]
	public class MyGcmListenerService : GcmListenerService
	{
		public override void OnMessageReceived (string from, Bundle data)
		{
			var message = data.GetString ("message");
			Log.Debug ("MyGcmListenerService", "From:    " + from);
			Log.Debug ("MyGcmListenerService", "Message: " + message);
			SendNotification (message);
		}

		void SendNotification (string message)
		{
			
			Bundle valuesForActivity = new Bundle();
			valuesForActivity.PutString ("message", message );

			Intent resultIntent = new Intent(this, typeof (NotificationProcessingActivity));
			resultIntent.PutExtras (valuesForActivity);

			// Construct a back stack for cross-task navigation:
			TaskStackBuilder stackBuilder = TaskStackBuilder.Create (this);
			stackBuilder.AddParentStack (
				Java.Lang.Class.FromType(typeof(NotificationProcessingActivity )));
			stackBuilder.AddNextIntent (resultIntent);

			// Create the PendingIntent with the back stack:            
			PendingIntent resultPendingIntent = 
				stackBuilder.GetPendingIntent (0, (int)PendingIntentFlags.UpdateCurrent);



			var notificationBuilder = new NotificationCompat.Builder(this)
				.SetSmallIcon (Resource.Drawable.ic_stat_button_click)
				.SetContentTitle ("GCM Message")
				.SetContentText (message)
				.SetAutoCancel (true)
				//.SetVibrate(vT) //设置震动，此震动数组为：long vT[]={300,100,300,100}; 还可以设置灯光.setLights(argb, onMs, offMs)
				.SetOngoing(true)
				.SetContentIntent (resultPendingIntent);

			var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify (0, notificationBuilder.Build());
		}
	}
}

