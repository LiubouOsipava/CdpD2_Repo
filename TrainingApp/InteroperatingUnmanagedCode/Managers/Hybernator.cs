using InteroperatingUnmanagedCode.NativePowerManager;

namespace InteroperatingUnmanagedCode.Managers
{
    static class Hybernator
    {
        public static bool Sleep()
        {
            return PowerManagerInterop.SetSuspendState(false, false, false);
        }

        public static bool Hybernate()
        {
            return PowerManagerInterop.SetSuspendState(false, false, false);
        }
    }
}
