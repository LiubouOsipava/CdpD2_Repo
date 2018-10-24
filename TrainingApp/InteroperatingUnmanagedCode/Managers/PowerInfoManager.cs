using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace InteroperatingUnmanagedCode.Managers
{
    static class PowerInfoManager
    {
        static int SystemPowerInformation = 12;
        static uint STATUS_SUCCESS = 0;
        public static byte[] GetSystemPowerInformation()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(PowerManagerInterop.SYSTEM_POWER_LEVEL)));
            uint result = PowerManagerInterop.CallNtPowerInformation(SystemPowerInformation, IntPtr.Zero, 0, buffer, Marshal.SizeOf(typeof(PowerManagerInterop.SYSTEM_POWER_LEVEL)));
            if (result == STATUS_SUCCESS)
            {
                PowerManagerInterop.SYSTEM_POWER_LEVEL powerInfo = (PowerManagerInterop.SYSTEM_POWER_LEVEL)Marshal.PtrToStructure(buffer, typeof(PowerManagerInterop.SYSTEM_POWER_LEVEL));
                Console.WriteLine($"Battery Level: {powerInfo.BatteryLevel}");
                return powerInfo.Spare;
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }


}
