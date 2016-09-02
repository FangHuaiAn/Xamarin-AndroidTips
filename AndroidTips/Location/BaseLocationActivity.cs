using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Android.OS;
using Android.App;
using Android.Widget;
using Android.Locations;

using static System.Diagnostics.Debug;

namespace AndroidTips
{
	[Activity (Label = "BaseLocationActivity")]
	public class BaseLocationActivity : Activity, ILocationListener
	{
		private LocationManager _locationManager;
		private string _locationProvider;
		private Location _currentLocation;

		TextView lbAddress;
		TextView lbLocation;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			//

			SetContentView (Resource.Layout.BaseLocationView);

			lbLocation = FindViewById<TextView> (Resource.Id.location_baselocationview_lbLocation);
			lbAddress = FindViewById<TextView> (Resource.Id.location_baselocationview_lbAddress);
			var btnAddress = FindViewById<Button> (Resource.Id.location_baselocationview_btnAddress);
			btnAddress.Click += async (sender, e) => {

				if (null == _currentLocation) {
					return;
				}

				Address address = await ReverseGeocodeCurrentLocation ();
				DisplayAddress (address);

			};

			// Create your application here
			InitializeLocationManager ();
		}

		private void InitializeLocationManager ()
		{
			_locationManager = (LocationManager)GetSystemService (LocationService);
			Criteria criteriaForLocationService = new Criteria {
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, true);

			if (acceptableLocationProviders.Any ()) {
				_locationProvider = acceptableLocationProviders.First ();
			} else {
				_locationProvider = string.Empty;
			}
			WriteLine ($"Using { _locationProvider }.");
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			_locationManager.RequestLocationUpdates (_locationProvider, 0, 0, this);
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			_locationManager.RemoveUpdates (this);
		}

		#region ILocationListener

		public void OnLocationChanged (Location location) {

			_currentLocation = location;

			WriteLine ($"OnLocationChanged 緯度:{_currentLocation.Latitude}; 經度:{_currentLocation.Longitude}");
		
			RunOnUiThread (() => {
				lbLocation.Text = $"OnLocationChanged 緯度:{_currentLocation.Latitude}; 經度:{_currentLocation.Longitude}";
			});
		}

		public void OnProviderDisabled (string provider) { 
			WriteLine ($"{provider} Disabled");
		}

		public void OnProviderEnabled (string provider) {
			WriteLine ($"{provider} Enabled" );
		}

		public void OnStatusChanged (string provider, Availability status, Bundle extras) {
			switch (status) {
				case Availability.Available: { 
					WriteLine ($"Location Service Available");
				}
				break;
				case Availability.OutOfService: { 
					WriteLine ($"Location Service OutOfService");
				}
				break;
				case Availability.TemporarilyUnavailable: { 
					WriteLine ($"Location Service TemporarilyUnavailable");
				}
				break;
			
			}
		}

		#endregion

		#region Geocode

		private async Task<Address> ReverseGeocodeCurrentLocation ()
		{
			Geocoder geocoder = new Geocoder (this);
			IList<Address> addressList =
				await geocoder.GetFromLocationAsync (_currentLocation.Latitude, _currentLocation.Longitude, 10);

			Address address = addressList.FirstOrDefault ();
			return address;
		}

		void DisplayAddress (Address address)
		{
			if (address != null) {
				var deviceAddress = new StringBuilder ();
				for (int i = 0; i < address.MaxAddressLineIndex; i++) {
					deviceAddress.AppendLine (address.GetAddressLine (i));
				}

				RunOnUiThread (() => { 
					lbAddress.Text = deviceAddress.ToString ();
				});


			} 
			else {

				RunOnUiThread (() => {
					lbAddress.Text = "Waiting";
				});
			}
		}

		#endregion
	}
}

