using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InteroperatingUnmanagedCode
{
    static class HybernateFileManager
    {
        private static uint STATUS_SUCCESS = 0;
        private static int SystemReserveHiberFile = 10;

        [DllImport("powrprof.dll")]
        static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            [Out] BoolConversion lpOutputBuffer,
            int nOutputBufferSize
        );

        public static bool ManageHybernateFile(bool isHybernateFileReserved)
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(BoolConversion)));
            Marshal.WriteInt32(buffer, 0, new BoolConversion { boolValue = isHybernateFileReserved }.byteValue);
            BoolConversion actionPerformed = new BoolConversion();
            uint result = CallNtPowerInformation(SystemReserveHiberFile, buffer, 0, actionPerformed, Marshal.SizeOf(typeof(bool)));
            if (result == STATUS_SUCCESS)
            {
                //actionPerformed = (BoolConversion)Marshal.PtrToStructure(buffer, typeof(BoolConversion));
                return actionPerformed.boolValue;
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }


        [StructLayout(LayoutKind.Explicit)]
        struct BoolConversion
        {
            [FieldOffset(0)]
            public bool boolValue;
            [FieldOffset(0)]
            public byte byteValue;
        }



    }
}
