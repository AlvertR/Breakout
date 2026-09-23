using System.Numerics;

namespace Breakout
{
    public class Paddle
    {
        public Paddle() { }
        public Paddle(int width, int height, float speed, float posX, float posY, int lifeNumber) { 
            Width = width;
            Height = height;
            Speed = speed;
            Position = new Vector2(posX, posY);
            Lifes = lifeNumber;
        }
        public int Height { get; set; }
        public int Width { get; set; }
        public float Speed { get; set; }
        public Vector2 Position { get; set; }
        public int Lifes { get; set; }

        public void SetPositionX(float position)
        {
            this.Position = new Vector2(position, this.Position.Y);
        }

        public void SetPositionY(float position)
        {
            this.Position = new Vector2(this.Position.X, position);
        }
    }
}
