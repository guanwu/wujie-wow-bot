using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace wowerClient;

internal class LuaManager
{
	private string string_0 = ".\\script\\";

	public Dictionary<string, GameLua> GameLuas = new Dictionary<string, GameLua>();

	public LuaManager()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(string_0);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
	}

	public bool put(GameLua gameLua_0)
	{
		throw new Exception("Runtime exception");
	}

	public GameLua get(string string_1)
	{
		return new GameLua();
	}

	public bool check_version(GameLua gameLua_0)
	{
		GameLua gameLua = GameLuas[gameLua_0.string_3];
		if (gameLua_0.string_2 != gameLua.string_2)
		{
			delLuaFile(gameLua_0.string_3);
			return false;
		}
		return true;
	}

	public List<GameLua> getLuaByCareer(string string_1)
	{
		List<GameLua> list = new List<GameLua>();
		foreach (GameLua value in GameLuas.Values)
		{
			if (value.string_0 == string_1)
			{
				list.Add(value);
			}
		}
		return list;
	}

	public string HttpPost(string url, string context)
	{
		_ = $"需要请求的url";
		HttpWebRequest obj = (HttpWebRequest)WebRequest.Create(url);
		obj.Method = "POST";
		byte[] bytes = Encoding.UTF8.GetBytes(context);
		int num = bytes.Length;
		obj.ContentLength = num;
		Stream requestStream = obj.GetRequestStream();
		requestStream.Write(bytes, 0, num);
		requestStream.Close();
		return new StreamReader(((HttpWebResponse)obj.GetResponse()).GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd().ToString();
	}

	public string Base64Decrypt(string string_1, string username, string password)
	{
		// var rr = "eF9YSltCD1sKRgtXBB4JSQI4AVkJXgcLB1lCUBURBh1bbHFbRldTTRVFCk0IHgMFFgMyAABaFUAGCBtFSBkSChlXa1R4WkZXWkUVRQpNCB4DBRYDMgQHRgtEGQgNOREFCBEbAg1KBRwCDwwAFU4CfgNVW1tQChQAAVMDAUFXR094UlVJWFhSSnlVJgUFGk0FVxgeOE1gXwEUAwJTFUAHAQ0GBgUWX2NcV1JlWlpKWk5KA1cECGdCWkoLCQcfWAtLD0tDGgJQbFdYVU1XelpGVlA4WhhWAEACG0YoTFVCUg4OSD8KBUNeW1NdRV9TX3McHgkaTjNCC1gHWFtaXHVbXV0eSx1ZFHkAHxoHAV9Xa1N/Q1tXXlgLRQFOQRkYGlwzCgMLRghBABQHTz4JCQsdHw4KZSEfCxoVXUACfgBfR0ZUCA0eAVgASEZMWAVIYFBdRVRYSnlVJgUFGk0FVxgeIwIHFlhcCglgC0MEFAZBXlhUXFMeFQk5VWBUWUUVQw1YAlc7CQlXTEBcBhU8QFVHFBZYXG9YXFZKcVxGVFhNAwRMG0JXfVtWCBQAAFsVQg90WhAcH0orHAARBy1WUGxZQhVCCVgAX05QFU1XQglgDEEZDQJZQlAqBgYDFRQmA0YoHxlJFlxCCGdGWlMVDQsfWAtLD0tDGgJQbFRYXE1Xel9GVlA4VBJWAR4hFAUITUpdX0YJSD8BAllDWFNJW19YXDobBRZQfgpBFEYDWVtaXHVVV10fFT5WV1kBAAUKSScYDBYoC1JcYEMLWwlFHl9FU1xKTF1DUDNDAQ8bR0BfSlVTIQIJJxsYCQZYdQRQHVQZW1xcMwsGH1sJQBkKBUxIGRIKGVdrUHtDWFNbWAlNdBddAwMYCVUUfkACUBRBFA9PeFhUVUVcVVFlXVhfUAdNGEhOOFxEWUoNCB4DUHURWlZDBx0GSikaBQgAPUNdXGBAClsJRB5fRVNcSkxdQ1AzQwwKG0ZBRlZfJQ4OCD0dBQpGOkwaSBVWVE1gVw8AHgdGC0AMAkQBHRpcb1hZVkp7XV9KWk51FFcaRh8YBkp1S1paDE1eAwI9REpfSlRfQVNUcFUZEgUEA30ORh5fQltKCQJ
		// var myresult = Encoding.GetEncoding(54936).GetString(Convert.FromBase64String(string_1));

		//string context = "username=" + username + "&password=" + password + "&luatext=" + string_1;
		//var rightresult = HttpPost("http://1.13.173.226:12341/api/luajiemi", context);
		
		//return myresult;

		return "102,166,229:stop:\n230,230,0:stop:\n84,199,229:stop:\n230,230,0:stop:\n215,101,229:stop:\n64,26,0:Lcontrol,Lshift,9:\n18,163,229:stop:\n83,153,0:Lcontrol,U:\n98,119,229:stop:\n164,50,0:stop:\n50,215,229:stop:\n218,135,0:Lcontrol,Numpad7:\n226,158,229:stop:\n53,55,0:Lcontrol,Numpad6:\n56,114,229:stop:\n218,135,0:Lcontrol,Numpad7:\n220,215,229:stop:\n68,99,0:Lcontrol,Numpad8:\n211,14,229:stop:\n231,45,0:Lcontrol,Numpad2:\n117,83,229:stop:\n101,231,0:Lmenu,Numpad9:\n36,51,229:stop:\n53,55,0:Lcontrol,Numpad6:\n105,59,229:stop:\n111,130,0:Lmenu,Lcontrol,0:\n95,125,229:stop:\n36,214,0:Lmenu,Lcontrol,Numpad8:\n72,11,229:stop:\n147,225,0:Lcontrol,Lshift,6:\n34,102,229:stop:\n62,251,0:Lcontrol,Lshift,8:\n220,147,229:stop:\n133,40,0:Lcontrol,Lshift,7:\n43,10,229:stop:\n192,33,0:Lcontrol,Numpad9:\n168,4,229:stop:\n147,225,0:Lcontrol,Lshift,6:\n185,16,229:stop:\n62,251,0:Lcontrol,Lshift,8:\n115,172,229:stop:\n133,40,0:Lcontrol,Lshift,7:\n214,5,229:stop:\n119,224,0:Lcontrol,6:\n151,52,229:stop:\n192,33,0:Lcontrol,Numpad9:\n96,211,229:stop:\n192,33,0:Lcontrol,Numpad9:\n223,80,229:stop:\n82,124,0:Lmenu,Lcontrol,F7:\n227,192,229:stop:\n191,184,0:Lmenu,Lcontrol,F6:\n51,176,229:stop:\n45,9,0:Lmenu,Lcontrol,F5:\n124,41,229:stop:\n144,4,0:Lcontrol,Numpad0:\n168,52,229:stop:\n51,40,0:Lmenu,Numpad0:\n143,100,229:stop:\n69,115,0:Lmenu,Lcontrol,F10:\n36,165,229:stop:\n210,179,0:Lmenu,Add:\n167,140,229:stop:\n232,206,0:Lmenu,Lcontrol,F2:\n24,187,229:stop:\n24,198,0:Lmenu,Lcontrol,F3:\n75,114,229:stop:\n224,55,0:Lmenu,Lcontrol,Numpad5:\n124,181,229:stop:\n224,55,0:Lmenu,Lcontrol,Numpad5:\n195,11,229:stop:\n51,65,0:Lcontrol,7:\n119,109,229:stop:\n173,160,0:Lcontrol,F1:\n200,109,229:stop:\n112,134,0:Lmenu,Subtract:\n107,38,229:stop:\n210,179,0:Lmenu,Add:\n31,99,229:stop:\n87,87,0:Lmenu,Numpad1:\n166,69,229:stop:\n213,148,0:Lcontrol,Add:\n69,81,229:stop:\n123,43,0:Lmenu,Lcontrol,Numpad7:\n140,73,229:stop:\n108,218,0:Lmenu,Lcontrol,Numpad6:\n88,42,229:stop:\n111,130,0:Lmenu,Lcontrol,0:\n160,154,229:stop:\n99,79,0:Lmenu,Lcontrol,8:\n96,15,229:stop:\n193,162,0:Lmenu,Lcontrol,7:\n108,187,229:stop:\n66,242,0:Lmenu,Lcontrol,6:\n60,98,229:stop:\n31,108,0:Lmenu,Lcontrol,Numpad9:\n26,96,229:stop:\n148,129,0:Lmenu,Lcontrol,Subtract:\n105,90,229:stop:\n25,132,0:Lmenu,Lcontrol,Add:\n76,229,229:stop:\n116,20,0:Lmenu,Lcontrol,Numpad0:\n108,174,229:stop:\n31,108,0:Lmenu,Lcontrol,Numpad9:\n10,78,229:stop:\n64,26,0:Lcontrol,Lshift,9:\n171,199,229:stop:\n194,239,0:Lmenu,Numpad7:\n60,154,229:stop:\n210,182,0:Lmenu,Lcontrol,Numpad4:\n183,121,229:stop:\n174,105,0:Lcontrol,Numpad3:\n57,214,229:stop:\n218,135,0:Lcontrol,Numpad7:\n164,196,229:stop:\n36,25,0:Lmenu,Lcontrol,F9:\n138,22,229:stop:\n222,79,0:Lcontrol,Numpad5:\n32,8,229:stop:\n93,65,0:Lcontrol,Numpad4:\n173,29,229:stop:\n86,127,0:Lmenu,Numpad5:\n43,11,229:stop:\n156,170,0:Lmenu,Numpad8:\n12,41,229:stop:\n157,132,0:Lmenu,Numpad6:\n139,63,229:stop:\n106,179,0:Lcontrol,Numpad1:\n197,43,229:stop:\n223,220,0:Lmenu,Numpad4:\n71,102,229:stop:\n68,99,0:Lcontrol,Numpad8:\n43,133,229:stop:\n230,230,0:stop:\n126,29,229:stop:\n205,96,0:stop:\n64,221,229:stop:\n40,128,0:stop:\n20,196,229:stop:\n173,160,0:Lcontrol,F1:\n27,50,229:stop:\n123,169,0:Lcontrol,F2:\n168,92,229:stop:\n92,43,0:Lcontrol,F3:\n30,3,229:stop:\n164,109,0:Lcontrol,F4:\n85,32,229:stop:\n183,149,0:Lcontrol,F5:\n180,178,229:stop:\n196,44,0:Lcontrol,F6:\n24,37,229:stop:\n43,94,0:Lcontrol,F7:\n47,187,229:stop:\n20,150,0:Lcontrol,Numpad6:Lcontrol,Numpad7:Lcontrol,Numpad4:\n196,190,229:stop:\n225,29,0:Lcontrol,F9:\n68,223,229:stop:\n76,171,0:Lcontrol,F10:\n65,221,229:stop:\n17,1,0:Lcontrol,F11:\n32,196,229:stop:\n49,243,0:Lcontrol,F12:\n172,223,229:stop:\n236,106,0:Lshift,F1:\n102,4,229:stop:\n175,158,0:Lshift,F2:\n221,108,229:stop:\n161,77,0:Lshift,F3:\n30,43,229:stop:\n36,50,0:Lshift,F4:\n53,160,229:stop:\n221,228,0:Lshift,F5:\n85,115,229:stop:\n102,100,0:Lshift,F6:\n54,41,229:stop:\n213,195,0:Lshift,F7:\n105,41,229:stop:\n183,77,0:Lshift,F10:\n227,7,229:stop:\n219,223,0:Lshift,F11:\n10,78,229:stop:\n190,135,0:Lshift,F12:\n170,202,229:stop:\n92,72,0:Lmenu,6:\n196,185,229:stop:\n98,250,0:Lmenu,7:\n55,151,229:stop:\n212,243,0:Lmenu,8:\n114,54,229:stop:\n163,79,0:Lmenu,9:\n157,206,229:stop:\n149,138,0:Lmenu,0:\n227,28,229:stop:\n119,224,0:Lcontrol,6:\n188,41,229:stop:\n51,65,0:Lcontrol,7:\n149,113,229:stop:\n120,155,0:Lcontrol,8:\n226,183,229:stop:\n226,117,0:Lcontrol,9:\n200,44,229:stop:\n88,32,0:Lcontrol,0:\n3,158,229:stop:\n95,45,0:Lcontrol,Y:\n94,111,229:stop:\n83,153,0:Lcontrol,U:\n179,161,229:stop:\n42,76,0:Lcontrol,I:\n162,151,229:stop:\n85,41,0:Lcontrol,O:\n170,135,229:stop:\n140,141,0:Lcontrol,P:\n113,116,229:stop:\n51,196,0:Lcontrol,H:\n60,159,229:stop:\n52,197,0:Lcontrol,J:\n156,210,229:stop:\n95,236,0:Lcontrol,K:\n74,67,229:stop:\n21,209,0:Lcontrol,L:\n182,11,229:stop:\n164,50,0:stop:\n121,147,229:stop:\n10,84,0:Lmenu,Lcontrol,Numpad1:\n137,95,229:stop:\n55,73,0:Lmenu,Lcontrol,Numpad2:\n48,89,229:stop:\n51,148,0:Lmenu,Lcontrol,Numpad3:\n139,132,229:stop:\n210,182,0:Lmenu,Lcontrol,Numpad4:\n81,81,229:stop:\n224,55,0:Lmenu,Lcontrol,Numpad5:\n216,166,229:stop:\n108,218,0:Lmenu,Lcontrol,Numpad6:\n100,211,229:stop:\n123,43,0:Lmenu,Lcontrol,Numpad7:\n210,61,229:stop:\n36,214,0:Lmenu,Lcontrol,Numpad8:\n132,51,229:stop:\n88,224,0:stop:\n101,4,229:stop:\n88,240,0:stop:\n157,181,229:stop:\n104,16,0:stop:";
	}

	public GameLua parseLuaFile(string string_1, string username, string password)
	{
		Encoding encoding = Encoding.GetEncoding("utf-8");
		string[] array = File.ReadAllText(string_0 + string_1, encoding).Split(new string[1] { "!!" }, StringSplitOptions.None);
		string[] array2 = Encoding.UTF8.GetString(Convert.FromBase64String(array[0])).Split(new string[1] { "#!!#" }, StringSplitOptions.None);
		GameLua gameLua = new GameLua();
		gameLua.string_0 = array2[0];
		gameLua.string_1 = array2[1];
		gameLua.string_3 = array2[2];
		gameLua.string_4 = array2[3];
		gameLua.string_2 = array2[4];
		gameLua.string_5 = string_0 + string_0;
		for (int i = 1; i < array.Length - 1; i++)
		{
			string[] array3 = array[i].Split(',');
			gameLua.files.Add(array3[0], Encoding.UTF8.GetString(Convert.FromBase64String(array3[1])));
		}
		string[] array4 = Base64Decrypt(array.Last().ToString(), username, password).Split('\n');
		foreach (string text in array4)
		{
			if (text != "")
			{
				string[] array5 = text.Split(':');
				if (!gameLua.keys.ContainsKey(array5[0]))
				{
					gameLua.keys.Add(array5[0], array5.Skip(1).Take(array5.Length - 1).ToArray());
				}
			}
		}
		return gameLua;
	}

	public bool checkWowDir(string string_1, string string_2)
	{
		string[] source = new string[3] { "Wow.exe", "wow.exe", "WowClassic.exe" };
		DirectoryInfo directoryInfo;
		try
		{
			directoryInfo = new DirectoryInfo(string_1);
		}
		catch
		{
			return false;
		}
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			if (source.Contains(fileInfo.Name) && string_1.IndexOf(string_2) > 0)
			{
				return true;
			}
		}
		return false;
	}

	public bool checkAccountFile(string string_1, string string_2)
	{
		try
		{
			new DirectoryInfo(Path.Combine(string_1, string_2, "WTF\\Account"));
			return true;
		}
		catch
		{
			return false;
		}
	}

	public bool checkLuaFile(string string_1)
	{
		return new FileInfo(string_0 + string_1).Exists;
	}

	public bool delLuaFile(string string_1)
	{
		new FileInfo(string_0 + string_1).Delete();
		return true;
	}

	public bool writeBindfile(string string_1, string[] string_2)
	{
		FileInfo fileInfo = new FileInfo(string_1);
		if (!fileInfo.Exists)
		{
			fileInfo.Create().Close();
			fileInfo.Refresh();
		}
		string text = File.ReadAllText(fileInfo.FullName);
		List<string> list = new List<string>();
		foreach (string text2 in string_2)
		{
			if (text.IndexOf(text2) < 0)
			{
				list.Add(text2);
			}
		}
		StreamWriter streamWriter = new StreamWriter(fileInfo.FullName, append: true);
		foreach (string item in list)
		{
			streamWriter.Write("\r\n" + item);
		}
		streamWriter.Close();
		streamWriter.Dispose();
		return true;
	}

	public bool writeLua2WowDir(string string_1, GameLua gameLua_0)
	{
		string text = Path.Combine(string_1, ".\\Interface\\AddOns\\wowshaoSkada\\");
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		else
		{
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
		}
		foreach (string key in gameLua_0.files.Keys)
		{
			if (key != "bind")
			{
				StreamWriter streamWriter = new FileInfo(text + key).CreateText();
				streamWriter.Write(gameLua_0.files[key]);
				streamWriter.Close();
				streamWriter.Dispose();
				continue;
			}
			string[] string_2 = gameLua_0.files[key].Split(new string[1] { "\r\n" }, StringSplitOptions.None);
			foreach (string item in LibFile.GetBindDirectory(string_1))
			{
				writeBindfile(item, string_2);
			}
		}
		return true;
	}

	public void writeWtfFile(string string_1)
	{
		FileInfo fileInfo = new FileInfo(Path.Combine(string_1, "WTF\\Config.wtf"));
		if (!fileInfo.Exists)
		{
			fileInfo.Create().Close();
			fileInfo.Refresh();
		}
		string text = File.ReadAllText(fileInfo.FullName);
		List<string> list = new List<string>();
		string[] array = text.Split(new string[1] { "\r\n" }, StringSplitOptions.None);
		foreach (string text2 in array)
		{
			if (text2.IndexOf("SET Contrast") < 0 && text2.IndexOf("SET Brightness") < 0 && text2.IndexOf("SET gxApi") < 0 && text2 != "")
			{
				list.Add(text2);
			}
		}
		list.Add("SET Contrast 50");
		list.Add("SET Brightness 50");
		list.Add("SET gxApi \"D3D12\"");
		StreamWriter streamWriter = new StreamWriter(fileInfo.FullName, append: false);
		foreach (string item in list)
		{
			streamWriter.Write(item + "\r\n");
		}
		streamWriter.Close();
		streamWriter.Dispose();
	}
}
