using System;
using System.Runtime.InteropServices;
using InteroperatingUnmanagedCode.NativePowerManager;

namespace InteroperatingUnmanagedCode.ManagedPowerManager
{
    [ComVisible(true)]
    [Guid("E7E20B95-6D50-4860-B94F-18020AFC320E")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PowerManager:IPowerManager
    {
        public uint CallNtPowerInformation(int InformationLevel, IntPtr lpInputBuffer, int nInputBufferSize, IntPtr lpOutputBuffer,
            int nOutputBufferSize)
        {
            return PowerManagerInterop.CallNtPowerInformation(InformationLevel, lpInputBuffer, nInputBufferSize,
                lpOutputBuffer,
                nOutputBufferSize);
        }

        public uint CallNtPowerInformation(int InformationLevel, IntPtr lpInputBuffer, int nInputBufferSize, bool lpOutputBuffer,
            int nOutputBufferSize)
        {
            return PowerManagerInterop.CallNtPowerInformation(InformationLevel, lpInputBuffer, nInputBufferSize,
                lpOutputBuffer,
                nOutputBufferSize);
        }

        public uint CallNtPowerInformation(int InformationLevel, IntPtr lpInputBuffer, int nInputBufferSize, long lpOutputBuffer,
            int nOutputBufferSize)
        {
            return PowerManagerInterop.CallNtPowerInformation(InformationLevel, lpInputBuffer, nInputBufferSize,
                lpOutputBuffer,
                nOutputBufferSize);
        }

        public bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent)
        {
            return PowerManagerInterop.SetSuspendState(hibernate, forceCritical, disableWakeEvent);
        }
    }
   
}
