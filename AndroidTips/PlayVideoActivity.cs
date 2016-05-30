using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Android.OS;
using Android.App;
using Android.Media;
using Android.Views;
using Android.Widget;
using Android.Runtime;
using Android.Content;

namespace AndroidTips
{
	[Activity (Label = "PlayVideoActivity")]			
	public class PlayVideoActivity : Activity,MediaPlayer.IOnPreparedListener,ISurfaceHolderCallback
	{
		MediaPlayer _player;
		VideoView _videoView;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.PlayVideoView);

			_videoView = FindViewById<VideoView> (Resource.Id.videoView);


			play ("sample_iPod.m4v");  
		}

		void play(string fullPath)
		{
			ISurfaceHolder holder = _videoView.Holder;
			holder.SetType (SurfaceType.PushBuffers);
			holder.AddCallback (this);

			_player = new MediaPlayer();

			Android.Content.Res.AssetFileDescriptor afd = this.Assets.OpenFd(fullPath);
			if (afd != null)
			{
				_player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
				_player.Prepare ();
				_player.Start();
			}
		}

		public void SurfaceCreated (ISurfaceHolder holder)
		{
			Console.WriteLine ("SurfaceCreated");
			_player.SetDisplay(holder);
		}

		public void SurfaceDestroyed (ISurfaceHolder holder)
		{
			Console.WriteLine ("SurfaceDestroyed");
		}

		public void SurfaceChanged (ISurfaceHolder holder, Android.Graphics.Format format, int w, int h)
		{
			Console.WriteLine ("SurfaceChanged");
		}

		public void OnPrepared(MediaPlayer player)
		{

		}
	}
}

