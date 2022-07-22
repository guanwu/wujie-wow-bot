using System;
using System.IO;

namespace WindowsFormsApp1;

public class Config
{
	public string string_0 = "";

	public string string_1 = "";

	public string string_2 = "";

	public bool bool_0;

	public bool bool_1;

	private string string_3 = "config.dat";

	public bool write()
	{
		StreamWriter streamWriter = new StreamWriter(string_3);
		streamWriter.WriteLine("@@wowDir@@:" + string_0 + "\r\n");
		streamWriter.WriteLine("@@username@@:" + string_1 + "\r\n");
		if (bool_0)
		{
			streamWriter.WriteLine("@@password@@:" + string_2 + "\r\n");
			streamWriter.WriteLine("@@savePasswd@@:1\r\n");
		}
		else
		{
			streamWriter.WriteLine("@@savePasswd@@:0\r\n");
		}
		if (bool_1)
		{
			streamWriter.WriteLine("@@savePasswd@@:1\r\n");
		}
		else
		{
			streamWriter.WriteLine("@@savePasswd@@:0\r\n");
		}
		streamWriter.Close();
		return true;
	}

	public bool read()
	{
		FileInfo fileInfo = new FileInfo(string_3);
		if (!File.Exists(fileInfo.FullName))
		{
			FileStream fileStream = fileInfo.Create();
			fileStream.Close();
			fileStream.Dispose();
			return false;
		}
		string[] array = File.ReadAllText(fileInfo.FullName).Split(new string[1] { "\r\n" }, StringSplitOptions.None);
		foreach (string text in array)
		{
			if (text.IndexOf("@@wowDir@@:") == 0)
			{
				string_0 = text.Replace("@@wowDir@@:", "");
			}
			if (text.IndexOf("@@username@@:") == 0)
			{
				string_1 = text.Replace("@@username@@:", "");
			}
			if (text.IndexOf("@@password@@:") == 0)
			{
				string_2 = text.Replace("@@password@@:", "");
			}
			if (text.IndexOf("@@autoLogin@@:") == 0 && text.Replace("@@autoLogin@@:", "").ToString() == "1")
			{
				bool_1 = true;
			}
			if (text.IndexOf("@@savePasswd@@:") == 0 && text.Replace("@@savePasswd@@:", "").ToString() == "1")
			{
				bool_0 = true;
			}
		}
		return true;
	}
}
