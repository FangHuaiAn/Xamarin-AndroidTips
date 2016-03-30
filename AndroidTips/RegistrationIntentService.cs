using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Android.OS;
using Android.App;
using Android.Util;
using Android.Content;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using Android.Gms.Common;

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
				lock (locker)
				{
					var instanceID = InstanceID.GetInstance (this);
					var token = instanceID.GetToken ("783702026879", GoogleCloudMessaging.InstanceIdScope, null);

					Log.Info ("AndroidTips", "GCM Registration Token: " + token);
					SendRegistrationToAppServer (token);
					Subscribe (token);

					// If receiving "Not Registered" failures when sending a notification
					// remove token and instanceId

					//instanceID.DeleteToken( token, GoogleCloudMessaging.InstanceIdScope );
					//instanceID.DeleteInstanceID();
				}
			}
			catch (Exception ex)
			{
				Log.Debug("AndroidTips", "Failed to get a registration token" + ex.Message);
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

	}
}

