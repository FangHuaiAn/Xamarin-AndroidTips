
using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace AndroidTips
{
	[Activity (Label = "TouchGestureActivity")]
	public class GestureActivity : Activity, GestureDetector.IOnGestureListener
	{
		private TextView _textView;
		private GestureDetector _gestureDetector;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.GestureDemoView);
			_textView = FindViewById<TextView> (Resource.Id.velocity_text_view);
			_textView.Text = "Fling Velocity: ";
			_gestureDetector = new GestureDetector (this);

		}


		#region GestureDetector.IOnGestureListener

		public bool OnDown (MotionEvent e){
			return false;
		}

		public bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY){
			_textView.Text = $"Fling velocity: {velocityX} x {velocityY}" ;
			return true;
		}

		public void OnLongPress (MotionEvent e) { }

		public bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY){
			return false;
		}

		public void OnShowPress (MotionEvent e) { }

		public bool OnSingleTapUp (MotionEvent e){
			return false;
		}

		public override bool OnTouchEvent (MotionEvent e){
			_gestureDetector.OnTouchEvent (e);
			return false;
		}

		#endregion


	}
}

