using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame.Model.Apple
{
    public class Apple : ObjectModel
    {
        public bool IsBoom { get; private set; }
        public Apple(Texture2D texture, Vector2 positionVector, bool IsBoom) : base(texture, positionVector)
        {
            this.IsBoom = IsBoom;
        }
    }
}
