using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using IAScience;
using Newtonsoft.Json;

/* The config.json file defines 
- Questions - each question is a question, several answers plus which one is correct
- Streams. Each stream is a sequence of items that are shown to the user. Item are mainly videos, questions or questionsets, may be other things (e.g. login, results)
- A questionset is a group of questions, only one of which is asked - e.g. if the user can retry then the first question is asked on the forst pass, the second on the next
- Login information (yet to be developed) which may be a list of what information the user is asked for in order to login


*/
namespace BaeApp
	{
	public class BaeSettings
		{
		Newtonsoft.Json.Linq.JObject m_LoadedConfig; // the original JObject created from the config.json file
		public Question[] m_Questions;
		public Dictionary<string, Question> m_QuestionsDictionary;

		Config m_Config = null;

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
		private void Load()
			{
			//System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
			//string name = a.FullName;
			string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

			using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(assemblyName + ".config.config.txt")) {
				using (StreamReader streamReader = new StreamReader(stream)) {
					string filetext = streamReader.ReadToEnd();
					using (StringReader stringReader = new StringReader(filetext)) {
						string[] lines = EnumerateLines(stringReader).ToArray();
						m_Config = new Config(lines);
						}
					}
				}
			}

		private IEnumerable<string> EnumerateLines(TextReader reader)
			{
			string line;

			while ((line = reader.ReadLine()) != null) {
				yield return line;
				}
			}
		//private void Load1()
		//	{
		//	using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("BaeApp.config.config.json")) {
		//		using (StreamReader reader = new StreamReader(stream)) {
		//			string filetext = reader.ReadToEnd();
		//			m_LoadedConfig = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(filetext);
		//			SetQuestions((Newtonsoft.Json.Linq.JObject)m_LoadedConfig["questions"]);
		//			}
		//		}

		//	}
		//private void SetQuestions(Newtonsoft.Json.Linq.JObject questions)
		//	{
		//	m_Questions = Question.Parse(questions);
		//	m_QuestionsDictionary = new Dictionary<string, Question>();
		//	foreach (Question q in m_Questions)
		//		m_QuestionsDictionary[q.Name] = q;
		//	}
		}
	}
