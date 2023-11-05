

namespace FunnyBird.Models.Settings
{
    /// <summary>
    /// Настройки программы
    /// </summary>
    public class ProgramSettings
    {
        /// <summary>
        /// Высота окна
        /// </summary>
        public  int WindowHeight { get; } = 700;
        /// <summary>
        /// Ширина окна
        /// </summary>
        public  int WindowWidth { get; } = 1000;
        /// <summary>
        /// Максимальное расстояние между двумя трубами
        /// </summary>
        public int MaxWidthBetweenPipes { get; } = 200;
        /// <summary>
        /// Ширина птицы
        /// </summary>
        public  float BirdWidth { get; } = 70;
        /// <summary>
        /// Высота птицы
        /// </summary>
        public float BirdHeight { get; } = 60;
        /// <summary>
        /// Ширина трубы
        /// </summary>
        public float PipeWidth { get; } = 50;
        /// <summary>
        /// Высота трубы
        /// </summary>
        public float PipeHeight { get; } = 70;
        /// <summary>
        /// Временной интервал обновлени окна
        /// </summary>
        public int TimerInterval { get; } = 10;
        /// <summary>
        /// Скорость движения птицы вниз
        /// </summary>
        public  float MapSpeed { get; } = -1F;
        /// <summary>
        /// Скорость птицы
        /// </summary>
        public  float BirdSpeed { get; } = 6F;
        /// <summary>
        /// Радиус,который определя
        /// </summary>
        public  float ContactRadius { get; } = 0.5F;

        /// <summary>
        /// Singletone
        /// </summary>
        private static ProgramSettings _programSettings;
        private ProgramSettings() { }
        public static ProgramSettings GetInit()
        {
            if (_programSettings == null) { _programSettings = new ProgramSettings(); }
            return _programSettings;
        }
    }
}
