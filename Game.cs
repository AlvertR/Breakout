using System;
using Raylib_cs;

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

            while (!Raylib.WindowShouldClose())
            {
                this.DeltaTime = Raylib.GetFrameTime();
                //HandleInput();
                //Update();
                //Draw();
            }

            //Raylib.UnloadSound(hitBallSound);
            //Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        }
    }
}
