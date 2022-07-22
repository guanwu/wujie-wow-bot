using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace wowerClient;

public static class LibFile
{
	public static List<string> DirectoryList = new List<string>();

	public static List<string> FileList = new List<string>();

	public static void GetDirectory(string string_0)
	{
		DirectoryList.Clear();
		FileList.Clear();
		getDirectorys(string_0);
	}

	public static List<string> GetBindDirectory(string string_0)
	{
		string string_ = Path.Combine(string_0, "WTF\\Account\\");
		List<string> list = new List<string>();
		GetDirectory(string_);
		foreach (string file in FileList)
		{
			if (Regex.IsMatch(file, "\\\\bindings-cache\\.wtf$"))
			{
				list.Add(file);
			}
		}
		return list;
	}

	private static void getDirectorys(string string_0)
	{
		if (!Directory.Exists(string_0))
		{
			return;
		}
		string[] fileSystemEntries = Directory.GetFileSystemEntries(string_0);
		foreach (string text in fileSystemEntries)
		{
			if (File.Exists(text))
			{
				FileList.Add(text);
			}
			else if (Directory.GetDirectories(text).Length == 0)
			{
				DirectoryList.Add(text);
			}
			getDirectorys(text);
		}
	}
}
