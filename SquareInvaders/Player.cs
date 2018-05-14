using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders
{
    class Player
    {
        SpriteObj playerSprite;
        int width;
        int height;
        int hitCounter;
        float speed;
        int score;
        bool isSpacePressed;
        Vector2 position;
        public bool isAlive;
        public Bullet[] bullet = new Bullet[50];

        public Player(float x, float y)
        {
            isAlive = true;
            position.X = x;
            position.Y = y;
            playerSprite = new SpriteObj("Asset/player.png", position);
            width = playerSprite.width;
            height = playerSprite.height;

            hitCounter = 0;
            playerSprite.Traslate(-width/2, height/2);

            speed = 150;
            isSpacePressed = false;

            Color bulletCol;
            bulletCol.R = 0;
            bulletCol.G = 255;
            bulletCol.B = 0;

            for (int i = 0; i < bullet.Length; i++)
            {
                bullet[i] = new Bullet(10, 5, bulletCol);
            }
        }

        public void Draw()
        {
            playerSprite.Draw();

            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].IsActive)
                    bullet[i].Draw();
            }
        }

        public void KeyInput()
        {
            float squareSpeed = 300;

            if (GfxTools.window.GetKey(KeyCode.Right))
            {
                speed = squareSpeed;
                if (position.X + width / 2 >= GfxTools.window.width)
                    speed = 0;
            }

            else if (GfxTools.window.GetKey(KeyCode.Left))
            {
                speed = -squareSpeed;
                if (position.X - width / 2 <= 0)
                    speed = 0;
            }

            else
                speed = 0;

            if (GfxTools.window.GetKey(KeyCode.Space))
            {
                if (!isSpacePressed)
                {
                    isSpacePressed = true;
                    Shoot();
                }
            }

            else
                isSpacePressed = false;
        }

        public void Update()
        {
            position.X += speed * GfxTools.window.deltaTime;
            playerSprite.Traslate(speed * GfxTools.window.deltaTime, 0);
            
            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].IsActive)
                {
                    bullet[i].Update();

                    /*if (BarrierManager.Collision(bullet[i]))
                    {
                        bullet[i].IsActive = false;
                    }*/

                    if (EnemyManager.CollideWithBullet(bullet[i]))
                        bullet[i].IsActive = false;
                }
            }
        }

        private Bullet GetBullet()
        {
            for (int i = 0; i < bullet.Length; i++)
            {
                if (bullet[i].IsActive == false)
                    return bullet[i];
            }

            return null;
        }

        public void Shoot()
        {
            Bullet b = GetBullet();
            if (b != null)
            {
                b.Position.X = position.X;
                b.Position.Y = position.Y;
                b.SetVelocity(new Vector2(0, -250));
                b.IsActive = true;
            }
            
        }

        public bool OnHit()
        {
            hitCounter++;

            if (hitCounter >= 3)
                isAlive = false;

            return true;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public float GetRay()
        {
            return width / 2;
        }
        
    }
}
