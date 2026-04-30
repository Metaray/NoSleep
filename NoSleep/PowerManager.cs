using System;
using System.Runtime.InteropServices;

namespace NoSleep
{
    public class PowerManager : IDisposable
    {
        // https://stackoverflow.com/questions/629240/prevent-windows-from-going-into-sleep-when-my-program-is-running

        private readonly REASON_CONTEXT powerRequestContext;

        private readonly WinApiHandle powerRequest;

        private bool displayRequested = false;

        private bool powerRequested = false;

        public PowerManager()
        {
            // Set up the diagnostic string
            powerRequestContext.Version = POWER_REQUEST_CONTEXT_VERSION;
            powerRequestContext.Flags = POWER_REQUEST_CONTEXT_SIMPLE_STRING;
            powerRequestContext.SimpleReasonString = "Sleep preventor app";

            // Create the request, get a handle
            powerRequest = PowerCreateRequest(ref powerRequestContext);

            if (powerRequest.IsInvalid)
            {
                throw new Exception($"Failed to create power request (code {Marshal.GetLastWin32Error()})");
            }
        }

        public void Dispose()
        {
            powerRequest.Dispose();
        }

        public void EnableConstantDisplay(bool enable)
        {
            if (enable != displayRequested)
            {
                if (enable)
                {
                    AssertPowerCall(PowerSetRequest(powerRequest, PowerRequestType.PowerRequestDisplayRequired));
                }
                else
                {
                    AssertPowerCall(PowerClearRequest(powerRequest, PowerRequestType.PowerRequestDisplayRequired));
                }
                displayRequested = enable;
            }
        }

        public void EnableConstantPower(bool enable)
        {
            if (enable != powerRequested)
            {
                if (enable)
                {
                    AssertPowerCall(PowerSetRequest(powerRequest, PowerRequestType.PowerRequestSystemRequired));
                }
                else
                {
                    AssertPowerCall(PowerClearRequest(powerRequest, PowerRequestType.PowerRequestSystemRequired));
                }
                powerRequested = enable;
            }
        }

        private static void AssertPowerCall(bool result)
        {
            if (!result)
            {
                throw new Exception($"Power call failed (code {Marshal.GetLastWin32Error()})");
            }
        }

        #region P/Invoke

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern WinApiHandle PowerCreateRequest(ref REASON_CONTEXT Context);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PowerSetRequest(WinApiHandle PowerRequestHandle, PowerRequestType RequestType);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PowerClearRequest(WinApiHandle PowerRequestHandle, PowerRequestType RequestType);

        private enum PowerRequestType
        {
            PowerRequestDisplayRequired = 0,
            PowerRequestSystemRequired,
            PowerRequestAwayModeRequired,
            PowerRequestMaximum
        }

        private const int POWER_REQUEST_CONTEXT_VERSION = 0;
        private const int POWER_REQUEST_CONTEXT_SIMPLE_STRING = 0x1;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct REASON_CONTEXT
        {
            public UInt32 Version;
            public UInt32 Flags;
            
            // Only simple reason supported
            [MarshalAs(UnmanagedType.LPWStr)]
            public string SimpleReasonString;
        }

        #endregion
    }
}
