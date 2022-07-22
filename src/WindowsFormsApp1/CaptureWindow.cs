using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1;

public static class CaptureWindow
{
	private class User32
	{
		public struct RECT
		{
			public int left;

			public int top;

			public int right;

			public int bottom;
		}

		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr intptr_0);

		[DllImport("user32.dll")]
		public static extern IntPtr ReleaseDC(IntPtr intptr_0, IntPtr intptr_1);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowRect(IntPtr intptr_0, ref RECT rect_0);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr FindWindow(string string_0, string string_1);

		[DllImport("user32.dll")]
		public static extern bool MapDialogRect(IntPtr intptr_0, ref RECT rect_0);

		[DllImport("user32.dll")]
		public static extern bool GetClientRect(IntPtr intptr_0, ref RECT rect_0);
	}

	private class Gdi32
	{
		public const int SRCCOPY = 13369376;

		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(IntPtr intptr_0, int int_0, int int_1, int int_2, int int_3, IntPtr intptr_1, int int_4, int int_5, int int_6);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr intptr_0, int int_0, int int_1);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr intptr_0);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteDC(IntPtr intptr_0);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr intptr_0);

		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr intptr_0, IntPtr intptr_1);
	}

	public static Bitmap getWow4(IntPtr intptr_0)
	{
		Rectangle rectangle = default(Rectangle);
		rectangle.Width = 50;
		rectangle.Height = 50;
		User32.RECT rect_ = default(User32.RECT);
		User32.GetWindowRect(intptr_0, ref rect_);
		User32.RECT rect_2 = default(User32.RECT);
		User32.GetClientRect(intptr_0, ref rect_2);
		int num = rect_.right - rect_.left;
		int num2 = rect_.bottom - rect_.top;
		int num3 = (num - rect_2.right + rect_2.left) / 2;
		int num4 = num2 - rect_2.bottom + rect_2.top - num3;
		Session.test1 = rect_.right + ":" + rect_.left + ":" + rect_.bottom + ":" + rect_.top;
		Session.test2 = rect_2.right + ":" + rect_2.left + ":" + rect_2.bottom + ":" + rect_2.top;
		Bitmap bitmap = new Bitmap(50, 50);
		Graphics graphics = Graphics.FromImage(bitmap);
		if (num > rect_2.right - rect_2.left)
		{
			if ((double)Session.dpi != 1.0)
			{
				graphics.CopyFromScreen(new Point((int)((float)(rect_.left + num3) * Session.dpi) + 1, (int)((float)(rect_.top + num4) * Session.dpi) + 1), new Point(0, 0), rectangle.Size);
			}
			else
			{
				graphics.CopyFromScreen(new Point(rect_.left + num3, rect_.top + num4), new Point(0, 0), rectangle.Size);
			}
		}
		else
		{
			if (num != rect_2.right - rect_2.left)
			{
				throw new Exception("faild");
			}
			graphics.CopyFromScreen(new Point(rect_.left, rect_.top), new Point(0, 0), rectangle.Size);
		}
		graphics.Dispose();
		return bitmap;
	}
}
