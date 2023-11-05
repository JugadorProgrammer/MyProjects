

namespace FunnyBird.Models.Settings
{
    /// <summary>
    /// Состояние игры
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Перед стартом
        /// </summary>
        Pause, 
        /// <summary>
        /// В игре
        /// </summary>
        Active,
        /// <summary>
        /// проигрыш
        /// </summary>
        Loss
    }
}
