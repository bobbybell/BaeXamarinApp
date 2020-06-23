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
		Newtonsoft.Json.Linq.JObject m_LoadedConfig;
		public Question[] m_Questions;
		public Dictionary<string, Question> m_QuestionsDictionary;

		public Form1()
			{
			InitializeComponent();
			}

		private void testButton_Click(object sender, EventArgs e)
			{
			string srcFile = @"F:\MyProjects\BaeXamarinApp\BaeAppTest\BaeAppTest\config\config.json";
			string filetext = File.ReadAllText(srcFile);
			m_LoadedConfig = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(filetext);
			SetQuestions((Newtonsoft.Json.Linq.JObject)m_LoadedConfig["questions"]);
			}
		private void SetQuestions(Newtonsoft.Json.Linq.JObject questions)
			{
			m_Questions = Question.Parse(questions);
			m_QuestionsDictionary = new Dictionary<string, Question>();
			foreach (Question q in m_Questions)
				m_QuestionsDictionary[q.Name] = q;
			}
		}
	}
