using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyFirstGame.Model.Sanke;
using MyFirstGame.Model.Physics;
using MyFirstGame.Model.Apple;


namespace MyFirstGame.Model.Field
{
    public class GameField
    {
        public List<List<ObjectModel>> Field { get; private set; }

        private Random _random;
        private Apple.Apple _apple;
        private Texture2D _boomTexture;
        private List<Apple.Apple> _booms;
        private List<Vector2> _freeFieldList;

        public GameField(Texture2D texture, Texture2D AppleTexture, Texture2D BoomTexture)
        {
            this._random = new Random();
            this._boomTexture = BoomTexture;
            this._freeFieldList = new List<Vector2>();

            this.Field = new List<List<ObjectModel>>();

            for (int i = 0; i < PhysicData.FieldPartCount; ++i)
            {
                Field.Add(new List<ObjectModel>());

                for (int j = 0; j < PhysicData.FieldPartCount; ++j)
                {
                    Field[i].Add(new ObjectModel(texture, new Vector2(i * PhysicData.FieldWidth, j * PhysicData.FieldWidth)));

                    _freeFieldList.Add(new Vector2(i * PhysicData.FieldWidth, j * PhysicData.FieldHeight));
                }
            }

            this._booms = new List<Apple.Apple>();
            CreateNewBoom();
            

            this._apple = new Apple.Apple(AppleTexture,
                new Vector2(_random.Next(0, PhysicData.FieldPartCount) * PhysicData.FieldWidth, _random.Next(0, PhysicData.FieldPartCount) * PhysicData.FieldHeight), false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < PhysicData.FieldPartCount; ++i)
            {
                for (int j = 0; j < PhysicData.FieldPartCount; ++j)
                {
                    spriteBatch.Draw(Field[i][j].Texture, Field[i][j].PositionVector, Color.White);
                }
            }

            for (int i = 0; i < _booms.Count; ++i)
            {
                spriteBatch.Draw(_booms[i].Texture, _booms[i].PositionVector, Color.White);
            }

            spriteBatch.Draw(_apple.Texture, _apple.PositionVector, Color.White);

        }

        public void Eat(Snake snake)
        {
            if (ObjectModel.IsContact(snake.GameSnake[0], _apple))
            {
                snake.Eat();
                MoveApple();
                if (snake.Scores % PhysicData.NewBooms == 0)
                {
                    CreateNewBoom();
                }
            }
            else
            {
                for (int i = 0; i < _booms.Count; ++i)
                {
                    if (ObjectModel.IsContact(snake.GameSnake[0], _booms[i]))
                    {
                        snake.Dead();
                        break;
                    }
                }
                snake.TellCheck();
            }
        }
        private void MoveApple()
        {
            _freeFieldList.Add(_apple.PositionVector);
            Vector2 position = NewPositionVector();

            _apple.SetPosition(position);
        }
        private void CreateNewBoom() 
        {
            if (this._booms.Count < PhysicData.MaxBooms)
            {
                Vector2 position = NewPositionVector();
                _booms.Add(new Apple.Apple(this._boomTexture, position, true));
            }
            
        }
        private Vector2 NewPositionVector() 
        {
            int randIndex = _random.Next(0, _freeFieldList.Count);
            Vector2 positionVector = _freeFieldList[randIndex];
            _freeFieldList.RemoveAt(randIndex);
            return positionVector;
        }
    }
}
