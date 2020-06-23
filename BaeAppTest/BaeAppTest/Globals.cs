using System;
using System.Collections.Generic;
using System.Text;
using IAScience;
 
namespace BaeApp
	{
	public static class Globals
		{
		//public static event EventHandler<EventArgs> Initialised;

		public static int VideoWidth = 704;
		public static int VideoHeight = 400;
		public static string DirectoryPath = "";

		public static double MainPageWidth { get; set; } = -1;
		public static double MainPageHeight { get; set; } = -1;


		public static bool IsInitialised { get; set; } = false;

		public static BaeSettings m_Settings;

		public static void Initialise()
			{
			m_Settings = new BaeSettings();
			IsInitialised = true;
			}

		public static bool UpdatePageSize(double width, double height)
			{
			if ((width > 1) && (height > 1) && !(Utility.CloseValues(MainPageWidth, width) && Utility.CloseValues(MainPageHeight, height))) {
				MainPageWidth = width;
				MainPageHeight = height;
				return true;
				}
			else
				return false;
			}
		public static void GetVideoSizeForScreen(double maxDisplayWidth, double maxDisplayHeight, out double videoWidth, out double videoHeight, out double xOffs, out double yOffs)
			{
			if (maxDisplayWidth * VideoHeight > VideoWidth * maxDisplayHeight) {
				// fit to height
				videoHeight = maxDisplayHeight;
				videoWidth = videoHeight * VideoWidth / VideoHeight;
				yOffs = 0;
				xOffs = (maxDisplayWidth - videoWidth) / 2;
				}
			else {
				// fit to width
				videoWidth = maxDisplayWidth;
				videoHeight = videoWidth * VideoHeight / VideoWidth;
				yOffs = (maxDisplayHeight - videoHeight)/2;
				xOffs = 0;
				}
			}
		}
	}
