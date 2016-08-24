
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace AndroidTips
{
	[Activity (Label = "TouchActivity")]
	public class TouchActivity : Activity, View.IOnTouchListener
	{
		private Button _myButton;
		private float _viewX;
		private float _viewY;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView (Resource.Layout.TouchDemoView);

			_myButton = FindViewById<Button> (Resource.Id.touchView);
			_myButton.SetOnTouchListener (this);
		}

		public bool OnTouch (View v, MotionEvent e)
		{
			switch (e.Action) {
			case MotionEventActions.Down:
				_viewX = e.GetX ();
				_viewY = e.GetY ();
				break;
			case MotionEventActions.Move:
				var left = (int)(e.RawX - _viewX);
				var right = left + v.Width;
				var top = (int)(e.RawY - _viewY);
				var bottom = top + v.Height;

				v.Layout (left, top, right, bottom);
				break;
			}
			return true;
		}
	}
}

