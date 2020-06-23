using MediaManager;
using MediaManager.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace BaeApp
	{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
		{
		private bool m_PageBuilt = false;
		private bool m_FirstAppearance = true;
		private AbsoluteLayout m_MainLayout;

		public MainPage()
			{
			InitializeComponent();
			}
		protected override void OnSizeAllocated(double width, double height)
			{
			base.OnSizeAllocated(width, height);

			if (Globals.UpdatePageSize(width, height) || !m_PageBuilt) { // i.e. if the global page size values have changed
				m_PageBuilt = true;
				BuildPage();

				if (m_FirstAppearance)
					m_FirstAppearance = false;
				}
			}

		private object m_BuildPageLock = new object();
		private void BuildPage()
			{
			lock (m_BuildPageLock)
				_BuildPage();
			}
		private void _BuildPage()
			{
			double width = Globals.MainPageWidth;
			double height = Globals.MainPageHeight;

			m_MainLayout = new AbsoluteLayout() { BackgroundColor = Xamarin.Forms.Color.GreenYellow };
			AbsoluteLayout.SetLayoutBounds(m_MainLayout, new Xamarin.Forms.Rectangle(0, 0, 1, 1));
			AbsoluteLayout.SetLayoutFlags(m_MainLayout, AbsoluteLayoutFlags.All);
			this.Content = m_MainLayout;

			VideoView videoView = new VideoView();
			double videoWidth, videoHeight, xOffs, yOffs;
			Globals.GetVideoSizeForScreen(Math.Max(width, height), Math.Min(width, height), out videoWidth, out videoHeight, out xOffs, out yOffs);
			AbsoluteLayout.SetLayoutBounds(videoView, new Xamarin.Forms.Rectangle(xOffs, yOffs, videoWidth, videoHeight));
			AbsoluteLayout.SetLayoutFlags(videoView, AbsoluteLayoutFlags.None);
			m_MainLayout.Children.Add(videoView);

			Button playButton = new Button() {
				//Margin = new Thickness(0, 10, 0, 0),
				Padding = new Thickness(0, 0, 0, 0),
				//FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
				//FontFamily = "Metropolis-SemiBold",
				Text = "Play",
				//HorizontalOptions = LayoutOptions.Center,
				//TextColor = loginBtnTextColor,
				//BackgroundColor = loginBtnBgColor,
				//Name = "loginButton"
				};
			playButton.Clicked += PlayButton_Clicked;

			double size = Math.Min(width, height);
			double btnWidth = size * 0.2;
			double btnHeight = btnWidth * 0.5;
			AbsoluteLayout.SetLayoutBounds(playButton, new Xamarin.Forms.Rectangle(0, 0, btnWidth, btnHeight));
			AbsoluteLayout.SetLayoutFlags(playButton, AbsoluteLayoutFlags.None);
			m_MainLayout.Children.Add(playButton);

			Button pauseButton = new Button() {
				Text = "Toggle",
				};
			pauseButton.Clicked += PauseButton_Clicked;

			AbsoluteLayout.SetLayoutBounds(pauseButton, new Xamarin.Forms.Rectangle(btnWidth + 10, 0, btnWidth, btnHeight));
			AbsoluteLayout.SetLayoutFlags(pauseButton, AbsoluteLayoutFlags.None);
			m_MainLayout.Children.Add(pauseButton);

			Button testButton = new Button() {
				Text = "Test",
				};
			testButton.Clicked += TestButton_Clicked;

			AbsoluteLayout.SetLayoutBounds(testButton, new Xamarin.Forms.Rectangle(2.0 * (btnWidth + 10), 0, btnWidth, btnHeight));
			AbsoluteLayout.SetLayoutFlags(testButton, AbsoluteLayoutFlags.None);
			m_MainLayout.Children.Add(testButton);


			//PlayVideo();
			}

		private void TestButton_Clicked(object sender, EventArgs e)
			{
			
			DisplayAlert("Android path", Globals.DirectoryPath, "OK");
			return;

			//var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);

			//string dstFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "data.txt");
			//DisplayAlert("Msg", dstFile, "OK");
			//string t = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			//DisplayAlert("SpecialFolder.CommonApplicationData", t, "OK");
			//try {
			//	Directory.CreateDirectory(t);
			//	File.WriteAllText(Path.Combine(t,"test.txt"), "Testing\nTesting");
			//	}
			//catch (Exception ex) {
			//	DisplayAlert("Exception", ex.Message, "OK");
			//	}

			//t = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			//DisplayAlert("SpecialFolder.Personal", t, "OK");


			//string dstFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.txt");
			//DisplayAlert("SpecialFolder.MyDocuments", dstFile, "OK");


			//		/storage/emulated/0/Android/data/com.iascience.baeapp/files
			//DisplayAlert("Test string", Globals.TestString, "OK");

			//string dstFile = Path.Combine(Globals.TestString, "data.txt");
			string folder = "/storage/emulated/0/Documents";
			string dstFile = "/storage/emulated/0/Documents/data.txt";

			try {
				if (!Directory.Exists(folder))
					Directory.CreateDirectory(folder);
				File.WriteAllText(dstFile, "Testing\nTesting");
				string s = File.ReadAllText(dstFile);
				DisplayAlert("File read", s, "OK");
				}
			catch (Exception ex) {
				DisplayAlert("Exception", ex.Message, "OK");
				}
			}

		private void PlayButton_Clicked(object sender, EventArgs e)
			{
			PlayVideo();
			}

		private void PauseButton_Clicked(object sender, EventArgs e)
			{
			StopVideo();
			}

		private void StopVideo()
			{
			//CrossMediaManager.Current.Pause();
			CrossMediaManager.Current.PlayPause();
			}

		private void PlayVideo()
			{
			string filename = "introduction_zoe.mp4";

			if (Device.RuntimePlatform == Device.Android) {
				//await CrossMediaManager.Current.Play("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4");
				//await CrossMediaManager.Current.PlayFromResource("Assets:///introduction_zoe.mp4");
				//await CrossMediaManager.Current.Play("file:///android_asset/introduction_zoe.mp4");
				//CrossMediaManager.Current.Play("file:///android_asset/minions.mp4");
				CrossMediaManager.Current.Play("file:///android_asset/" + filename);
				}
			else if (Device.RuntimePlatform == Device.iOS) {
				CrossMediaManager.Current.PlayFromResource(filename);
				}

			CrossMediaManager.Current.Volume.CurrentVolume = 100;
			}

		}
	}
