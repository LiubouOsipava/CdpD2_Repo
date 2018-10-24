using System;
using System.Runtime.InteropServices;

namespace InteroperatingUnmanagedCode.Managers
{
    static class BatteryManager
    {
        private static uint STATUS_SUCCESS = 0;
        private static int SystemBatteryState = 5;

        

        public static UInt32 GetSystemBatteryState()
        {
            //IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SYSTEM_BATTERY_STATE)));
            PowerManagerInterop.SYSTEM_BATTERY_STATE batteryState = new PowerManagerInterop.SYSTEM_BATTERY_STATE();
            PowerManagerInterop.CallNtPowerInformation(SystemBatteryState, IntPtr.Zero, 0, batteryState, Marshal.SizeOf(typeof(PowerManagerInterop.SYSTEM_BATTERY_STATE)));
            
            //batteryState = (SYSTEM_BATTERY_STATE)Marshal.PtrToStructure(buffer, typeof(SYSTEM_BATTERY_STATE));
            Console.WriteLine($"Battery Level ET: {batteryState.EstimatedTime}");
            return batteryState.MaxCapacity;
           
        }

        
    }
}
