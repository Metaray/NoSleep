using System;
using System.Runtime.InteropServices;

namespace NoSleep
{
    public class PowerManager : IDisposable
    {
        // https://stackoverflow.com/questions/629240/prevent-windows-from-going-into-sleep-when-my-program-is-running

        private POWER_REQUEST_CONTEXT powerRequestContext;

        private WinApiHandle powerRequest;

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
                    PowerSetRequest(powerRequest, PowerRequestType.PowerRequestDisplayRequired);
                }
                else
                {
                    PowerClearRequest(powerRequest, PowerRequestType.PowerRequestDisplayRequired);
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
                    PowerSetRequest(powerRequest, PowerRequestType.PowerRequestSystemRequired);
                }
                else
                {
                    PowerClearRequest(powerRequest, PowerRequestType.PowerRequestSystemRequired);
                }
                powerRequested = enable;
            }
        }

        #region P/Invoke

        // Availability Request Functions
        [DllImport("kernel32.dll")]
        static extern WinApiHandle PowerCreateRequest(ref POWER_REQUEST_CONTEXT Context);

        [DllImport("kernel32.dll")]
        static extern bool PowerSetRequest(WinApiHandle PowerRequestHandle, PowerRequestType RequestType);

        [DllImport("kernel32.dll")]
        static extern bool PowerClearRequest(WinApiHandle PowerRequestHandle, PowerRequestType RequestType);

        // Availablity Request Enumerations and Constants
        enum PowerRequestType
        {
            PowerRequestDisplayRequired = 0,
            PowerRequestSystemRequired,
            PowerRequestAwayModeRequired,
            PowerRequestMaximum
        }

        const int POWER_REQUEST_CONTEXT_VERSION = 0;
        const int POWER_REQUEST_CONTEXT_SIMPLE_STRING = 0x1;
        const int POWER_REQUEST_CONTEXT_DETAILED_STRING = 0x2;

        // Availablity Request Structures
        // Note:  Windows defines the POWER_REQUEST_CONTEXT structure with an
        // internal union of SimpleReasonString and Detailed information.
        // To avoid runtime interop issues, this version of 
        // POWER_REQUEST_CONTEXT only supports SimpleReasonString.  
        // To use the detailed information,
        // define the PowerCreateRequest function with the first 
        // parameter of type POWER_REQUEST_CONTEXT_DETAILED.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct POWER_REQUEST_CONTEXT
        {
            public UInt32 Version;
            public UInt32 Flags;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string SimpleReasonString;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PowerRequestContextDetailedInformation
        {
            public WinApiHandle LocalizedReasonModule;
            public UInt32 LocalizedReasonId;
            public UInt32 ReasonStringCount;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string[] ReasonStrings;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct POWER_REQUEST_CONTEXT_DETAILED
        {
            public UInt32 Version;
            public UInt32 Flags;
            public PowerRequestContextDetailedInformation DetailedInformation;
        }

        #endregion
    }
}
