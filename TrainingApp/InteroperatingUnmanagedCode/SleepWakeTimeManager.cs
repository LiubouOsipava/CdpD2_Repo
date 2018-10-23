using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace InteroperatingUnmanagedCode
{
    static class SleepWakeTimeManager
    {
        private static int LastSleepTime = 15;
        private static int LastWakeTime = 14;
        const uint STATUS_SUCCESS = 0;

        [DllImport("powrprof.dll")]
        static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            IntPtr lpOutputBuffer,
            int nOutputBufferSize
        );


        public static void GetLastSleepTime()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(long)));
            uint result = CallNtPowerInformation(SleepWakeTimeManager.LastSleepTime, IntPtr.Zero, 0, buffer, Marshal.SizeOf(typeof(long)));
            if (result == STATUS_SUCCESS)
            {
                Console.WriteLine($"Interrupt-time count: {buffer}");
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public static void GetLastWakeTime()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(long)));
            uint result = CallNtPowerInformation(SleepWakeTimeManager.LastWakeTime, IntPtr.Zero, 0, buffer, Marshal.SizeOf(typeof(long)));
            if (result == STATUS_SUCCESS)
            {
                Console.WriteLine($"Interrupt-time count: {buffer}");
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

    }
}
