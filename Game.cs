using Raylib_cs;
using System.Numerics;

namespace Breakout
{
    public class Game
    {
        public Game() { }

        public Game(string name, int fps, int widthWindow, int heightWindow)
        {
            HeightWindow = heightWindow;
            WidthWindow = widthWindow;
            Name = name;
            FPS = fps;
        }
        public int HeightWindow { get; set; }
        public int WidthWindow { get; set; }
        public string Name { get; set; }
        public int FPS { get; set; }
        public float DeltaTime { get; set; } = 0;
        public GameStatus GameStatus { get; set; } = GameStatus.Start;
        public int Score { get; set; } = 0;
        public Paddle Paddle {get; set;}
        public Ball Ball {get; set;}
        public List<Brick> BrickList {get; set;} = new List<Brick>();
        public int DefaultMagnitudeVel { get; set; }
        public int MaxMagnitudeVel { get; set; }
        //public int MinMagnitudeVel { get; set; }
        public int BaseSpeed { get; set; }
        public int Columns { get; set; } = 10;
        public int Rows { get; set; } = 10;

        public void LoadGame()
        {
            Raylib.InitWindow(this.WidthWindow, this.HeightWindow, this.Name);
            //Raylib.InitAudioDevice();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string fulPathIcon = Path.Combine(basePath, "Resources", "break-icon.png");
            Image icon = Raylib.LoadImage(fulPathIcon);
            Raylib.ImageFormat(ref icon, PixelFormat.UncompressedR8G8B8A8);
            Raylib.SetWindowIcon(icon);
            Raylib.UnloadImage(icon);

            //string soundPath = Path.Combine(basePath, "Resource", "sound.mp3");
            //Sound hitBallSound = Raylib.LoadSound(soundPath);
            Raylib.SetTargetFPS(this.FPS);

            DefaultMagnitudeVel = 160;
            MaxMagnitudeVel = 200;
            //MinMagnitudeVel = 140;
            BaseSpeed = 226;
            this.Paddle = new Paddle(100,20,225,370,550, 5);
            this.Ball = new Ball(20,0,0, DefaultMagnitudeVel);
            this.SetBrickList();

            while (!Raylib.WindowShouldClose())
            {
                this.DeltaTime = Raylib.GetFrameTime();
                HandleInput();
                Update();
                Draw();
            }

            //Raylib.UnloadSound(hitBallSound);
            //Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        }

        public void HandleInput()
        {
            float movement = 0;
            switch (GameStatus)
            {
                case GameStatus.Start:
                    if (Raylib.IsKeyPressed(KeyboardKey.S))
                    {
                        GameStatus = GameStatus.Playing;
                        this.ResetBall();
                    }
                    break;
                case GameStatus.Playing:
                    if (Raylib.IsKeyDown(KeyboardKey.Left) && Paddle.Position.X >=0)
                        movement -= this.Paddle.Speed * DeltaTime;
                    if (Raylib.IsKeyDown(KeyboardKey.Right) && (Paddle.Position.X + Paddle.Width) <= WidthWindow)
                        movement += this.Paddle.Speed * DeltaTime;
                    if (Raylib.IsKeyDown(KeyboardKey.Space))
                        Ball.SetRunnig();
                    float newX = Paddle.Position.X + movement;
                    newX = Math.Clamp(newX, 0, WidthWindow -Paddle.Width);
                    Paddle.SetPositionX(newX);
                    if (Raylib.IsKeyDown(KeyboardKey.P))
                        GameStatus = GameStatus.Paused;
                    break;
                case GameStatus.Paused:
                    if (Raylib.IsKeyDown(KeyboardKey.C))
                        GameStatus = GameStatus.Playing;
                        break;
                case GameStatus.GameOver:
                case GameStatus.End:
                    break;
                default: 
                    break;
            }
        }

        public void Update()
        {
            switch (GameStatus)
            {
                case GameStatus.Playing:
                    this.MoveBall();
                    this.CheckCollision();
                    if (this.IsLavelComplete())
                        GameStatus = GameStatus.End;
                    break;
                default:
                    break;
            }
        }

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            switch (GameStatus)
            {
                case GameStatus.Start:
                    Raylib.DrawText("Breakout", WidthWindow/3, HeightWindow/3, 24, Color.White);
                    Raylib.DrawText("Presiona S para iniciar", WidthWindow/4, HeightWindow/2, 20, Color.White);
                    break;
                case GameStatus.Playing:
                    Raylib.DrawText("Puntos: " + Score.ToString(), 10, 5, 14, Color.White);
                    Raylib.DrawText("Vidas: " + Paddle.Lifes.ToString(), WidthWindow-80, 5, 14, Color.White);
                    Raylib.DrawCircleV(Ball.Position, Ball.Radius, Color.White);
                    Raylib.DrawRectangleV(Paddle.Position, new Vector2(Paddle.Width, Paddle.Height), Color.White);
                    foreach(var brick in BrickList)
                    {
                        if (brick.Status == BrickStatus.Active)
                        {
                            Raylib.DrawRectangleV(brick.Position, new Vector2(brick.Width, brick.Height), brick.Color);
                            Raylib.DrawRectangleLines((int)brick.Position.X, (int)brick.Position.Y, (int)brick.Width, (int)brick.Height, Color.White);
                        }
                    }
                    break;
                case GameStatus.Paused:
                    Raylib.DrawText("Presiona C para continuar", WidthWindow / 4, HeightWindow / 2, 20, Color.White);
                    break;
                case GameStatus.GameOver:
                    Raylib.DrawText("Fin del juego", 10, 5, 34, Color.White);
                    Raylib.DrawText("Puntos: " + Score.ToString(), 10, 75, 14, Color.White);
                    break;
                case GameStatus.End:
                    Raylib.DrawText("Nivel completado", 10, 5, 34, Color.White);
                    break;
                default:
                    break;
            }
            Raylib.EndDrawing();
        }
    
        public void ResetBall()
        {
            Ball.SetPositionX(Paddle.Position.X + Paddle.Width / 2);
            Ball.SetPositionY(Paddle.Position.Y - Ball.Radius);
            Vector2 normalize = Vector2.Normalize(new Vector2(0 , Ball.DefaultMagnitudeVel * -1));
            Ball.Velocity = normalize * BaseSpeed;
        }

        public void ChangeAngle()
        {
            float paddleCenter = Paddle.Position.X + (Paddle.Width / 2);
            float impactPoint = (Ball.Position.X - paddleCenter) / (Paddle.Width / 2);
            impactPoint = Math.Clamp(impactPoint, -1, 1);

            float velocityX = impactPoint * MaxMagnitudeVel;
            float velocityY = -Math.Abs(Ball.Velocity.Y);

            Vector2 direction = Vector2.Normalize(new Vector2(velocityX, velocityY));
            Ball.Velocity = direction * BaseSpeed;
        }

        public void MoveBall()
        {
            if (Ball.Status == BallStatus.Running)
            {
                Ball.Position += Ball.Velocity * DeltaTime;
                if (Ball.Position.Y <= 1 + Ball.Radius)
                {
                    Ball.SetVelocityY(Ball.Velocity.Y * -1);
                }
                if (Ball.Position.Y + Ball.Radius >= Paddle.Position.Y)
                {
                    if (Ball.Position.X + Ball.Radius >= Paddle.Position.X
                        && Ball.Position.X - Ball.Radius <= Paddle.Position.X + Paddle.Width)
                    {
                        Ball.SetPositionY(Paddle.Position.Y - Ball.Radius);
                        this.ChangeAngle();
                    }
                }
                if (Ball.Position.Y >= HeightWindow - Ball.Radius - 1)
                {
                    Ball.SetStop();
                    Paddle.Lifes--;
                    if(this.IsGameOver())
                        GameStatus = GameStatus.GameOver;
                    else
                        this.ResetBall();
                }

                if (Ball.Position.X <= 1 + Ball.Radius || Ball.Position.X >= WidthWindow - Ball.Radius - 1)
                {
                    Ball.SetVelocityX(Ball.Velocity.X * -1);
                }
            }
            else
            {
                //Ball.SetPositionX(Paddle.Position.X+Paddle.Width/2);
                this.ResetBall();
            }
        }

        public bool IsGameOver()
        {
            if(Paddle.Lifes <= 0)
                return true;
            return false;
        }
        public bool IsLavelComplete()
        {
            var activeBricks = BrickList.Count(b => b.Status == BrickStatus.Active);
            if (activeBricks == 0)
                return true;
            return false;
        }

        public void SetBrickList()
        {
            float brickHeight = (Paddle.Position.Y - 200)/Rows;
            float brickWidth = WidthWindow/Columns;
            for(int c = 0; c < Columns; c++)
                for(int r = 0; r < Rows; r++)
                {
                    float posX = c * brickWidth;
                    float posY = (r * brickHeight) + 30;
                    Brick newBrick = new Brick(brickHeight, brickWidth, posX,posY);
                    newBrick.Color = this.GetBrickColor(r);
                    this.BrickList.Add(newBrick);
                }
        }

        public Color GetBrickColor(int row)
        {
            switch (row) { 
                case 0:
                    return Color.FromHSV(8, 99 , 40);
                case 1:
                    return Color.FromHSV(17, 90, 59);
                case 2:
                    return Color.FromHSV(42, 97, 54);
                case 3:
                    return Color.FromHSV(56, 100, 51);
                case 4:
                    return Color.FromHSV(67, 100, 45);
                case 5:
                    return Color.FromHSV(79, 100, 41);
                case 6:
                    return Color.FromHSV(96, 100, 37);
                case 7:
                    return Color.FromHSV(120, 100, 33);
                case 8:
                    return Color.FromHSV(158, 99, 36);
                case 9:
                    return Color.FromHSV(181, 100, 40);
                default:
                    return Color.White;
            }
        }
    
        public void CheckCollision()
        {
            foreach (var brick in this.BrickList)
            {
                if (brick.Status != BrickStatus.Active)
                    continue;

                Rectangle checkRectangle = new Rectangle(brick.Position.X, brick.Position.Y, brick.Width, brick.Height);
                bool isCollision = Raylib.CheckCollisionCircleRec(Ball.Position, Ball.Radius, checkRectangle);
                if (isCollision)
                {
                    float closeX = Math.Clamp(Ball.Position.X, checkRectangle.X, checkRectangle.X + checkRectangle.Width);
                    float closeY = Math.Clamp(Ball.Position.Y, checkRectangle.Y, checkRectangle.Y + checkRectangle.Height);

                    float distX = Ball.Position.X - closeX;
                    float distY = Ball.Position.Y - closeY;

                    if (Math.Abs(distX) > Math.Abs(distY))
                        Ball.SetVelocityX(Ball.Velocity.X * -1);
                    else
                        Ball.SetVelocityY(Ball.Velocity.Y * -1);

                    brick.Status = BrickStatus.Dead;
                    this.Score++;
                    break;
                }
            }
        }
    }
}
