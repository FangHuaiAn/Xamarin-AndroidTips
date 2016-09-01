using System.IO;
using System.Collections.Generic;

using Android.OS;
using Android.App;
using Android.Widget;

using SQLite;

using static System.Diagnostics.Debug;

namespace AndroidTips
{
	[Activity (Label = "SQLiteActivity")]
	public class SQLiteActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//
			SetContentView (Resource.Layout.SQLiteView);

			SQLiteWorker worker = new SQLiteWorker ("demo.db");
			worker.CreateDatabase ();

			var btnWrite = FindViewById<Button> (Resource.Id.data_sqliteview_btnWrite);
			btnWrite.Click += (sender, e) => {
				
				worker.AddToDoItem (new ToDo { Name = $"TODO:{System.DateTime.Now.ToShortDateString ()}", Description = $"{ System.DateTime.Now.ToShortTimeString ()  }"});

			};

			var btnRead = FindViewById<Button> (Resource.Id.data_sqliteview_btnRead);
			btnRead.Click += (sender, e) => {
				var list = worker.ReadTodoItems ();

				foreach (var todo in list) {
					WriteLine ($"todo:Name{ todo.Name };Description{ todo.Description }");
				}

			};


		}
	}

	public class SQLiteWorker
	{

		public SQLiteWorker (string databaseName)
		{

			DatabasePath = Path.Combine (DocumentsPath, databaseName);

			Connection = new SQLiteConnection (DatabasePath);
		}

		public string DatabasePath { get; set; }

		public SQLiteConnection Connection { get; set; }

		public bool CreateDatabase ()
		{

			Connection.CreateTable<ToDo> ();

			return File.Exists (DatabasePath);
		}


		public void AddToDoItem (ToDo todo)
		{
			Connection.Insert (todo);

			WriteLine ($"Write { todo.Name },{ todo.Description }");
		}

		public List<ToDo> ReadTodoItems ()
		{
			var results = new List<ToDo> ();

			var list = Connection.Table<ToDo> ();

			foreach (var item in list) {
				results.Add (new ToDo {
					Name = item.Name,
					Description = item.Description
				});

			}

			return results;

		}

		public static string DocumentsPath {
			get {
				var docsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
				WriteLine ($"documentsPath:{ docsPath }");

				return docsPath;
			}
		}

	}

	public class ToDo
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[MaxLength (25)]
		public string Name { get; set; }


		[MaxLength (50)]
		public string Description { get; set; }
	}
}

