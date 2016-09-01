using Android.OS;
using Android.App;
using Android.Widget;
using Android.Content;
using Android.Preferences;

using static System.Diagnostics.Debug;

namespace AndroidTips
{
	[Activity (Label = "SharedPreferenceActivity")]
	public class SharedPreferenceActivity : Activity
	{
		
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.SharedPrefenceView);

			var btnRead = FindViewById<Button> (Resource.Id.data_sharedpreference_btnRead);
			btnRead.Click += (sender, e) => {
				var result = ReadFromPreference ();

				WriteLine ($"{result}");
			};

			var btnWrite = FindViewById<Button> (Resource.Id.data_sharedpreference_btnWrite);
			btnWrite.Click += (sender, e) => {
				WritePreference ();
			};

		}

		private void WritePreference () {

			// Custom ;
			ISharedPreferences privatePref = GetSharedPreferences ("myPrefs", FileCreationMode.Private);
			ISharedPreferencesEditor privateEditor = privatePref.Edit ();

			privateEditor.PutString ("prefDemo", "Hello, Xamarin from myPrefs");
			privateEditor.Apply ();

			// Default
			ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences (Application.Context);
			ISharedPreferencesEditor editor = pref.Edit ();

			editor.PutString ("prefDemo", "Hello, Xamarin from default");
			editor.Apply ();

		}

		private string ReadFromPreference ()
		{
			// Custom
			ISharedPreferences privatePref = GetSharedPreferences ("myPrefs", FileCreationMode.Private);

			var test = privatePref.GetString ("prefDemo", "error");
			WriteLine ($"{test}");

			// Default
			ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences (Application.Context);

			return pref.GetString ("prefDemo", "error");

		}
	}
}

