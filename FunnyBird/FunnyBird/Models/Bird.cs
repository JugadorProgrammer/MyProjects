using FunnyBird.Models.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FunnyBird.Models
{
    /// <summary>
    /// Птица
    /// </summary>
    public class Bird : SpriteModel
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="texture">Текстура</param>
        /// <param name="positionVector">Вектор с позицией</param>
        /// <param name="width">Ширина птицы</param>
        /// <param name="height">Высота птицы</param>
        public Bird(Texture2D texture, Vector2 positionVector, float width, float height)
            : base(texture, width, height)
        {
            PositionVector = positionVector;
        }
        /// <summary>
        /// Обновление позиции
        /// </summary>
        /// <param name="direction">позиция</param>
        public void UpDate(Direction direction, ref GameState gameState)
        {
            if (gameState == GameState.Active)
            {
                if (PositionVector.Y >= _programSettings.WindowHeight
                        || PositionVector.Y + _programSettings.BirdHeight <= 0)
                {
                    gameState = GameState.Loss;
                }
                else
                {
                    this.MoveOn(0, _programSettings.BirdSpeed * (int)direction);
                }
            }
        }
    }
}
