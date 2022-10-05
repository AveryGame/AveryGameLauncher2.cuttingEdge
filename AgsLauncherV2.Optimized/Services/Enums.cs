#pragma warning disable CS0649

namespace AgsLauncherV2.Optimized.Services
{
    internal class Enums
    {
        public enum LauncherStatus
        {
            intializing,
            initialized,
            downloading,
            gameLaunched,
            updating,
            waitingRPC
        }
        public static LauncherStatus launcherStatus;
    }
}
