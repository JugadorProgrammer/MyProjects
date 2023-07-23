using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using MyFirstGame.Model.Physics;

namespace MyFirstGame.Model
{
    public class ObjectModel
    {
        public Vector2 PositionVector { get; protected set; }
        public Texture2D Texture { get; set; }

        public ObjectModel(Texture2D texture, Vector2 positionVector)
        {
            Texture = texture;
            PositionVector = positionVector;
        }
        public void MoveOn(float x, float y)
        {
            x += PositionVector.X;
            y += PositionVector.Y;

            if (x < 0)
            {
                x = PhysicData.FieldPartCount * PhysicData.FieldWidth;
            }
            else if (x > 450)
            {
                x = 0;
            }

            if (y < 0)
            {
                y = PhysicData.FieldPartCount * PhysicData.FieldHeight;
            }
            else if (y > 450)
            {
                y = 0;
            }

            SetPosition(new Vector2(x,y));
        }
        public void SetPosition(Vector2 positionVector)
        {
            PositionVector = positionVector;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, PositionVector, Color.White);
        }

        public static bool IsContact(ObjectModel objectModel1, ObjectModel objectModel2)
        {
            if (Math.Abs(objectModel1.PositionVector.X - objectModel2.PositionVector.X) < PhysicData.RadiusEat
                && Math.Abs(objectModel1.PositionVector.Y - objectModel2.PositionVector.Y) < PhysicData.RadiusEat)
            {
                return true;
            }
            return false;
        }
    }
}
