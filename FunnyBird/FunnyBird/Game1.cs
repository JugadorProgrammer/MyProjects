using FunnyBird.Models;
using FunnyBird.Models.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework.Utilities.Deflate;

namespace FunnyBird
{
    /// <summary>
    /// Игра
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// Настройки
        /// </summary>
        private ProgramSettings _programSettings;
        /// <summary>
        /// Отображение графики
        /// </summary>
        private GraphicsDeviceManager _graphics;
        /// <summary>
        /// Отрисовка
        /// </summary>
        private SpriteBatch _spriteBatch;
        /// <summary>
        /// Текстутра птицы
        /// </summary>
        private Texture2D _birdTexture;
        /// <summary>
        /// Текстура основной части трубы
        /// </summary>
        private Texture2D _pipeTopTexture;
        /// <summary>
        /// Текстура фона
        /// </summary>
        private Texture2D _backgroundTexture;
        /// <summary>
        /// Шрифт
        /// </summary>
        private SpriteFont _spriteFont;
        /// <summary>
        /// Карта
        /// </summary>
        private GameMap _gameMap;
        /// <summary>
        /// Идёт ли игра
        /// </summary>
        private GameState _gameState;
        /// <summary>
        /// Направление
        /// </summary>
        private Direction _direction;

        /// <summary>
        /// Конструктор,инициализация
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _programSettings = ProgramSettings.GetInit();
            Content.RootDirectory = "Content";
            Window.Title = "FunnyBird";
            IsMouseVisible = true;
        }
        /// <summary>
        /// Инициализация
        /// </summary>
        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = _programSettings.WindowWidth;
            _graphics.PreferredBackBufferHeight = _programSettings.WindowHeight;
            _graphics.ApplyChanges();

            base.Initialize();
            //Before `LoadContent`

            _gameState = GameState.Pause;
            _gameMap = GameMap.GetInit(_birdTexture, _pipeTopTexture, _backgroundTexture, _spriteFont);
        }
        /// <summary>
        /// Загрузка контента
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _birdTexture = Content.Load<Texture2D>("Bird");
            _pipeTopTexture = Content.Load<Texture2D>("Chimney");
            _backgroundTexture = Content.Load<Texture2D>("Background");
            _spriteFont = Content.Load<SpriteFont>("mainFont");
        }
        /// <summary>
        /// Обновление
        /// </summary>
        /// <param name="gameTime">время в игре</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (gameTime.ElapsedGameTime.Ticks % _programSettings.TimerInterval > 5)
            {
                if (_gameState == GameState.Pause && Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    _gameState = GameState.Active;
                }
                else if (_gameState == GameState.Active)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        _direction = Direction.Up;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        _gameState = GameState.Pause;
                    }
                    else
                    {
                        _direction = Direction.Down;
                    }
                    _gameMap.UpDate(_direction, ref _gameState);
                }
                else if (_gameState == GameState.Loss && Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    _gameMap.Restart();
                    _gameState = GameState.Pause;
                    _gameMap = GameMap.GetInit(_birdTexture, _pipeTopTexture, _backgroundTexture, _spriteFont);
                }
            }
            
            base.Update(gameTime);
        }
        /// <summary>
        /// Отрисовка
        /// </summary>
        /// <param name="gameTime">время в игре</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _gameMap.Drow(_spriteBatch, in _gameState);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}