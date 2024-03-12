using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NoSleep
{
    public class MouseJiggler
    {
        private int jiggleTick = 0;

        private int lastInputTime = 0;

        public void Jiggle()
        {
            int newLastInput = GetLastInputTime();

            if (newLastInput != lastInputTime)
            {
                lastInputTime = newLastInput;
                return;
            }

            JiggleTick();

            // Injection of keystrokes triggers time change
            lastInputTime = GetLastInputTime();
        }

        private int GetLastInputTime()
        {
            var lii = new LASTINPUTINFO
            {
                cbSize = Marshal.SizeOf(typeof(LASTINPUTINFO)),
            };

            if (!GetLastInputInfo(ref lii))
            {
                Trace.WriteLine("GetLastInputInfo failed");
                return lastInputTime;
            }

            return lii.dwTime;
        }

        private void JiggleTick()
        {
            // Greater-than-one distance to move on HI-DPI displays
            const int Distance = 3;
            int delta = ((jiggleTick++) & 1) == 0 ? Distance : -Distance;
            MoveDelta(delta, delta);

            //var pos = Cursor.Position;
            //pos.Y += delta;
            //Cursor.Position = pos;
        }

        private void MoveDelta(int deltaX, int deltaY)
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

            uint sent = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
            
            if (sent != inputs.Length)
            {
                Trace.WriteLine("SendInput failed");
            }
        }

        #region P/Invoke

        [DllImport("user32.dll")]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

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

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            internal int cbSize;
            internal int dwTime;
        }

        #endregion
    }
}
