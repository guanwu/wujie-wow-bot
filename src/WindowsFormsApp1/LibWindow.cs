using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsFormsApp1;

internal class LibWindow
{
	private delegate bool deleWindowsProc(IntPtr intptr_0, string string_0);

	public struct SENDDATASTRUCT
	{
		public IntPtr dwData;

		public int DataLength;

		[MarshalAs(UnmanagedType.LPStr)]
		public string lpData;
	}

	public IntPtr handler;

	private Dictionary<IntPtr, string> allHandler = new Dictionary<IntPtr, string>();

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr FindWindow(string string_0, string string_1);

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern int EnumWindows(deleWindowsProc deleWindowsProc_0, string string_0);

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern int GetWindowText(IntPtr intptr_0, StringBuilder stringBuilder_0, int int_0);

	[DllImport("user32.dll")]
	public static extern int SendMessage(IntPtr intptr_0, int int_0, uint uint_0, int int_1);

	[DllImport("user32.dll")]
	public static extern int PostMessage(IntPtr intptr_0, int int_0, uint uint_0, int int_1);

	[DllImport("user32.dll")]
	public static extern void keybd_event(byte byte_0, byte byte_1, int int_0, int int_1);

	public List<IntPtr> getAllWindow(string string_0)
	{
		allHandler.Clear();
		EnumWindows(method_0, string_0);
		List<IntPtr> list = new List<IntPtr>();
		foreach (KeyValuePair<IntPtr, string> item in allHandler)
		{
			Console.WriteLine(item.Value);
			if (item.Value.Equals(string_0))
			{
				list.Add(item.Key);
			}
		}
		return list;
	}

	private bool method_0(IntPtr intptr_0, string string_0)
	{
		StringBuilder stringBuilder = new StringBuilder(50);
		GetWindowText(intptr_0, stringBuilder, stringBuilder.Capacity);
		allHandler.Add(intptr_0, stringBuilder.ToString());
		return true;
	}

	public bool checkWindowAlive(IntPtr intptr_0, string string_0)
	{
		return getAllWindow(string_0).Contains(intptr_0);
	}

	public bool setWindow(string string_0)
	{
		try
		{
			handler = FindWindow(null, string_0);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public string getRGB()
	{
		try
		{
			Color pixel = CaptureWindow.getWow4(handler).GetPixel(0, 0);
			return pixel.R + "," + pixel.G + "," + pixel.B;
		}
		catch
		{
			return "0,0,0";
		}
	}

	public bool sendKey(List<byte> list_0)
	{
		Console.WriteLine("----------------------------------");
		foreach (byte item in list_0)
		{
			PostMessage(handler, 256, item, 1);
			Console.WriteLine(item.ToString());
		}
		foreach (byte item2 in list_0)
		{
			PostMessage(handler, 257, item2, 1);
			Console.WriteLine(item2.ToString());
		}
		return false;
	}
}
