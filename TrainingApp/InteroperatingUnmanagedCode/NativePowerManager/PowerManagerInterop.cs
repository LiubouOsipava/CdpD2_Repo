using System;
using System.Runtime.InteropServices;

namespace InteroperatingUnmanagedCode.NativePowerManager
{
    internal class PowerManagerInterop
    {
        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            IntPtr lpOutputBuffer,
            int nOutputBufferSize
        );

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            [MarshalAs(UnmanagedType.Bool)][Out] bool lpOutputBuffer,
            int nOutputBufferSize
        );

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            [Out] SYSTEM_BATTERY_STATE lpOutputBuffer,
            int nOutputBufferSize
        );

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            [Out] Int64 lpOutputBuffer,
            int nOutputBufferSize
        );

        [DllImport("Powrprof.dll", SetLastError = true)]
        internal static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct SYSTEM_POWER_LEVEL
        {
            public bool Enable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Spare;
            public uint BatteryLevel;
            public POWER_ACTION_POLICY PowerPolicy;
            public SYSTEM_POWER_STATE MinSystemState;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct POWER_ACTION_POLICY
        {
            public POWER_ACTION Action;
            public PowerActionFlags Flags;
            public PowerActionEventCode EventCode;
        }
        internal enum POWER_ACTION : uint
        {
            PowerActionNone = 0,       // No system power action. 
            PowerActionReserved,       // Reserved; do not use. 
            PowerActionSleep,      // Sleep. 
            PowerActionHibernate,      // Hibernate. 
            PowerActionShutdown,       // Shutdown. 
            PowerActionShutdownReset,  // Shutdown and reset. 
            PowerActionShutdownOff,    // Shutdown and power off. 
            PowerActionWarmEject,      // Warm eject.
        }

        [Flags]
        internal enum PowerActionFlags : uint
        {
            POWER_ACTION_QUERY_ALLOWED = 0x00000001, // Broadcasts a PBT_APMQUERYSUSPEND event to each application to request permission to suspend operation.
            POWER_ACTION_UI_ALLOWED = 0x00000002, // Applications can prompt the user for directions on how to prepare for suspension. Sets bit 0 in the Flags parameter passed in the lParam parameter of WM_POWERBROADCAST.
            POWER_ACTION_OVERRIDE_APPS = 0x00000004, // Ignores applications that do not respond to the PBT_APMQUERYSUSPEND event broadcast in the WM_POWERBROADCAST message.
            POWER_ACTION_LIGHTEST_FIRST = 0x10000000, // Uses the first lightest available sleep state.
            POWER_ACTION_LOCK_CONSOLE = 0x20000000, // Requires entry of the system password upon resume from one of the system standby states.
            POWER_ACTION_DISABLE_WAKES = 0x40000000, // Disables all wake events.
            POWER_ACTION_CRITICAL = 0x80000000, // Forces a critical suspension.
        }

        [Flags]
        internal enum PowerActionEventCode : uint
        {
            POWER_LEVEL_USER_NOTIFY_TEXT = 0x00000001, // User notified using the UI. 
            POWER_LEVEL_USER_NOTIFY_SOUND = 0x00000002, // User notified using sound. 
            POWER_LEVEL_USER_NOTIFY_EXEC = 0x00000004, // Specifies a program to be executed. 
            POWER_USER_NOTIFY_BUTTON = 0x00000008, // Indicates that the power action is in response to a user power button press. 
            POWER_USER_NOTIFY_SHUTDOWN = 0x00000010, // Indicates a power action of shutdown/off.
            POWER_FORCE_TRIGGER_RESET = 0x80000000, // Clears a user power button press. 
        }

        internal enum SYSTEM_POWER_STATE
        {
            PowerSystemUnspecified = 0,
            PowerSystemWorking = 1,
            PowerSystemSleeping1 = 2,
            PowerSystemSleeping2 = 3,
            PowerSystemSleeping3 = 4,
            PowerSystemHibernate = 5,
            PowerSystemShutdown = 6,
            PowerSystemMaximum = 7
        }



        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct SYSTEM_BATTERY_STATE
        {
            public byte AcOnLine;
            public byte BatteryPresent;
            public byte Charging;
            public byte Discharging;
            public byte spare1;
            public byte spare2;
            public byte spare3;
            public byte spare4;
            public UInt32 MaxCapacity;
            public UInt32 RemainingCapacity;
            public Int32 Rate;
            public UInt32 EstimatedTime;
            public UInt32 DefaultAlert1;
            public UInt32 DefaultAlert2;
        }
        
    }


}
