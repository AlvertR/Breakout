using System.Numerics;

namespace Breakout
{
    public class Paddle
    {
        public Paddle() { }
        public Paddle(int width, int height, float speed, float posX, float posY) { 
            Width = width;
            Height = height;
            Speed = speed;
            Position = new Vector2(posX, posY);
        }
        public int Height { get; set; }
        public int Width { get; set; }
        public float Speed { get; set; }
        public Vector2 Position { get; set; }

        public void SetPositionX(float position)
        {
            this.Position = new Vector2(position, this.Position.Y);
        }

        public void SetPositionY(float position)
        {
            this.Position = new Vector2(this.Position.X, position);
        }

        //public void MoveRigth(KeyboardKey upKey, float deltaTime)
        //{
        //    if (Raylib.IsKeyDown(upKey) && this.Position.X > 0)
        //        this.SetPositionY(this.Position.X + (this.Speed * deltaTime));
        //}

        //public void MoveLeft(KeyboardKey downKey, float deltaTime, int heightWindow)
        //{
        //    if (Raylib.IsKeyDown(downKey) && this.Position.Y < heightWindow - this.Height - 1)
        //        this.SetPositionY(this.Position.Y + (this.Speed * deltaTime));
        //}
    }
}
