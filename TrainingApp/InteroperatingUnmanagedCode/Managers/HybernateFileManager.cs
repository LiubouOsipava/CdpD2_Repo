using System;
using System.Runtime.InteropServices;

namespace InteroperatingUnmanagedCode.Managers
{
    static class HybernateFileManager
    {
        private static uint STATUS_SUCCESS = 0;
        private static int SystemReserveHiberFile = 10;

        
        public static bool ManageHybernateFile(bool isHybernateFileReserved)
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(bool)));
            Marshal.WriteInt32(buffer, 0, Convert.ToInt32(isHybernateFileReserved));
            bool resultState = false;
            uint result = PowerManagerInterop.CallNtPowerInformation(SystemReserveHiberFile, buffer, 0, resultState, Marshal.SizeOf(typeof(bool)));
            //if (result == STATUS_SUCCESS)
            //{
                return resultState;
            //}
            //else
            //{
            //    throw new Win32Exception(Marshal.GetLastWin32Error());
            //}
        }
    }
}
