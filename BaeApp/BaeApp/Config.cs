using System;
using System.Collections.Generic;
using System.Text;
using IAScience;

namespace BaeApp
	{
	public class Config
		{
		public string DefaultVideoExtension { get; set; } = "";
		public string RetryOption { get; set; } = "";

		public Config(string[] configLines)
			{
			// Remove comments to make subsequent processing easier
			int count = configLines.Length;
			for (int n = 0; n < count; n++) {
				string s = configLines[n].Trim();
				int index = s.IndexOf('#');
				if (index == 0)
					configLines[n] = "";
				else if (index > 0)
					configLines[n] = s.Substring(0, index).Trim();
				}

			Dictionary<string, string> streamDefinitions = new Dictionary<string, string>();
			Dictionary<string, string> groupDefinitions = new Dictionary<string, string>();
			List<Question> questions = new List<Question>();

			for (int n = 0; n < count; n++) {
				string s = configLines[n];
				if (s.Length == 0)
					continue;

				if (s.StartsWith("video_default_extension "))
					DefaultVideoExtension = ParamValue(s);
				else if (s.StartsWith("retry_option "))
					RetryOption = ParamValue(s);
				else if (s.StartsWith("stream "))
					AddDefinition(configLines, ref n, streamDefinitions);
				else if (s.StartsWith("group "))
					AddDefinition(configLines, ref n, groupDefinitions);
				else if (s.StartsWith("question "))
					AddQuestion(configLines, ref n, questions);
				}

			// Now replace group items in streamDefinitions with the group text
			}
		private void AddQuestion(string[] lines, ref int lineIndex, List<Question> questions)
			{
			//question abrasive
			//Before using an abrasive wheel what should you check?
			//Your supervisor is watching you | *The safety guard is in place | You have a Permit to Work
			//Question q = new Question();
			string qname = ParamValue(lines[lineIndex]);
			lineIndex++;


			}

		private void AddDefinition(string[] lines, ref int lineIndex, Dictionary<string, string> streamDefinitions)
			{
			int count = lines.Length;
			string streamName = ParamValue(lines[lineIndex]);
			lineIndex++;

			StringBuilder sb = new StringBuilder();
			while ((lineIndex < count) && (lines[lineIndex].Length > 0))
				sb.Append(lines[lineIndex++].Trim());

			streamDefinitions.Add(streamName, sb.ToString());
			}
		//private void AddGroupDefinition(string[] lines, int lineIndex, Dictionary<string, string> groupDefinitions)
		//	{
		//	int count = lines.Length;

		//	}

		private string ParamValue(string line)
			{
			int index = line.IndexOf(' ');
			return index < 0 ? "" : Utility.SafeSubstring(line, index + 1).Trim();
			}
		}

	public class LearnerStream
		{
		public string Name { get; set; } = "";
		public string[] Items { get; set; } = null;
		}
	}
