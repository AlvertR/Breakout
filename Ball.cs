using System.Numerics;

namespace Breakout
{
    public class Ball
    {
        public Ball() { }
        public Ball(int radius, float posX, float posY, int baseMag) {
            Radius = radius;
            DefaultMagnitudeVel = baseMag;
            Position = new Vector2(posX, posY);
            Velocity = new Vector2(baseMag, baseMag);
        }
        public int Radius { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int DefaultMagnitudeVel { get; set; }
        public BallStatsu Statsus { get; set; } = BallStatsu.Stop;

        public void SetVelocityY(float velocity)
        {
            this.Velocity = new Vector2(this.Velocity.X, velocity);
        }

        public void SetVelocityX(float velocity)
        {
            this.Velocity = new Vector2(velocity, this.Velocity.Y);
        }

        public void SetPositionY(float position)
        {
            this.Position = new Vector2(this.Position.X, position);
        }

        public void SetPositionX(float position)
        {
            this.Position = new Vector2(position, this.Position.Y);
        }

        public void SetRunnig()
            => this.Statsus = BallStatsu.Running;

        public void SetStop()
            => this.Statsus = BallStatsu.Stop;
    }
}
