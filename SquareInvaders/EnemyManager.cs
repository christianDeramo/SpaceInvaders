using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders
{
    static class EnemyManager
    {
        static Alien[] alien;
        static AlienBullet[] bullet;
        static int numAliens;
        static int aliensPerRow;
        static int numRows;
        static int numAlives;

        public static void Init(int Aliens, int Rows)
        {
            numAliens = Aliens;
            numRows = Rows;
            aliensPerRow = numAliens / numRows;
            numAlives = numAliens;

            alien = new Alien[numAliens];
            Color white = new Color(255, 255, 255);

            int alienWidth = 30;
            int alienHeight = 20;
            int startX = 40;
            int startY = 40;
            int dist = 15;

            for (int i = 0; i < alien.Length; i++)
            {
                if (i != 0 && i % aliensPerRow == 0)
                    startY += alienHeight + dist;

                int alienX = startX + ((i % aliensPerRow) * (dist + alienWidth));

                alien[i] = new Alien(new Vector2(alienX, startY), white, alienWidth, alienWidth);
                if (i > numAliens - aliensPerRow)
                    alien[i].CanShoot = true;
                
            }

            bullet = new AlienBullet[aliensPerRow];
            for (int i = 0; i < bullet.Length; i++)
            {
                bullet[i] = new AlienBullet();
            }
        }

        public static void Update()
        {
            float overflowX = 0;
            bool endReached = false;

            for (int i = 0; i < alien.Length; i++)
            {
                if (alien[i].IsVisible)
                {
                    if (alien[i].Update(ref overflowX))
                        endReached = true;
                }
            }

            if (endReached)
            {
                for (int i = 0; i < alien.Length; i++)
                {
                    alien[i].TraslateSprite(new Vector2(-overflowX, 50));
                    alien[i].Traslate(new Vector2(-overflowX, 50));
                    alien[i].Velocity.X = alien[i].Velocity.X * -1.15f;
                }
            }

            Player player = Game.GetPlayer();

            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].IsActive)
                {
                    bullet[i].Update();

                    /*if (BarrierManager.Collision(bullet[i]))
                    {
                        bullet[i].IsActive = false;
                    }*/

                    if (bullet[i].Collide(player.GetPosition(), player.GetRay()))
                    {
                        player.OnHit();
                        bullet[i].IsActive = false;
                    }
                }

            }

            
        }

        public static void Draw()
        {
            for (int i = 0; i < alien.Length; i++)
            {
                if (alien[i].IsVisible)
                    alien[i].Draw();
            }

            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].IsActive)
                    bullet[i].Draw();
            }
        }

        private static AlienBullet GetBullet()
        {
            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].IsActive == false)
                    return bullet[i];
            }

            return null;
        }

        public static void Shoot (Alien shooter)
        {
            AlienBullet b = GetBullet();
            if (b != null)
            {
                b.Position.X = shooter.AlienPosition.X;
                b.Position.Y = shooter.AlienPosition.Y + shooter.GetHeight() / 2 + 1;
                b.SetVelocity(new Vector2(0, 250));
                b.IsActive = true;
            }
        }

        public static bool CollideWithBullet(Bullet bullet)
        {
            for (int i = 0; i < alien.Length; i++)
            {
                if (alien[i].IsAlive)
                {
                    if (bullet.Collide(alien[i].AlienPosition, alien[i].GetWidth() / 2))
                    {
                        if (alien[i].OnHit())
                        {
                            bullet.IsActive = false;
                            Game.AddScore(5);

                            if (alien[i].CanShoot)
                            {
                                alien[i].CanShoot = false;
                                int prevAlien = i - aliensPerRow;
                                while (prevAlien >= 0)
                                {
                                    if (alien[prevAlien].IsAlive)
                                    {
                                        alien[prevAlien].CanShoot = true;
                                        break;
                                    }

                                    prevAlien -= aliensPerRow;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void OnAlienDeath()
        {
            numAlives--;
        }

        public static int GetAlives()
        {
            return numAlives;
        }


    }
}
