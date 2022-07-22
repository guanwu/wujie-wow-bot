using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1;

internal class KeyboardHook
{
	public delegate int HookProc(int int_0, int int_1, IntPtr intptr_0);

	[StructLayout(LayoutKind.Sequential)]
	public class KeyboardHookStruct
	{
		public int vkCode;

		public int scanCode;

		public int flags;

		public int time;

		public int dwExtraInfo;
	}

	[CompilerGenerated]
	private KeyEventHandler keyEventHandler_0;

	[CompilerGenerated]
	private KeyPressEventHandler keyPressEventHandler_0;

	[CompilerGenerated]
	private KeyEventHandler keyEventHandler_1;

	private static int int_0;

	public const int WH_KEYBOARD_LL = 13;

	private HookProc hookProc_0;

	private const int WM_KEYDOWN = 256;

	private const int WM_KEYUP = 257;

	private const int WM_SYSKEYDOWN = 260;

	private const int WM_SYSKEYUP = 261;

	public event KeyEventHandler Event_0
	{
		[CompilerGenerated]
		add
		{
			KeyEventHandler keyEventHandler = keyEventHandler_0;
			KeyEventHandler keyEventHandler2;
			do
			{
				keyEventHandler2 = keyEventHandler;
				KeyEventHandler value2 = (KeyEventHandler)Delegate.Combine(keyEventHandler2, value);
				keyEventHandler = Interlocked.CompareExchange(ref keyEventHandler_0, value2, keyEventHandler2);
			}
			while (keyEventHandler != keyEventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			KeyEventHandler keyEventHandler = keyEventHandler_0;
			KeyEventHandler keyEventHandler2;
			do
			{
				keyEventHandler2 = keyEventHandler;
				KeyEventHandler value2 = (KeyEventHandler)Delegate.Remove(keyEventHandler2, value);
				keyEventHandler = Interlocked.CompareExchange(ref keyEventHandler_0, value2, keyEventHandler2);
			}
			while (keyEventHandler != keyEventHandler2);
		}
	}

	public event KeyPressEventHandler Event_1
	{
		[CompilerGenerated]
		add
		{
			KeyPressEventHandler keyPressEventHandler = keyPressEventHandler_0;
			KeyPressEventHandler keyPressEventHandler2;
			do
			{
				keyPressEventHandler2 = keyPressEventHandler;
				KeyPressEventHandler value2 = (KeyPressEventHandler)Delegate.Combine(keyPressEventHandler2, value);
				keyPressEventHandler = Interlocked.CompareExchange(ref keyPressEventHandler_0, value2, keyPressEventHandler2);
			}
			while (keyPressEventHandler != keyPressEventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			KeyPressEventHandler keyPressEventHandler = keyPressEventHandler_0;
			KeyPressEventHandler keyPressEventHandler2;
			do
			{
				keyPressEventHandler2 = keyPressEventHandler;
				KeyPressEventHandler value2 = (KeyPressEventHandler)Delegate.Remove(keyPressEventHandler2, value);
				keyPressEventHandler = Interlocked.CompareExchange(ref keyPressEventHandler_0, value2, keyPressEventHandler2);
			}
			while (keyPressEventHandler != keyPressEventHandler2);
		}
	}

	public event KeyEventHandler Event_2
	{
		[CompilerGenerated]
		add
		{
			KeyEventHandler keyEventHandler = keyEventHandler_1;
			KeyEventHandler keyEventHandler2;
			do
			{
				keyEventHandler2 = keyEventHandler;
				KeyEventHandler value2 = (KeyEventHandler)Delegate.Combine(keyEventHandler2, value);
				keyEventHandler = Interlocked.CompareExchange(ref keyEventHandler_1, value2, keyEventHandler2);
			}
			while (keyEventHandler != keyEventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			KeyEventHandler keyEventHandler = keyEventHandler_1;
			KeyEventHandler keyEventHandler2;
			do
			{
				keyEventHandler2 = keyEventHandler;
				KeyEventHandler value2 = (KeyEventHandler)Delegate.Remove(keyEventHandler2, value);
				keyEventHandler = Interlocked.CompareExchange(ref keyEventHandler_1, value2, keyEventHandler2);
			}
			while (keyEventHandler != keyEventHandler2);
		}
	}

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
	public static extern int SetWindowsHookEx(int int_1, HookProc hookProc_1, IntPtr intptr_0, int int_2);

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
	public static extern bool UnhookWindowsHookEx(int int_1);

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
	public static extern int CallNextHookEx(int int_1, int int_2, int int_3, IntPtr intptr_0);

	[DllImport("kernel32.dll")]
	private static extern int GetCurrentThreadId();

	[DllImport("kernel32.dll")]
	public static extern IntPtr GetModuleHandle(string string_0);

	public void Start()
	{
		if (int_0 == 0)
		{
			hookProc_0 = method_0;
			int_0 = SetWindowsHookEx(13, hookProc_0, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
			SetWindowsHookEx(13, hookProc_0, IntPtr.Zero, GetCurrentThreadId());
			if (int_0 == 0)
			{
				Stop();
				throw new Exception("安装键盘钩子失败");
			}
		}
	}

	public void Stop()
	{
		bool flag = true;
		if (int_0 != 0)
		{
			flag = UnhookWindowsHookEx(int_0);
			int_0 = 0;
		}
		if (!flag)
		{
			throw new Exception("卸载钩子失败！");
		}
	}

	[DllImport("user32")]
	public static extern int ToAscii(int int_1, int int_2, byte[] byte_0, byte[] byte_1, int int_3);

	[DllImport("user32")]
	public static extern int GetKeyboardState(byte[] byte_0);

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
	private static extern short GetKeyState(int int_1);

	private int method_0(int int_1, int int_2, IntPtr intptr_0)
	{
		if (int_1 >= 0 && (keyEventHandler_0 != null || keyEventHandler_1 != null || keyPressEventHandler_0 != null))
		{
			KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(intptr_0, typeof(KeyboardHookStruct));
			if (keyEventHandler_0 != null && (int_2 == 256 || int_2 == 260))
			{
				KeyEventArgs e = new KeyEventArgs((Keys)keyboardHookStruct.vkCode);
				keyEventHandler_0(this, e);
			}
			if (keyPressEventHandler_0 != null && int_2 == 256)
			{
				byte[] byte_ = new byte[256];
				GetKeyboardState(byte_);
				byte[] array = new byte[2];
				if (ToAscii(keyboardHookStruct.vkCode, keyboardHookStruct.scanCode, byte_, array, keyboardHookStruct.flags) == 1)
				{
					KeyPressEventArgs e2 = new KeyPressEventArgs((char)array[0]);
					keyPressEventHandler_0(this, e2);
				}
			}
			if (keyEventHandler_1 != null && (int_2 == 257 || int_2 == 261))
			{
				KeyEventArgs e3 = new KeyEventArgs((Keys)keyboardHookStruct.vkCode);
				keyEventHandler_1(this, e3);
			}
		}
		return CallNextHookEx(int_0, int_1, int_2, intptr_0);
	}

	~KeyboardHook()
	{
		Stop();
	}
}
