using Raylib_cs;
using System;
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
        public float Score { get; set; } = 0f;
        public Paddle Paddle {get; set;}
        public Ball Ball {get; set;}

        public void LoadGame()
        {
            Raylib.InitWindow(this.WidthWindow, this.HeightWindow, this.Name);
            //Raylib.InitAudioDevice();
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;

            //string fulPathIcon = Path.Combine(basePath, "Resources", "snake-icon-2.png");
            //Image icon = Raylib.LoadImage(fulPathIcon);
            //Raylib.ImageFormat(ref icon, PixelFormat.UncompressedR8G8B8A8);
            //Raylib.SetWindowIcon(icon);
            //Raylib.UnloadImage(icon);

            //string soundPath = Path.Combine(basePath, "Resource", "sound.mp3");
            //Sound hitBallSound = Raylib.LoadSound(soundPath);
            Raylib.SetTargetFPS(this.FPS);

            this.Paddle = new Paddle(40,10,220,400,450);
            this.Ball = new Ball(20, 350,300, 160);

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
            switch (GameStatus)
            {
                case GameStatus.Start:
                    if (Raylib.IsKeyPressed(KeyboardKey.S))
                        GameStatus = GameStatus.Playing;
                    break;
                case GameStatus.Playing:
                    break;
                case GameStatus.Paused:
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
                    Raylib.DrawCircleV(Ball.Position, Ball.Radius, Color.White);
                    Raylib.DrawRectangleV(Paddle.Position, new Vector2(Paddle.Width, Paddle.Height), Color.White);
                    break;
                case GameStatus.Paused:
                    break;
                case GameStatus.GameOver:
                    break;
                case GameStatus.End:
                    break;
                default:
                    break;
            }
            Raylib.EndDrawing();
        }
    }
}
