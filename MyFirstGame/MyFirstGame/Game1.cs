using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.Model.Field;
using MyFirstGame.Model.Sanke;
using MyFirstGame.Model.Physics;
using MyFirstGame.Model.Direction;
using System.Timers;
using System.Diagnostics;
using System;
using MyFirstGame.Model.SnakeState;

namespace MyFirstGame
{
    public class Game1 : Game
    {
        private int _timer;
        private long _pauseTimeInSeconds;
        private Stopwatch _stopwatch;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _mainFont;
        private SpriteFont _scoreFont;
        private SpriteFont _startFont;

        private Snake _snake;
        private GameField _gameField;

        private Texture2D[] _snakeHeadTexture;
        private Texture2D _snakePartTexture;
        private Texture2D _appleTexture;
        private Texture2D _boomTexture;
        private Texture2D _gameFieldTexture;
        private Texture2D _gameDeadTexture;
        private Texture2D _gameWinTexture;
        private Texture2D _deadSnakeTexture;
        private Texture2D _spaceButtonTexture;

        private Direction _direction;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Window.Title = "Snake";

            _timer = 1;
            _pauseTimeInSeconds = 0;
            _direction = Direction.Up;
            _stopwatch = new Stopwatch();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = PhysicData.FieldWidth * PhysicData.FieldPartCount;
            _graphics.PreferredBackBufferHeight = PhysicData.FieldHeight * PhysicData.FieldPartCount;
            _graphics.ApplyChanges();

            base.Initialize();

            _snake = new Snake(_snakeHeadTexture, _snakePartTexture, _deadSnakeTexture, new Vector2(PhysicData.FieldWidth, PhysicData.FieldHeight));
            _gameField = new GameField(_gameFieldTexture, _appleTexture, _boomTexture);
        }

        protected override void LoadContent()
        {
            this._spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            this._snakeHeadTexture = new Texture2D[4]
            {
                Content.Load<Texture2D>("SnakeHead"),
                Content.Load<Texture2D>("SnakeHeadDown"),
                Content.Load<Texture2D>("SnakeHeadToLeft"),
                Content.Load<Texture2D>("SnakeHeadToRight")
            };

            this._snakePartTexture = Content.Load<Texture2D>("SnakePart");
            this._appleTexture = Content.Load<Texture2D>("Apple");
            this._boomTexture = Content.Load<Texture2D>("Boom");
            this._gameFieldTexture = Content.Load<Texture2D>("FieldPart");
            this._gameDeadTexture = Content.Load<Texture2D>("Table");
            this._deadSnakeTexture = Content.Load<Texture2D>("DeadSnake");
            this._gameWinTexture = Content.Load<Texture2D>("WinTable");
            this._spaceButtonTexture = Content.Load<Texture2D>("SpaceButton");

            _mainFont = Content.Load<SpriteFont>("galleryFont");
            _scoreFont = Content.Load<SpriteFont>("ScoreFont");
            _startFont = Content.Load<SpriteFont>("StartFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _snake.Stop();
                _pauseTimeInSeconds += _stopwatch.ElapsedMilliseconds / 1000;
                _stopwatch.Restart();
                _stopwatch.Stop();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _snake.SlowStart();
            }


            // TODO: Add your update logic here

            var keys = Keyboard.GetState().GetPressedKeys();

            if (keys.Length > 0)
            {
                switch (keys[0])
                {
                    case Keys.W:
                    case Keys.Up: _direction = Direction.Up; break;

                    case Keys.S:
                    case Keys.Down: _direction = Direction.Down; break;

                    case Keys.A:
                    case Keys.Left: _direction = Direction.Left; break;

                    case Keys.D:
                    case Keys.Right: _direction = Direction.Right; break;
                }
            }


            if (_timer % 30 == 0 && _snake.State == SnakeState.Active)
            {
                _snake.Move(_direction);
                _gameField.Eat(_snake);
                _timer = 1;
            }
            else
            {
                ++_timer;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _gameField.Draw(_spriteBatch);
            _snake.Draw(_spriteBatch);

            _spriteBatch.DrawString(_scoreFont, $"Your scores: {_snake.Scores}/{PhysicData.MaxScores}\n", new Vector2(210, 10), Color.White);

            switch (_snake.State)
            {
                case SnakeState.Attention:
                    _stopwatch.Start();
                    if ((_stopwatch.ElapsedMilliseconds / 1001 + 1) % 5 == 0)
                    {
                        _snake.Start();
                    }
                    else
                    {
                        _spriteBatch.DrawString(_startFont, $"Ready !\n   {_stopwatch.ElapsedMilliseconds / 1000}", new Vector2(180, 180), Color.White);
                    }
                    break;

                case SnakeState.Dead:
                    _spriteBatch.Draw(_gameDeadTexture, new Vector2(0, 180), Color.White);
                    _spriteBatch.DrawString(_mainFont, $"Game is over !!!\n", new Vector2(100, 200), Color.Black);
                    break;

                case SnakeState.Win:
                    if (_stopwatch.IsRunning)
                    {
                        _stopwatch.Stop();
                    }
                    _spriteBatch.Draw(_gameWinTexture, new Vector2(0, 120), Color.White);
                    _spriteBatch.DrawString(_mainFont, $"   You Win !!!\nTime: {new TimeSpan(_stopwatch.ElapsedTicks + _pauseTimeInSeconds).ToString(@"hh\:mm\:ss")}\n", new Vector2(100, 160), Color.White);
                    break;
                case SnakeState.Pause:
                    _spriteBatch.Draw(_spaceButtonTexture, new Vector2(140, 140), Color.White);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


