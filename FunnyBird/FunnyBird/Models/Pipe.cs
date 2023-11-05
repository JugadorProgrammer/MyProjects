using FunnyBird.Models.Settings;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
namespace FunnyBird.Models
{
    /// <summary>
    /// Труба
    /// </summary>
    public class Pipe
    {
        /// <summary>
        /// Координата X трубы
        /// </summary>
        public float X { get => _pipes.LastOrDefault().PositionVector.X; }
        /// <summary>
        /// Рандомайзер
        /// </summary>
        private Random _random;
        /// <summary>
        /// Настройки
        /// </summary>
        private ProgramSettings _settings;
        /// <summary>
        /// Составные части трубы
        /// </summary>
        private List<SpriteModel> _pipes;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pipeTopTexture">Текстура трубы</param>
        public Pipe(Texture2D pipeTopTexture)
        {
            _random = new Random();
            _pipes = new List<SpriteModel>();
            _settings = ProgramSettings.GetInit();
            int pipeCount = (int)((_settings.WindowHeight - _settings.BirdHeight * _random.Next(3, 6)) / _settings.PipeHeight),
                randButtom = _random.Next(1, pipeCount);//нижняя труба


            for (int i = 0; i < randButtom; ++i)
            {
                var partOfPipe = new SpriteModel(pipeTopTexture, _settings.PipeWidth, _settings.PipeHeight);
                partOfPipe.SetPositionVector(new Vector2(_settings.WindowWidth,
                    _settings.WindowHeight - (i + 1) * _settings.PipeHeight));

                _pipes.Add(partOfPipe);
            }

            for (int i = 0; i < pipeCount - randButtom; ++i)
            {
                var partOfPipe = new SpriteModel(pipeTopTexture, _settings.PipeWidth, _settings.PipeHeight);
                partOfPipe.SetPositionVector(new Vector2(_settings.WindowWidth,
                    i * _settings.PipeHeight));

                _pipes.Add(partOfPipe);
            }
        }
        /// <summary>
        /// Функция отрисовки
        /// </summary>
        /// <param name="spriteBatch">Для отрисовки</param>
        public void Drow(SpriteBatch spriteBatch)
        {
            foreach (var pipe in _pipes)
            {
                pipe.Drow(spriteBatch);
            }
        }
        /// <summary>
        /// Обновление
        /// </summary>
        public void UpDate()
        {
            for (int i = 0; i < _pipes.Count; ++i)
            {
                _pipes[i].MoveOn(-5, 0);
            }
        }
        /// <summary>
        /// Произошёл ли контакт
        /// </summary>
        /// <returns>Логическое значение</returns>
        public bool IsContact(Bird bird)
        {
            for (int i = 0; i < _pipes.Count; ++i)
            {
                if (//Если птица находиться в трубе по OX
                    (bird.PositionVector.X + _settings.BirdWidth > _pipes[i].PositionVector.X
                        && bird.PositionVector.X < _pipes[i].PositionVector.X + _settings.PipeWidth)
                    //Если птица находиться в трубе по OX
                    && (bird.PositionVector.Y + _settings.BirdHeight > _pipes[i].PositionVector.Y
                        && bird.PositionVector.Y + _settings.BirdHeight < _pipes[i].PositionVector.Y + _settings.PipeHeight))
                {
                    return true;
                }
            }
            return false;
        }



    }
}
