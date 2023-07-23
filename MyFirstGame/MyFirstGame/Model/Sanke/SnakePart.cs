using Microsoft.Xna.Framework;
using MyFirstGame.Model.Physics;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using System;

namespace MyFirstGame.Model.Sanke
{
    public class SnakePart : ObjectModel
    {

        public SnakePart(Texture2D texture, Vector2 positionVector) : base(texture, positionVector)
        {
            
        }

        public void Move(Direction.Direction direction, Texture2D newTexture)
        {
            Vector2 YXVector = PhysicData.NewCordinatesDelta[(int)direction];
            this.Texture = newTexture;
            base.MoveOn(YXVector.X,YXVector.Y);
        }
    }
}
