using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Android.App;
using Android.Util;
using Android.Content;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;

using Debug = System.Diagnostics.Debug;

namespace AndroidTips
{
	// Sender ID:783702026879
	// Server API Key :AIzaSyD9-hOco-LBJo_rZ5u3N4JXtkJ6HoFdxdI

	[Service(Exported = false)]
	public class RegistrationIntentService : IntentService
	{
		static object locker = new object();

		public RegistrationIntentService() : base("RegistrationIntentService") { }

		protected override void OnHandleIntent (Intent intent)
		{
			try
			{
				Log.Info ("AndroidTips", "Calling InstanceID.GetToken");
				lock (locker)
				{
					var instanceID = InstanceID.GetInstance (this);
					var token = instanceID.GetToken (
						"783702026879", GoogleCloudMessaging.InstanceIdScope, null);

					Log.Info ("AndroidTips", "GCM Registration Token: " + token);
					SendRegistrationToAppServer (token);
					Subscribe (token);
				}
			}
			catch (Exception e)
			{
				Log.Debug("AndroidTips", "Failed to get a registration token");
				return;
			}
		}

		void SendRegistrationToAppServer (string token)
		{
			// Add custom implementation here as needed.
			Debug.WriteLine("token:"+token);
		}

		void Subscribe (string token)
		{
			var pubSub = GcmPubSub.GetInstance(this);
			pubSub.Subscribe(token, "/topics/global", null);
		}

		/*
		[InstanceID] token: d7ZLT7Y8AEk:APA91bFQ1FMj0nrSrHnC52QG5P-YX2KFLLJNCSQbfj-nwKY5FbnIj1DzJaw9iSsk1Sh3v2quO8hiLVX1Is5kv2zOB7grKJFktbQ-p22NfxLALSRYgHRJt3-hhXvlu-yQjSNYpFpvaP8v
		token:d7ZLT7Y8AEk:APA91bFQ1FMj0nrSrHnC52QG5P-YX2KFLLJNCSQbfj-nwKY5FbnIj1DzJaw9iSsk1Sh3v2quO8hiLVX1Is5kv2zOB7grKJFktbQ-p22NfxLALSRYgHRJt3-hhXvlu-yQjSNYpFpvaP8v
		[AndroidTips] GCM Registration Token: d7ZLT7Y8AEk:APA91bFQ1FMj0nrSrHnC52QG5P-YX2KFLLJNCSQbfj-nwKY5FbnIj1DzJaw9iSsk1Sh3v2quO8hiLVX1Is5kv2zOB7grKJFktbQ-p22NfxLALSRYgHRJt3-hhXvlu-yQjSNYpFpvaP8v
		token:d7ZLT7Y8AEk:APA91bFQ1FMj0nrSrHnC52QG5P-YX2KFLLJNCSQbfj-nwKY5FbnIj1DzJaw9iSsk1Sh3v2quO8hiLVX1Is5kv2zOB7grKJFktbQ-p22NfxLALSRYgHRJt3-hhXvlu-yQjSNYpFpvaP8v
		[InstanceID] token: 
		*/

	}
}

