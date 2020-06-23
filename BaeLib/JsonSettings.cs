using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
 
namespace IAScience
	{
	public abstract class JsonSettings
		{
		// override LoadValues in a derived class to set values from the loaded json
		protected abstract void LoadValues(Newtonsoft.Json.Linq.JObject jsonValues, out bool updateSettingsFile);

		private string m_LoadedFile = "";

		public string LoadedFile()
			{
			return m_LoadedFile;
			}

		public JsonSettings()
			{
			}
		//private void CopyFrom(JsonSettings src)
		//	{
		//	User = src.User;
		//	Password = src.Password;
		//	}

		protected string StringValue(Newtonsoft.Json.Linq.JObject jsonValues, string name, string defaultValue)
			{
			return jsonValues.ContainsKey(name) ? (string)jsonValues[name] : defaultValue;
			}


		public bool Load(string filename, out string errMsg)
			{
			errMsg = "";

			try {
				string filetext = File.ReadAllText(filename);
				Newtonsoft.Json.Linq.JObject json = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(filetext);
				bool updateSettingsFile;
				LoadValues(json, out updateSettingsFile);
				if (updateSettingsFile)
					Save(filename, out string _);
				m_LoadedFile = filename;
				return true;
				}
			catch (Exception ex) {
				errMsg = ex.Message;
				m_LoadedFile = "";
				return false;
				}
			}
		public bool Save(string filename, out string errMsg)
			{
			errMsg = "";

			try {
				string directory = Path.GetDirectoryName(filename);
				if (!Directory.Exists(directory))
					Directory.CreateDirectory(directory);
				string json = JsonConvert.SerializeObject(this);
				File.WriteAllText(filename, json);
				return true;
				}
			catch (Exception ex) {
				errMsg = ex.Message;
				return false;
				}
			}
		}
	}
