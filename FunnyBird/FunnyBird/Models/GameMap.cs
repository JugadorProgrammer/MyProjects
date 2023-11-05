using FunnyBird.Models.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FunnyBird.Models
{
    /// <summary>
    /// Карта
    /// </summary>
    public class GameMap
    {
        /// <summary>
        /// Настройки
        /// </summary>
        private ProgramSettings _programSettings;
        /// <summary>
        /// Трубы
        /// </summary>
        private List<Pipe> _pipes;
        /// <summary>
        /// Текстура трубы
        /// </summary>
        private Texture2D _pipeTopTexture;
        /// <summary>
        /// Рандомайзер
        /// </summary>
        private Random _random;
        /// <summary>
        /// Птица
        /// </summary>
        private Bird _bird;
        /// <summary>
        /// Фон
        /// </summary>
        private Texture2D _backgroundTexture;
        /// <summary>
        /// Шрифт
        /// </summary>
        private SpriteFont _font;
        /// <summary>
        /// Нужен ли перезапуск в функции GetInit
        /// </summary>
        public bool NeedRestart { get; private set; }

        /// <summary>
        /// Метод для отрисовки карты
        /// </summary>
        /// <param name="spriteBatch">Отрисовка</param>
        public void Drow(SpriteBatch spriteBatch, in GameState gameState)
        {
            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);
            _bird.Drow(spriteBatch);
            foreach (var pipe in _pipes)
            {
                pipe.Drow(spriteBatch);
            }

            switch (gameState)
            {
                case GameState.Pause:
                    spriteBatch.DrawString(_font, "Press `Enter` for start\nPress `Space` for stop",
                    new Vector2(_programSettings.WindowWidth / 2 - 200, _programSettings.WindowHeight / 2 - 100), Color.Black);
                    break;
                case GameState.Loss:
                    spriteBatch.DrawString(_font, "    You lose!\nPress `R` for restart !",
                    new Vector2(_programSettings.WindowWidth / 2 - 200, _programSettings.WindowHeight / 2 - 100), Color.Black);
                    break;

            }

        }
        /// <summary>
        /// Движение карты
        /// </summary>
        public void UpDate(Direction birdDirection, ref GameState gameState)
        {
            _bird.UpDate(birdDirection, ref gameState);
            if (gameState == GameState.Active)
            {
                for (int i = 0; i < _pipes.Count; ++i)
                {
                    _pipes[i].UpDate();
                    if (_pipes[i].IsContact(_bird))
                    {
                        gameState = GameState.Loss;
                        return;
                    }
                }

                if (_pipes.Count == 0
                    || _programSettings.WindowWidth - _pipes[_pipes.Count - 1].X > _programSettings.MaxWidthBetweenPipes + _programSettings.PipeWidth
                        && _random.Next(2) > 0 // Добавляем случайность генерацию труб
                        )
                {
                    ///Создаём новую трубу с координатой _programSettings.WindowWidth
                    var pipe = new Pipe(_pipeTopTexture);
                    _pipes.Add(pipe);
                }
            }
        }
        /// <summary>
        /// Sigletone
        /// </summary>
        private static GameMap _gameMap;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="birdTexture">Текстура птицы</param>
        /// <param name="pipeTopTexture">Текстура трубы</param>
        /// <param name="backgroundTexture">Текстура фона</param>
        /// <param name="font">Шрифт</param>
        private GameMap(Texture2D birdTexture, Texture2D pipeTopTexture, Texture2D backgroundTexture, SpriteFont font)
        {
            _font = font;
            _random = new Random();
            _pipes = new List<Pipe>();
            _pipeTopTexture = pipeTopTexture;
            _backgroundTexture = backgroundTexture;
            _programSettings = ProgramSettings.GetInit();
            _bird = new Bird(birdTexture, new Vector2(30, _programSettings.WindowHeight / 2), _programSettings.BirdWidth, _programSettings.WindowHeight);
        }
        public static GameMap GetInit(Texture2D birdTexture, Texture2D pipeTopTexture, Texture2D backgroundTexture, SpriteFont font)
        {
            if (_gameMap == null || _gameMap.NeedRestart) { _gameMap = new GameMap(birdTexture, pipeTopTexture, backgroundTexture, font); }
            return _gameMap;
        }

        /// <summary>
        /// Перезапуск карты
        /// </summary>
        /// <param name="birdTexture">Текстура птицы</param>
        /// <param name="pipeTopTexture">Текстура трубы</param>
        /// <param name="backgroundTexture">Текстура фона</param>
        /// <param name="font">Шрифт</param>
        public void Restart()
        {
            _gameMap.NeedRestart = true;
        }
    }
}
