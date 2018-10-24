using System;
using System.Runtime.InteropServices;
using InteroperatingUnmanagedCode.NativePowerManager;

namespace InteroperatingUnmanagedCode.Managers
{
    static class BatteryManager
    {
        private static int SystemBatteryState = 5;

        public static uint GetSystemBatteryState()
        {
            PowerManagerInterop.SYSTEM_BATTERY_STATE batteryState = new PowerManagerInterop.SYSTEM_BATTERY_STATE();
            PowerManagerInterop.CallNtPowerInformation(SystemBatteryState, IntPtr.Zero, 0, batteryState, Marshal.SizeOf(typeof(PowerManagerInterop.SYSTEM_BATTERY_STATE)));
            return batteryState.RemainingCapacity;
        }

        
    }
}
