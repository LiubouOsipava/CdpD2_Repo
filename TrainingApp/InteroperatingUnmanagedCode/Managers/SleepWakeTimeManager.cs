using System;
using System.Runtime.InteropServices;
using InteroperatingUnmanagedCode.NativePowerManager;

namespace InteroperatingUnmanagedCode.Managers
{
    static class SleepWakeTimeManager
    {
        private static int LastSleepTime = 15;
        private static int LastWakeTime = 14;
        
        public static Int64 GetLastSleepTime()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Int64)));
            PowerManagerInterop.CallNtPowerInformation(SleepWakeTimeManager.LastSleepTime, IntPtr.Zero, 0, buffer.ToInt64(), Marshal.SizeOf(typeof(Int64)));
            
            return buffer.ToInt64();
        }

        public static Int64 GetLastWakeTime()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Int64)));
            PowerManagerInterop.CallNtPowerInformation(SleepWakeTimeManager.LastWakeTime, IntPtr.Zero, 0, buffer.ToInt64(), Marshal.SizeOf(typeof(Int64)));
            
            return buffer.ToInt64();
        }
    }
}
