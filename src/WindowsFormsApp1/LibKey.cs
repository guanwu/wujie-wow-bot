using System.Collections.Generic;

namespace WindowsFormsApp1;

internal class LibKey
{
	public Dictionary<string, byte> keys_byte = new Dictionary<string, byte>();

	public Dictionary<string, string> keys_string = new Dictionary<string, string>();

	public LibKey()
	{
		for (int i = 65; i <= 90; i++)
		{
			keys_byte.Add(((char)i).ToString(), (byte)i);
		}
		for (int j = 48; j <= 57; j++)
		{
			keys_byte.Add(((char)j).ToString(), (byte)j);
		}
		for (int k = 112; k <= 123; k++)
		{
			keys_byte.Add("F" + (k - 111), (byte)k);
		}
		for (int l = 96; l <= 105; l++)
		{
			keys_byte.Add("Numpad" + (l - 96), (byte)l);
		}
		keys_byte.Add("Lmenu", 18);
		keys_byte.Add("Lshift", 16);
		keys_byte.Add("Space", 32);
		keys_byte.Add("Lcontrol", 17);
		keys_byte.Add("Subtract", 109);
		keys_byte.Add("Add", 107);
		for (int m = 1; m <= 12; m++)
		{
			keys_string.Add("F" + m, "{F" + m + "}");
		}
	}
}
