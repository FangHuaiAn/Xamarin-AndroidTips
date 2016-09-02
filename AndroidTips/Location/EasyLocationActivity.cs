
using System;
using System.Threading.Tasks;

using Android.OS;
using Android.App;
using Android.Widget;

using Geolocator.Plugin;
using Geolocator.Plugin.Abstractions;

using static System.Diagnostics.Debug;

namespace AndroidTips
{
	[Activity (Label = "EasyLocationActivity")]
	public class EasyLocationActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.EasyLocationView);

			var lbLocation = FindViewById<TextView> (Resource.Id.location_easylocationview_lbLocation);
			var btnLocation = FindViewById<Button> (Resource.Id.location_easylocationview_btnLocation);
			btnLocation.Click += async (sender, e) => {

				try {
					var position = await ReadLocation ();
					lbLocation.Text = $"緯度:{position.Latitude}; 經度:{position.Longitude}";
					WriteLine ($"緯度:{position.Latitude}; 經度:{position.Longitude}");
				}
				catch (TaskCanceledException ex) {
					WriteLine (ex.Message);
				}



			};
		}

		private async Task<Position> ReadLocation () { 

			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			var position = await locator.GetPositionAsync (timeoutMilliseconds: 10000);

			Console.WriteLine ("Position Status: {0}", position.Timestamp);
			Console.WriteLine ("Position Latitude: {0}", position.Latitude);
			Console.WriteLine ("Position Longitude: {0}", position.Longitude);

			return position;
		}
	}
}

