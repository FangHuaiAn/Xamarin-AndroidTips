
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidTips
{
	[Activity (Label = "ImageScaleTypeActivity")]			
	public class ImageScaleTypeActivity : Activity
	{
		ImageView _imageView ;
		Spinner _selectImageViewScaleType ;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// 
			SetContentView( Resource.Layout.ImageScaleTypeView );


			_imageView = FindViewById<ImageView> (Resource.Id.scaleImageView);
			_selectImageViewScaleType = FindViewById <Spinner>( Resource.Id.selectImageScaleType);

			var modes = new[] {
				"Center",
				"CenterCrop",
				"CenterInside",
				"FitCenter",
				"FitEnd",
				"FitStart",
				"FitXy",
				"Matrix"
			};

			_selectImageViewScaleType.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {

				int position = e.Position ;

				switch(position){

				case 0:{_imageView.SetScaleType(ImageView.ScaleType.Center);}break;
				case 1:{_imageView.SetScaleType(ImageView.ScaleType.CenterCrop);}break;
				case 2:{_imageView.SetScaleType(ImageView.ScaleType.CenterInside);}break;
				case 3:{_imageView.SetScaleType(ImageView.ScaleType.FitCenter);}break;
				case 4:{_imageView.SetScaleType(ImageView.ScaleType.FitEnd);}break;
				case 5:{_imageView.SetScaleType(ImageView.ScaleType.FitStart);}break;
				case 6:{_imageView.SetScaleType(ImageView.ScaleType.FitXy);}break;
				case 7:{_imageView.SetScaleType(ImageView.ScaleType.Matrix);}break;
				}



			};

			var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, modes);
			_selectImageViewScaleType.Adapter = adapter;
		}
	}
}

