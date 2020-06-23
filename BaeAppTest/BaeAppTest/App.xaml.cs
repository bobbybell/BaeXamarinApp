using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaeApp
	{
	public partial class App : Application
		{
		public App(string s = "")
			{
			InitializeComponent();
			Globals.DirectoryPath = s;
			Globals.Initialise();

			MainPage = new MainPage();
			}

		protected override void OnStart()
			{
			}

		protected override void OnSleep()
			{
			}

		protected override void OnResume()
			{
			}
		}
	}
