using System;
using System.Runtime.InteropServices;

namespace InteroperatingUnmanagedCode.ManagedPowerManager
{
    [ComVisible(true)]
    [Guid("D821B5BB-423A-42E4-A20D-85326C861E3B")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    interface IPowerManager
    {
        uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            IntPtr lpOutputBuffer,
            int nOutputBufferSize
        );
        uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            bool lpOutputBuffer,
            int nOutputBufferSize
        );

        uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            long lpOutputBuffer,
            int nOutputBufferSize
        );

        bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

    }
}
