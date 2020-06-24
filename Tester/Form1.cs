using BaeApp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
	{
	public partial class Form1 : Form
		{
		Config m_Config;

		public Form1()
			{
			InitializeComponent();
			}

		private void testButton_Click(object sender, EventArgs e)
			{
			string srcFile = @"F:\MyProjects\BaeXamarinApp\BaeApp\BaeApp\config\config.txt";
			m_Config = new Config(File.ReadAllLines(srcFile));
			}
		}
	}
