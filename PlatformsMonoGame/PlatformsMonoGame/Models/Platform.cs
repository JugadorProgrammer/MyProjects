using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformsMonoGame.Models
{
    public class Platform : BaseModel.BaseModel
    {
        public Platform(Texture2D texture, Vector2 positionVector, float width, float height) : base(texture, positionVector, width, height)
        {
        }
    }
}
