using System.IO;
using System.Threading.Tasks;

using Android.OS;
using Android.App;
using Android.Widget;

using static System.Diagnostics.Debug;

namespace AndroidTips
{
	[Activity (Label = "FileActivity")]
	public class FileActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.FileView);

			var worker = new StorageService ();

			var btnWrite = FindViewById<Button> (Resource.Id.data_fileview_btnWrite);
			btnWrite.Click += async (sender, e) => {

				WriteLine ($"Start:{ System.DateTime.Now.ToLongTimeString () }");

				await worker.SaveTextAsync ("io.txt", $"{ System.DateTime.Now.ToLongTimeString() }");

				WriteLine ($"Stop:{ System.DateTime.Now.ToLongTimeString () }");
			};

			var btnRead = FindViewById<Button> (Resource.Id.data_fileview_btnRead);
			btnRead.Click += async (sender, e) => {
				WriteLine ($"Start:{ System.DateTime.Now.ToLongTimeString () }");
				var result = await worker.LoadTextAsync ("io.txt");
				WriteLine ($"Stop:{ System.DateTime.Now.ToLongTimeString () }");

				WriteLine ($"result:{result}");
			};

		}
	}

	public class StorageService 
	{
		#region ISaveAndLoad implementation

		public async Task SaveTextAsync (string filename, string text)
		{
			string path = CreatePathToFile (filename);
			using (StreamWriter sw = File.CreateText (path))
				await sw.WriteAsync (text);
		}

		public async Task<string> LoadTextAsync (string filename)
		{
			var result = string.Empty;
			string path = CreatePathToFile (filename);

			using (var file = new FileStream (path, FileMode.Open)) {
				using (var reader = new StreamReader (file)) {

					result = await reader.ReadToEndAsync ();
				}
			}

			return result;
		}

		private async Task<string> ReadFileText (StreamReader reader)
		{
			return await Task.Run (() => reader.ReadToEndAsync ()).ConfigureAwait (false);
		}

		public bool FileExists (string filename)
		{
			return File.Exists (CreatePathToFile (filename));
		}

		#endregion

		string CreatePathToFile (string filename)
		{
			var docsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);

			var path = Path.Combine (docsPath, filename);

			WriteLine ($"path:{path}");

			return path;
		}
	}
}

