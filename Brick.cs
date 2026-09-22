using Raylib_cs;
using System.Numerics;

namespace Breakout
{
    public class Brick
    {
        public Brick() { }
        public Brick(float height, float width, float posX, float posY) { 
            Height = height;
            Width = width;
            Position = new Vector2(posX, posY);
        }
        public float Height { get; set; }
        public float Width { get; set; }
        public Vector2 Position { get; set; }
        public BrickStatus Status { get; set; } = BrickStatus.Active;
        public Color Color { get; set; }
    }
}
