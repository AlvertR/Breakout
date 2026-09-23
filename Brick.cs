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

        public void SetBrickColor(int row)
        {
            switch (row)
            {
                case 0:
                    this.Color = Color.FromHSV(8, 0.99f, 0.40f);
                    break;
                case 1:
                    this.Color = Color.FromHSV(17, 0.90f, 0.59f);
                    break;
                case 2:
                    this.Color = Color.FromHSV(42, 0.97f, 0.54f);
                    break;
                case 3:
                    this.Color = Color.FromHSV(56, 1.00f, 0.51f);
                    break;
                case 4:
                    this.Color = Color.FromHSV(67, 1.00f, 0.45f);
                    break;
                case 5:
                    this.Color = Color.FromHSV(79, 1.00f, 0.41f);
                    break;
                case 6:
                    this.Color = Color.FromHSV(96, 1.00f, 0.37f);
                    break;
                case 7:
                    this.Color = Color.FromHSV(120, 1.00f, 0.33f);
                    break;
                case 8:
                    this.Color = Color.FromHSV(158, 0.99f, 0.36f); 
                    break;
                case 9:
                    this.Color = Color.FromHSV(181, 1.00f, 0.40f);
                    break;
                default:
                    this.Color = Color.White;
                    break;
            }
        }
    }
}
