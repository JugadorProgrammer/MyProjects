
namespace PlatformsMonoGame.PhysicalSettings
{
    public static class Settings
    {
        public static int WindowHeight { get; } = 500;
        public static int WindowWidth { get; } = 500;
        public static int PlatformWidth { get; } = 150;
        public static int PlatformHeight { get; } = 50;
        public static int BallRadius { get; } = 25;

        public static ulong TimerInterval { get; } = 2;

        public static float Speed { get; } = 2F;
        public static float ContactRadius { get; } = 0.5F;


    }
}
