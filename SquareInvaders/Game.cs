using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders
{
    static class Game
    {
        static Player player;
        static Window window;
        static float totalTime;
        static int score;
        static SpriteText points;

        static Game()
        {
            Color playerColor = new Color(0, 0, 255);
            Color alienColor = new Color(0, 255, 0);
            window = new Window(1000, 700, "Square Invaders", PixelFormat.RGB);
            GfxTools.Init(window);
            EnemyManager.Init(18, 3);
            BarrierManager.Init(new Vector2(65, 500));
            player = new Player(500, 630);
            points = new SpriteText(new Vector2(10, 10), "000000");
            totalTime = 0;
            score = 0;
        }

        public static void Play()
        {
            while (window.opened)
            {
                if (window.GetKey(KeyCode.Esc))
                    return;

                GfxTools.ClearScreen();

                player.KeyInput();

                EnemyManager.Update();
                player.Update();
                points.SetText(GetScore().ToString("D6"));

                if (EnemyManager.GetAlives() <= 0 || !player.isAlive)
                    break;

                player.Draw();
                EnemyManager.Draw();
                points.Draw();
                BarrierManager.Draw();

                window.Blit();
            }

            while (window.opened)
            {
                GfxTools.ClearScreen();

                player.Draw();
                EnemyManager.Draw();
                points.SetText(GetScore().ToString("D6"));
                points.Draw();
                BarrierManager.Draw();

                window.Blit();
            }
        }

        public static Player GetPlayer()
        {
            return player;
        }

        public static int GetScore()
        {
            return score - (int)(totalTime * 0.25);
        }

        public static void AddScore(int amount)
        {
            score += amount;
            points.SetText(score.ToString("D6"));
        }
    }
}
