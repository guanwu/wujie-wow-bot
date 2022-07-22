using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace wowerClient;

public static class LibCharacter
{
	public class Character
	{
		public string account;

		public string area;

		public string career;

		public string name;

		public bool has_character_bind_file;

		public bool has_account_bind_file;

		public string path;

		public string account_path;

		public bool CreateBindFile()
		{
			if (!has_account_bind_file && account_path != "")
			{
				File.Create(Path.Combine(account_path, "bindings-cache.wtf")).Close();
				return true;
			}
			return false;
		}
	}

	public static List<Character> ListCharacter = new List<Character>();

	public static bool GetCharacter(string string_0)
	{
		string path = Path.Combine(string_0, "WTF\\Account");
		ListCharacter.Clear();
		if (!Directory.Exists(path))
		{
			return false;
		}
		string[] directories = Directory.GetDirectories(path);
		foreach (string text in directories)
		{
			bool has_account_bind_file = false;
			if (text.IndexOf("SavedVariables") > 0)
			{
				continue;
			}
			Console.WriteLine(text);
			string[] files = Directory.GetFiles(text);
			for (int j = 0; j < files.Length; j++)
			{
				if (files[j].IndexOf("bindings-cache.wtf") > 0)
				{
					has_account_bind_file = true;
					break;
				}
			}
			string[] directories2 = Directory.GetDirectories(text);
			foreach (string text2 in directories2)
			{
				if (text2.IndexOf("SavedVariables") > 0)
				{
					continue;
				}
				string[] directories3 = Directory.GetDirectories(text2);
				foreach (string text3 in directories3)
				{
					Character character = new Character();
					string[] separator = new string[1] { "\\" };
					character.account = text.Split(separator, StringSplitOptions.None).Last();
					string[] separator2 = new string[1] { "\\" };
					character.area = text2.Split(separator2, StringSplitOptions.None).Last();
					string[] separator3 = new string[1] { "\\" };
					character.name = text3.Split(separator3, StringSplitOptions.None).Last();
					character.path = text3;
					character.has_account_bind_file = has_account_bind_file;
					character.account_path = text;
					string[] files2 = Directory.GetFiles(text3);
					for (int m = 0; m < files2.Length; m++)
					{
						if (files2[m].IndexOf("bindings-cache.wtf") > 0)
						{
							character.has_character_bind_file = true;
							break;
						}
					}
					ListCharacter.Add(character);
				}
			}
		}
		return true;
	}

	public static bool CreateBindFile()
	{
		foreach (Character item in ListCharacter)
		{
			if (!item.has_account_bind_file)
			{
				item.CreateBindFile();
			}
		}
		return true;
	}
}
