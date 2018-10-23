using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InteroperatingUnmanagedCode
{
    static class Hybernator
    {
        [DllImport("Powrprof.dll", SetLastError = true)]
        static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        public static bool Sleep()
        {
            return SetSuspendState(false, true, true);
        }

        public static bool Hybernate()
        {
            return SetSuspendState(false, true, true);
        }
    }
}
