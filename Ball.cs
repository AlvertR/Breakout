using System.Numerics;

namespace Breakout
{
    public class Ball
    {
        public Ball() { }
        public Ball(int radius, float posX, float posY, int baseMag, int maxMagnitude, int baseSpeed) {
            Radius = radius;
            DefaultMagnitudeVel = baseMag;
            Position = new Vector2(posX, posY);
            Velocity = new Vector2(baseMag, baseMag);
            MaxMagnitudeVel = maxMagnitude;
            BaseSpeed = baseSpeed;
        }
        public int Radius { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int DefaultMagnitudeVel { get; set; }
        public int MaxMagnitudeVel { get; set; }
        public int BaseSpeed { get; set; }
        public BallStatus Status { get; set; } = BallStatus.Stop;

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

        public void SetRunning()
            => this.Status = BallStatus.Running;

        public void SetStop()
            => this.Status = BallStatus.Stop;
    }
}
