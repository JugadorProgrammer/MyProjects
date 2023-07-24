using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformsMonoGame.PhysicalSettings;


namespace PlatformsMonoGame.Models
{
    public class Ball : BaseModel.BaseModel
    {
        private float _deltaX;
        private float _deltaY;
        public bool IsDown { get; private set; }
        public Ball(Texture2D texture, Vector2 positionVector, float width, float height) : base(texture, positionVector, width, height)
        {
            this._deltaX = 0;
            this._deltaY = 5;
            this.IsDown = true;
        }

        public void Move()
        {
            this.MoveOn(this._deltaX, this._deltaY);

        }

        public void ChangeDirection(Direction.Direction platformDirection)
        {
            this._deltaY = -this._deltaY;
            this.IsDown = !this.IsDown;
            
            switch (platformDirection)
            {
                case Direction.Direction.Left: this._deltaX += (this._deltaX <= 0 ? -Settings.Speed  : Settings.Speed); break;
                case Direction.Direction.Righ: this._deltaX += (this._deltaX >= 0 ? Settings.Speed : -Settings.Speed); break;
            }
        }
        public bool LossCheck()
        {
            if (this.PositionVector.Y >= Settings.WindowHeight || this.PositionVector.Y <= 0)
            {
                return true;
            }
            return false;
        }

        public void SideWallCheck()
        {
            if (this.PositionVector.X <= 0 || this.PositionVector.X >= Settings.WindowWidth - this._width)
            {
                this._deltaX = -this._deltaX;
            }
        }

        public Vector2 CenterCoordinatesVector()
        {
            return new Vector2(this.PositionVector.X + this._width / 2, this.PositionVector.Y + this._height / 2);
        }
    }
}
