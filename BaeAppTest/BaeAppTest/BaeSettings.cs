using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using IAScience;
using Newtonsoft.Json;
 
namespace BaeApp
	{
	public class BaeSettings
		{
		Newtonsoft.Json.Linq.JObject m_LoadedConfig; // the original JObject created from the config.json file
		public Question[] m_Questions;
		public Dictionary<string, Question> m_QuestionsDictionary;

		public static string AppDataFolder()
			{
			string d = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			return d.IndexOf("thomstream", StringComparison.OrdinalIgnoreCase) >= 0 ? d : Path.Combine(d, "thomstream");
			}
		//public static string ConfigFile {
		//	get {
		//		return Path.Combine(AppDataFolder(), "config.json");
		//		}
		//	}

		public BaeSettings()
			{
			Load();
			}
		public void Load()
			{
			//string configFile = Path.Combine(AppDataFolder(), "config.json");
			//var assembly = Assembly.GetExecutingAssembly();
			using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("BaeApp.config.config.json")) {
				using (StreamReader reader = new StreamReader(stream)) {
					string filetext = reader.ReadToEnd();
					m_LoadedConfig = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(filetext);
					SetQuestions((Newtonsoft.Json.Linq.JObject)m_LoadedConfig["questions"]);
					}
				}

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
