using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace NoSleep
{
	public class WinApiHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		public WinApiHandle() : base(true) { }

		protected override bool ReleaseHandle()
		{
			return CloseHandle(handle);
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr hObject);
	}
}
