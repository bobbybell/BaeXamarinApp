using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaeApp
	{
	public class Question
		{
		public string Name { get; private set; }
		public string Q { get; private set; }
		public string[] Answers { get; private set; }
		public int Correct { get; private set; }

		public Question(KeyValuePair<string, JToken> x)
			{
			Name = x.Key;
			JToken data = x.Value;
			Q = data["question"].ToString();
			Correct = (int)data["correct"];

			List<string> answerList = new List<string>();
			JArray answers = (JArray)data["answers"];
			foreach (JValue answer in answers) {
				answerList.Add(answer.ToString());
				}
			Answers = answerList.ToArray();
			}
		public static Question[] Parse(Newtonsoft.Json.Linq.JObject data)
			{
			List<Question> qlist = new List<Question>();
			foreach (KeyValuePair<string,JToken> x in data) {
				Question q = new Question(x);
				qlist.Add(q);
				}

			return qlist.ToArray();
			}
		}
	}
