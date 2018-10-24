using System;
using System.Runtime.InteropServices;

namespace InteroperatingUnmanagedCode.Managers
{
    static class SleepWakeTimeManager
    {
        private static int LastSleepTime = 15;
        private static int LastWakeTime = 14;
        const uint STATUS_SUCCESS = 0;
        
        public static void GetLastSleepTime()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Int64)));
            uint result = PowerManagerInterop.CallNtPowerInformation(SleepWakeTimeManager.LastSleepTime, IntPtr.Zero, 0, buffer.ToInt64(), Marshal.SizeOf(typeof(Int64)));
            //if (result == STATUS_SUCCESS)
            //{
                Console.WriteLine($"Interrupt-time count: {buffer}");
            //}
            //else
            //{
            //    throw new Win32Exception(Marshal.GetLastWin32Error());
            //}
        }

        public static void GetLastWakeTime()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Int64)));
            uint result = PowerManagerInterop.CallNtPowerInformation(SleepWakeTimeManager.LastWakeTime, IntPtr.Zero, 0, buffer.ToInt64(), Marshal.SizeOf(typeof(Int64)));
            //if (result == STATUS_SUCCESS)
            //{
                Console.WriteLine($"Interrupt-time count: {buffer}");
            //}
            //else
            //{
            //    throw new Win32Exception(Marshal.GetLastWin32Error());
            //}
        }
    }
}
