
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
	[Activity (Label = "QRCodeActivity")]			
	public class QRCodeActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.QRCodeView);

			var txtQRResult = FindViewById<EditText> (Resource.Id.txtQRResult);

			var btnScan = FindViewById<Button> (Resource.Id.btnScan);
			btnScan.Click += async (object sender, EventArgs e) => {

				var scanner = new ZXing.Mobile.MobileBarcodeScanner();
				var result = await scanner.Scan();

				if (result != null)
				{
					Console.WriteLine("Scanned Barcode: " + result.Text);

					RunOnUiThread(()=>{

						txtQRResult.Text = result.Text ;

					});

				}



			};
		}
	}
}

