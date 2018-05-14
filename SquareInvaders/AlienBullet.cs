using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders
{
    class AlienBullet
    {
        SpriteObj sprite;
        Sprite[] bullets;
        Animation animation;
        Vector2 Velocity;
        int height;
        int width;
        public Vector2 Position;
        public bool IsActive;

        public AlienBullet()
        {
            Position.X = 0;
            Position.Y = 0;
            Velocity = Position;
            bullets = new Sprite[2];

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Sprite("Asset/alienBullet_" + i + ".png");
            }

            animation = new Animation(bullets, 30, sprite);
            IsActive = false;
            
        }

        public void Draw()
        {
            GfxTools.DrawSprite(bullets[animation.CurrentFrame], (int)Position.X, (int)Position.Y);
        }

        public bool Collide(Vector2 centerPosition, float ray)
        {
            Vector2 dist = Position.Sub(centerPosition);
            if (dist.GetLenght() <= width / 2 + ray)
                return true;

            return false;
        }

        public void Update()
        {
            animation.Update();

            Position.X += Velocity.X * GfxTools.window.deltaTime;
            Position.Y += Velocity.Y * GfxTools.window.deltaTime;

            if ((int)Position.Y - height / 2 < 0 || (int)Position.Y + height / 2 > GfxTools.window.height)
                IsActive = false;

            if (!IsActive)
            {
                Position.X = 0;
                Position.Y = 0;
            }

        }
        
        public void SetVelocity(Vector2 newVel)
        {
            Velocity = newVel;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }
    }
}
