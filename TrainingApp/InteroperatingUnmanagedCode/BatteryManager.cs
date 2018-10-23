using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InteroperatingUnmanagedCode
{
    static class BatteryManager
    {
        private static uint STATUS_SUCCESS = 0;
        private static int SystemBatteryState = 5;

        [DllImport("powrprof.dll")]
        static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            [Out] SYSTEM_BATTERY_STATE lpOutputBuffer,
            int nOutputBufferSize
        );

        public static UInt32 GetSystemBatteryState()
        {
            //IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SYSTEM_BATTERY_STATE)));
            SYSTEM_BATTERY_STATE batteryState = new SYSTEM_BATTERY_STATE();
            CallNtPowerInformation(SystemBatteryState, IntPtr.Zero, 0, batteryState, Marshal.SizeOf(typeof(SYSTEM_BATTERY_STATE)));
            
            //batteryState = (SYSTEM_BATTERY_STATE)Marshal.PtrToStructure(buffer, typeof(SYSTEM_BATTERY_STATE));
            Console.WriteLine($"Battery Level ET: {batteryState.EstimatedTime}");
            return batteryState.MaxCapacity;
           
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct SYSTEM_BATTERY_STATE
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
