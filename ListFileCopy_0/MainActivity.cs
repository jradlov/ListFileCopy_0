using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using Android.Util;
using System.IO;
using System;

// set Permissions in Manifest:
//	<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
//	<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
// ALSO on device: Settings/Apps/ListFileCopy_0 Permissions:Storage


// ListView_1 creates a Custom Adapter for lists of single lines of text.
// If we want more than one line of text we have to create a Custom Row.
// A Custom Row is a layout: we create custom_row.axml in Resources/layout.
// Implement the Custom Row in MyAdapter GetView() method.

// Steps for Custom Row:
// 1) design Custom Row in axml file under Rwsources/layout
// 2) in GetView method in custom adapter, inflate Custom Row you designed
//    view = context.LayoutInflater.Inflate(Resource.Layout.custom_row, null);
// 3) Find views you want to populate with data
// 4) Populate them
//    view.FindViewById<TextView>(Resource.Id.textViewCustomRow).Text = item;


// Create a custom adapter: MyAdapter class
// Custom Adapter works the same as SimpleListItem1 from ListView_0
// but now can add code for changing rows for ads/purchase


namespace ListFileCopy_0 
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity 
	{
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			// create data set for listview
			var data = new List<string>();
			for (int i = 0; i < 100; i++)
				data.Add("Item number: " + i);

			// reference listView with layout resource
			var listView = FindViewById<ListView>(Resource.Id.listView1);
			listView.FastScrollEnabled = true;  // enable vertical scrollbar 

			// create custom adapter and apply it to listView
			var adapter = new MyAdapter(this, data);
			listView.Adapter = adapter;



			var btnCopy = FindViewById<Button>(Resource.Id.btnCopy);
			btnCopy.Click += btnCopy_Click;
		}

		private void btnCopy_Click(object sender, EventArgs e) {
			FileUtilities fu = new FileUtilities();
			string downloadFolder = fu.DownloadFolder;

			if(fu.IsExternalStorageReady()) {
				var filePath = Path.Combine(downloadFolder, "test123.txt");
				if (File.Exists(filePath)) File.Delete(filePath);
				File.WriteAllText(filePath, "is this text being written to the mito file ???");
			} else
				Toast.MakeText(this, "External Storage not ready!", ToastLength.Short).Show(); 
		}

	}
}

