using FunnyBird.Models.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FunnyBird.Models
{
    public class SpriteModel
    {
        protected ProgramSettings _programSettings;
        protected float _width;
        protected float _height;
        public Texture2D Texture { get; protected set; }
        public Vector2 PositionVector { get; protected set; }

        public virtual void MoveOn(float x, float y)
        {
            x += this.PositionVector.X;
            y += this.PositionVector.Y;
            
            //if (x > _programSettings.WindowWidth - this._width)
            //{
            //    x = _programSettings.WindowWidth - this._width;
            //}
            //else if (x <= 0)
            //{
            //    x = 0;
            //}

            this.SetPositionVector(new Vector2(x, y));
        }

        public void SetPositionVector(Vector2 positionVector)
        {
            this.PositionVector = positionVector;
        }

        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,PositionVector,Color.White);
        }

        public SpriteModel(Texture2D texture, float width, float height)
        {
            this._width = width;
            this._height = height;
            this.Texture = texture;
            this.PositionVector = Vector2.Zero;
            this._programSettings = ProgramSettings.GetInit();
        }
        
    }
}
