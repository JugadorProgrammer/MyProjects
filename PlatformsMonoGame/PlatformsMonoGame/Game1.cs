using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformsMonoGame.PhysicalSettings;
using PlatformsMonoGame.Models.Direction;
using System.Linq;

namespace PlatformsMonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _gameFieldTexture;
        private Texture2D _platformTexture;
        private Texture2D _ballTexture;

        private SpriteFont _mainFont;
        private SpriteFont _scoreFont;

        private GameManager.GameManager _gameManager;

        private Keys[] _controlKeys1;
        private Keys[] _controlKeys2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Inovation game";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = Settings.WindowWidth;
            _graphics.PreferredBackBufferHeight = Settings.WindowHeight;
            _graphics.ApplyChanges();

            base.Initialize();

            _gameManager = GameManager.GameManager.GetInstance(_platformTexture, _ballTexture, _gameFieldTexture, _mainFont, _scoreFont);
            _controlKeys1 = new Keys[] { Keys.A, Keys.D };
            _controlKeys2 = new Keys[] { Keys.Right, Keys.Left };
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _ballTexture = Content.Load<Texture2D>("Ball");
            _platformTexture = Content.Load<Texture2D>("Platform");
            _gameFieldTexture = Content.Load<Texture2D>("GameField");

            _mainFont = Content.Load<SpriteFont>("mainFont");
            _scoreFont = Content.Load<SpriteFont>("scoreFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            Direction platform1Direction = Direction.None, platform2Direction = Direction.None;

            Keys[] keys1 = Keyboard.GetState().GetPressedKeys().Where(key => _controlKeys1.Contains(key)).ToArray(),
            keys2 = Keyboard.GetState().GetPressedKeys().Where(key => _controlKeys2.Contains(key)).ToArray();

            if (keys1.Length > 0)
            {
                switch (keys1[0])
                {
                    case Keys.A: platform1Direction = Direction.Left;break;
                    case Keys.D: platform1Direction = Direction.Righ;break;
                }
            }

            if (keys2.Length > 0)
            {
                switch (keys2[0])
                {
                    case Keys.Left: platform2Direction = Direction.Left; break;
                    case Keys.Right: platform2Direction = Direction.Righ; break;
                }
            }

            _gameManager.TimerTick(platform1Direction, platform2Direction);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _gameManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}