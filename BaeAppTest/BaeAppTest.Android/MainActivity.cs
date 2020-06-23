using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MediaManager;
using System.Drawing;
using System.IO;
using Android.Content;
using Android.Provider;
using Java.IO;
using System.Collections.Generic;
 
namespace BaeApp.Droid
	{
	[Activity(Label = "BaeApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme",
	MainLauncher = true,
	ScreenOrientation = ScreenOrientation.Landscape
	/*ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation*/)]

	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
		{
		protected override void OnCreate(Bundle savedInstanceState)
			{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			CrossMediaManager.Current.Init();

			//Test2();

			string s = ApplicationContext.GetExternalFilesDir(null).AbsolutePath;
			LoadApplication(new App(s));

			//Environment.Ex
			//var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);

			}
		private void Test2()
			{
			// Android.Content.Context.GetExternalFilesDir(string type)
			//string xx = Android.OS.Environment.GetExternalStorageState; // (Android.OS.Environment.MediaMounted);
			//string docsFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
			//List<string> storageLocations = GetAvaliableStorage(ApplicationContext);
			List<string> storageLocations = GetAvaliableStorage(ApplicationContext);
			if (storageLocations.Count < 1)
				return;

			//var str = this.GetExternalFilesDir(null).AbsolutePath;
			string loc = storageLocations[storageLocations.Count - 1];

			string directory = loc + "/Android/data/" + this.PackageName + "/files/Pictures";
			Java.IO.File file = new Java.IO.File(directory);
			if (!file.Exists()) {
				file.Mkdirs();
				}
			// end
			Java.IO.File sdCardPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
			//  filePath: /storage/11E3-2116/Android/data/com.companyname.cropsample/files/Pictures

			Java.IO.File[] files = sdCardPath.ListFiles();

			if (files.Length == 0)
				return;

			string src = files[0].Path;

			string dst = Path.Combine(directory, files[0].Name);
			//string originPath = Path.Combine(sdCardPath.AbsolutePath, "test.png");
			//Android.Util.Log.Error("lv", destPath);

			try {
				FileOutputStream fos = new FileOutputStream(dst, false);
				FileInputStream fis = new FileInputStream(src);
				int b;
				byte[] d = new byte[1024 * 1024];
				while ((b = fis.Read(d)) != -1) {
					fos.Write(d, 0, b);
					}
				fos.Flush();
				fos.Close();
				fis.Close();
				}
			catch (Exception) {
				}
			}

		public List<string> GetAvaliableStorage(Context context)
			{
			List<string> list = null;
			try {

				var storageManager = (Android.OS.Storage.StorageManager)context.GetSystemService(Context.StorageService);

				var volumeList = (Java.Lang.Object[])storageManager.Class.GetDeclaredMethod("getVolumeList").Invoke(storageManager);

				list = new List<string>();

				foreach (var storage in volumeList) {
					Java.IO.File info = (Java.IO.File)storage.Class.GetDeclaredMethod("getPathFile").Invoke(storage);

					if ((bool)storage.Class.GetDeclaredMethod("isEmulated").Invoke(storage) == false && info.TotalSpace > 0) {
						list.Add(info.Path);
						}
					}
				}
			catch (Exception e) {

				}
			return list;
			}
		private void Test1()
			{
			Java.IO.File sdCardPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
			//  filePath: /storage/11E3-2116/Android/data/com.companyname.cropsample/files/Pictures
			string destPath = Path.Combine("/storage/11E3-2116/Android/data/com.companyname.cropsample/files/Pictures/", "test.png");
			string originPath = Path.Combine(sdCardPath.AbsolutePath, "nULSa.png");
			Android.Util.Log.Error("lv", destPath);
			FileOutputStream fos = new FileOutputStream(destPath, false);
			FileInputStream fis = new FileInputStream(originPath);
			int b;
			byte[] d = new byte[1024 * 1024];
			while ((b = fis.Read(d)) != -1) {
				fos.Write(d, 0, b);
				}
			fos.Flush();
			fos.Close();
			fis.Close();
			}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
			{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			}

		public void ExportBitmapAsPNG(Bitmap bitmap)
			{

			// Get/Create Album Folder To Save To
			var jFolder = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "MyAppNamePhotoAlbum");
			if (!jFolder.Exists())
				jFolder.Mkdirs();

			var jFile = new Java.IO.File(jFolder, "MyPhoto.jpg");

			// Save File
			using (var fs = new FileStream(jFile.AbsolutePath, FileMode.CreateNew)) {
				bitmap.Save(jFile.AbsolutePath);
				// bitmap.Compress(Bitmap.CompressFormat.Png, 100, fs);
				}

			// Save Picture To Gallery
			var intent = new Intent(MediaStore.ActionImageCapture);
			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(jFile));
			StartActivityForResult(intent, 0);

			// Request the media scanner to scan a file and add it to the media database
			var f = new Java.IO.File(jFile.AbsolutePath);
			intent = new Intent(Intent.ActionMediaScannerScanFile);
			intent.SetData(Android.Net.Uri.FromFile(f));
			Application.Context.SendBroadcast(intent);

			}
		}
	}