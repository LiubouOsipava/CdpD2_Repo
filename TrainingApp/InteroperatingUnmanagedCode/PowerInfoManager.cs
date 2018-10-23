using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InteroperatingUnmanagedCode
{
    public class PowerInfoManager
    {
        static int SystemPowerInformation = 12;
        static uint STATUS_SUCCESS = 0;

        [DllImport("powrprof.dll")]
        static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            IntPtr lpOutputBuffer,
            int nOutputBufferSize
        );
        
        public static byte[] GetSystemPowerInformation()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SYSTEM_POWER_LEVEL)));
            uint result = CallNtPowerInformation(SystemPowerInformation, IntPtr.Zero, 0, buffer, Marshal.SizeOf(typeof(SYSTEM_POWER_LEVEL)));
            if (result == STATUS_SUCCESS)
            {
                SYSTEM_POWER_LEVEL powerInfo = (SYSTEM_POWER_LEVEL)Marshal.PtrToStructure(buffer, typeof(SYSTEM_POWER_LEVEL));
                Console.WriteLine($"Battery Level: {powerInfo.BatteryLevel}");
                return powerInfo.Spare;
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        
        #region PowerInfo

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct SYSTEM_POWER_LEVEL
        {
            public bool Enable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Spare;
            public uint BatteryLevel;
            public POWER_ACTION_POLICY PowerPolicy;
            public SYSTEM_POWER_STATE MinSystemState;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct POWER_ACTION_POLICY
        {
            public POWER_ACTION Action;
            public PowerActionFlags Flags;
            public PowerActionEventCode EventCode;
        }
        enum POWER_ACTION : uint
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
        enum PowerActionFlags : uint
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
        enum PowerActionEventCode : uint
        {
            POWER_LEVEL_USER_NOTIFY_TEXT = 0x00000001, // User notified using the UI. 
            POWER_LEVEL_USER_NOTIFY_SOUND = 0x00000002, // User notified using sound. 
            POWER_LEVEL_USER_NOTIFY_EXEC = 0x00000004, // Specifies a program to be executed. 
            POWER_USER_NOTIFY_BUTTON = 0x00000008, // Indicates that the power action is in response to a user power button press. 
            POWER_USER_NOTIFY_SHUTDOWN = 0x00000010, // Indicates a power action of shutdown/off.
            POWER_FORCE_TRIGGER_RESET = 0x80000000, // Clears a user power button press. 
        }

        public enum SYSTEM_POWER_STATE
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

        #endregion
    }
}
