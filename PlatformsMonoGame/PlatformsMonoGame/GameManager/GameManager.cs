using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformsMonoGame.Models;
using PlatformsMonoGame.Models.BaseModel;
using PlatformsMonoGame.Models.Direction;
using PlatformsMonoGame.PhysicalSettings;

namespace PlatformsMonoGame.GameManager
{
    public class GameManager
    {
        private BaseModel _gameField;
        private Platform _platform1;
        private Platform _platform2;
        private Ball _ball;

        private SpriteFont _mainFont;
        private SpriteFont _scoreFont;

        private Direction _platform1Direction;
        private Direction _platform2Direction;

        private int _scores;
        private ulong _timer;
        private bool _isLose;

        private GameManager(Texture2D platformTexture, Texture2D ballTexture, Texture2D fieldTexture, SpriteFont mainFont,SpriteFont scoreFont)
        {
            this._timer = 1;
            this._scores = 0;
            this._isLose = false;
            this._mainFont = mainFont;
            this._scoreFont = scoreFont;
            this._platform1Direction = this._platform2Direction = Direction.Left;
            this._gameField = new BaseModel(fieldTexture, Vector2.Zero, Settings.WindowWidth, Settings.WindowHeight);
            this._platform1 = new Platform(platformTexture, new Vector2((Settings.WindowWidth - Settings.PlatformWidth) / 2, 0), Settings.PlatformWidth, Settings.PlatformHeight);
            this._platform2 = new Platform(platformTexture, new Vector2((Settings.WindowWidth - Settings.PlatformWidth) / 2, Settings.WindowHeight - Settings.PlatformHeight), Settings.PlatformWidth, Settings.PlatformHeight);
            this._ball = new Ball(ballTexture, new Vector2((Settings.WindowWidth - Settings.BallRadius) / 2, (Settings.WindowHeight - Settings.BallRadius) / 2), Settings.BallRadius * 2, Settings.BallRadius * 2);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_gameField.Texture, _gameField.PositionVector, Color.White);
            spriteBatch.Draw(_platform1.Texture, _platform1.PositionVector, Color.White);
            spriteBatch.Draw(_platform2.Texture, _platform2.PositionVector, Color.White);

            spriteBatch.DrawString(_scoreFont, $"Scores : {this._scores}", new Vector2(Settings.WindowWidth - Settings.PlatformWidth, 10), Color.Black);

            if (this._isLose)
            {
                spriteBatch.DrawString(_mainFont, "Game Over !!!", new Vector2(Settings.WindowWidth / 5, Settings.WindowHeight / 2), Color.Red);
            }
            else
            {
                spriteBatch.Draw(_ball.Texture, _ball.PositionVector, Color.White);
            }
        }
        public void TimerTick(Direction platform1Direction, Direction platform2Direction)
        {
            if (++this._timer % Settings.TimerInterval == 0)
            {
                Move(platform1Direction == Direction.None ? this._platform1Direction : platform1Direction,
                    platform2Direction == Direction.None ? this._platform2Direction : platform2Direction);
                if (!this._isLose)
                {
                    CollisionCheck();
                }

            }
            if (this._timer == ulong.MaxValue)
            {
                this._timer = 1;
            }
        }
        private void PlatformMove(BaseModel platform, ref Direction platformDirection)
        {
            float x;
            switch (platformDirection)
            {
                case Direction.Left: x = -10; break;
                case Direction.Righ: x = 10; break;
                default: x = 0; break;
            }

            platform.MoveOn(x, 0);

            if (platform.PositionVector.X == 0)
            {
                platformDirection = Direction.Righ;
            }
            else if (platform.PositionVector.X == Settings.WindowWidth - Settings.PlatformWidth)
            {
                platformDirection = Direction.Left;
            }
        }
        private void Move(Direction platform1Direction, Direction platform2Direction)
        {
            this._ball.Move();

            this._platform1Direction = platform1Direction;
            this._platform2Direction = platform2Direction;

            PlatformMove(_platform1, ref this._platform1Direction);
            PlatformMove(_platform2, ref this._platform2Direction);
        }
        private void CollisionCheck()
        {
            this._isLose = this._ball.LossCheck();

            this._ball.SideWallCheck();

            Vector2 ballCenterVector = this._ball.CenterCoordinatesVector();

            if (ballCenterVector.X >= this._platform1.PositionVector.X
                && (ballCenterVector.X <= this._platform1.PositionVector.X + Settings.PlatformWidth)
                && ballCenterVector.Y - Settings.BallRadius <= Settings.PlatformHeight && !this._ball.IsDown)
            {
                ++this._scores;
                this._ball.ChangeDirection(this._platform1Direction);
            }
            else if (ballCenterVector.X >= this._platform2.PositionVector.X
                && (ballCenterVector.X <= this._platform2.PositionVector.X + Settings.PlatformWidth)
                && (ballCenterVector.Y + Settings.BallRadius >= this._platform2.PositionVector.Y)
                && this._ball.IsDown)

            {
                ++this._scores;
                this._ball.ChangeDirection(this._platform2Direction);
            }

        }


        //SingleTone
        private static GameManager _gameManager;
        public static GameManager GetInstance(Texture2D platformTexture, Texture2D ballTexture, Texture2D fieldTexture, SpriteFont mainFont, SpriteFont scoreFont)
        {
            if (_gameManager == null)
            {
                _gameManager = new GameManager(platformTexture, ballTexture, fieldTexture, mainFont, scoreFont);
            }
            return _gameManager;
        }
    }
}
