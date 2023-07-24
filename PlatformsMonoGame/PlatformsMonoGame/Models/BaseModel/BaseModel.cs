using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformsMonoGame.PhysicalSettings;

namespace PlatformsMonoGame.Models.BaseModel
{
    public class BaseModel
    {
        protected float _width;
        protected float _height;
        public Texture2D Texture { get; private set; }
        public Vector2 PositionVector { get; private set; }

        public BaseModel(Texture2D texture, Vector2 positionVector, float width, float height)
        {
            this._width = width;
            this._height = height;
            this.Texture = texture;
            this.PositionVector = positionVector;
        }

        public virtual void MoveOn(float x, float y)
        {
            x += this.PositionVector.X;
            y += this.PositionVector.Y;

            if (x > Settings.WindowWidth - this._width)
            {
                x = Settings.WindowWidth - this._width;
            }
            else if (x <= 0)
            {
                x = 0;
            }

            

            this.PositionVector = new Vector2(x, y);
        }

        public void SetPositionVector(Vector2 positionVector)
        {
            this.PositionVector = positionVector;
        }
    }
}
