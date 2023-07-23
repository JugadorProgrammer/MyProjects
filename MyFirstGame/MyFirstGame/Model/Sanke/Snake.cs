using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Graphics;
using MyFirstGame.Model.Apple;
using MyFirstGame.Model.SnakeState;
using Vector2 = Microsoft.Xna.Framework.Vector2;


namespace MyFirstGame.Model.Sanke
{
    public class Snake
    {
        private Texture2D[] _head;
        private Texture2D _snakePart;
        private Texture2D _deadSnake;
        private Direction.Direction _previousDirection;
        public List<SnakePart> GameSnake { get; private set; }
        public SnakeState.SnakeState State { get; private set; }
        public int Scores { get { return GameSnake.Count - 1; } }

        public Snake(Texture2D[] head, Texture2D snakePart, Texture2D deadSnake, Vector2 positionVector)
        {
            this._deadSnake = deadSnake;
            this._previousDirection = Direction.Direction.Up;
            this.State = SnakeState.SnakeState.Attention;
            this._head = head;
            this.GameSnake = new List<SnakePart>() { new SnakePart(_head[0], positionVector) };
            this._snakePart = snakePart;
        }
        public void Start()
        {
            State = SnakeState.SnakeState.Active;
        }
        public void SlowStart()
        {
            State = SnakeState.SnakeState.Attention;
        }
        public void Stop()
        {
            State = SnakeState.SnakeState.Pause;
        }

        public void Move(Direction.Direction direction)//don`t move, а сам move
        {
            if (GameSnake.Count == 1 || CorrectDirectionCheck(direction))
            {
                _previousDirection = direction;
            }

            for (int i = GameSnake.Count - 1; i >= 1; --i)
            {
                GameSnake[i].SetPosition(GameSnake[i - 1].PositionVector);
            }
            GameSnake[0].Move(_previousDirection, _head[(int)_previousDirection]);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameSnake.Count; ++i)
            {
                GameSnake[i].Draw(spriteBatch);
            }
        }

        public void Eat()//логика поедания
        {
            Vector2 newCordinatesDelta = Physics.PhysicData.NewCordinatesDelta[(int)_previousDirection];
            Vector2 position = new Vector2(GameSnake.LastOrDefault().PositionVector.X + newCordinatesDelta.X, GameSnake.LastOrDefault().PositionVector.Y + newCordinatesDelta.Y);
            GameSnake.Add(new SnakePart(_snakePart, position));

            if (GameSnake.Count > Physics.PhysicData.MaxScores)
            {
                State = SnakeState.SnakeState.Win;
            }
        }

        public void Dead()//логика убиения
        {
            State = SnakeState.SnakeState.Dead;
            GameSnake[0].Texture = _deadSnake;
        }

        public void TellCheck()//логика самопоедания
        {
            for (int i = 1; i < GameSnake.Count; ++i)
            {
                if (ObjectModel.IsContact(GameSnake[0], GameSnake[i]))
                {
                    this.Dead();
                    break;
                }
            }
        }

        private bool CorrectDirectionCheck(Direction.Direction direction)
        {
            if (direction == Direction.Direction.Up && _previousDirection == Direction.Direction.Down
                || direction == Direction.Direction.Down && _previousDirection == Direction.Direction.Up
                || direction == Direction.Direction.Right && _previousDirection == Direction.Direction.Left
                || direction == Direction.Direction.Left && _previousDirection == Direction.Direction.Right)
            {
                return false;
            }
            return true;
        }
    }
}
