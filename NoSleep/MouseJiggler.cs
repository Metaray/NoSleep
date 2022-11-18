using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NoSleep
{
	public static class MouseJiggler
	{
		public static void MoveDelta(int deltaX, int deltaY)
		{
			var inputs = new INPUT[]
			{
				new INPUT
				{
					type = InputType.INPUT_MOUSE,
					U = new InputUnion
					{
						mi = new MOUSEINPUT
						{
							dx = deltaX,
							dy = deltaY,
							mouseData = 0,
							dwFlags = MOUSEEVENTF.MOVE,
							dwExtraInfo = UIntPtr.Zero,
							time = 0,
						}
					}
				}
			};
			var sent = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
			if (sent != inputs.Length)
			{
				Trace.WriteLine("SendInput failed");
			}
		}

		#region P/Invoke

		[DllImport("user32.dll")]
		static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

		[StructLayout(LayoutKind.Sequential)]
		struct INPUT
		{
			internal InputType type;
			internal InputUnion U;
		}

		enum InputType : uint
		{
			INPUT_MOUSE,
			INPUT_KEYBOARD,
			INPUT_HARDWARE
		}

		[StructLayout(LayoutKind.Explicit)]
		struct InputUnion
		{
			[FieldOffset(0)]
			internal MOUSEINPUT mi;
			//[FieldOffset(0)]
			//internal KEYBDINPUT ki;
			//[FieldOffset(0)]
			//internal HARDWAREINPUT hi;
		}

		[StructLayout(LayoutKind.Sequential)]
		struct MOUSEINPUT
		{
			internal int dx;
			internal int dy;
			internal int mouseData;
			internal MOUSEEVENTF dwFlags;
			internal uint time;
			internal UIntPtr dwExtraInfo;
		}

		[Flags]
		enum MOUSEEVENTF : uint
		{
			ABSOLUTE = 0x8000,
			HWHEEL = 0x01000,
			MOVE = 0x0001,
			MOVE_NOCOALESCE = 0x2000,
			LEFTDOWN = 0x0002,
			LEFTUP = 0x0004,
			RIGHTDOWN = 0x0008,
			RIGHTUP = 0x0010,
			MIDDLEDOWN = 0x0020,
			MIDDLEUP = 0x0040,
			VIRTUALDESK = 0x4000,
			WHEEL = 0x0800,
			XDOWN = 0x0080,
			XUP = 0x0100
		}

		#endregion
	}
}
