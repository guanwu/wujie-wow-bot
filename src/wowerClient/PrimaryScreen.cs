using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace wowerClient;

internal class PrimaryScreen
{
	private const int HORZRES = 8;

	private const int VERTRES = 10;

	private const int LOGPIXELSX = 88;

	private const int LOGPIXELSY = 90;

	private const int DESKTOPVERTRES = 117;

	private const int DESKTOPHORZRES = 118;

	public static Size WorkingArea
	{
		get
		{
			IntPtr dC = GetDC(IntPtr.Zero);
			Size result = default(Size);
			result.Width = GetDeviceCaps(dC, 8);
			result.Height = GetDeviceCaps(dC, 10);
			ReleaseDC(IntPtr.Zero, dC);
			return result;
		}
	}

	public static int DpiX
	{
		get
		{
			IntPtr dC = GetDC(IntPtr.Zero);
			int deviceCaps = GetDeviceCaps(dC, 88);
			ReleaseDC(IntPtr.Zero, dC);
			return deviceCaps;
		}
	}

	public static int DpiY
	{
		get
		{
			IntPtr dC = GetDC(IntPtr.Zero);
			int deviceCaps = GetDeviceCaps(dC, 90);
			ReleaseDC(IntPtr.Zero, dC);
			return deviceCaps;
		}
	}

	public static Size DESKTOP
	{
		get
		{
			IntPtr dC = GetDC(IntPtr.Zero);
			Size result = default(Size);
			result.Width = GetDeviceCaps(dC, 118);
			result.Height = GetDeviceCaps(dC, 117);
			ReleaseDC(IntPtr.Zero, dC);
			return result;
		}
	}

	public static float ScaleX
	{
		get
		{
			IntPtr dC = GetDC(IntPtr.Zero);
			GetDeviceCaps(dC, 118);
			GetDeviceCaps(dC, 8);
			float result = (float)GetDeviceCaps(dC, 118) / (float)GetDeviceCaps(dC, 8);
			ReleaseDC(IntPtr.Zero, dC);
			return result;
		}
	}

	public static float ScaleY
	{
		get
		{
			IntPtr dC = GetDC(IntPtr.Zero);
			float result = (float)GetDeviceCaps(dC, 117) / (float)GetDeviceCaps(dC, 10);
			ReleaseDC(IntPtr.Zero, dC);
			return result;
		}
	}

	[DllImport("user32.dll")]
	private static extern IntPtr GetDC(IntPtr intptr_0);

	[DllImport("gdi32.dll")]
	private static extern int GetDeviceCaps(IntPtr intptr_0, int int_0);

	[DllImport("user32.dll")]
	private static extern IntPtr ReleaseDC(IntPtr intptr_0, IntPtr intptr_1);
}
