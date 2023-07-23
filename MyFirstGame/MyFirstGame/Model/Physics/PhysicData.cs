using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyFirstGame.Model.Physics
{
    public static class PhysicData
    {
        public static int MaxScores { get; } = 35;
        public static int MaxBooms { get; } = 5;
        public static int NewBooms { get; } = 7;
        public static float RadiusEat { get; } = 0.1F;
        public static float StepLength { get; } = 50F;
        public static int FieldWidth { get; } = 50;
        public static int FieldHeight { get; } = 50;
        public static int ScoreTanbleHeight { get; } = 50;
        public static int FieldPartCount { get; } = 10;
        public static Vector2[] NewCordinatesDelta { get; } = new Vector2[4]
        {
            new Vector2(0F,-50F),// Up
            new Vector2(0F,50F),//Down
            new Vector2(-50F,0F),//Left
            new Vector2(50F,0F)//Right
        }; 
    }
}
