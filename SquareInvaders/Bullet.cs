using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders
{
    class Bullet
    {
        int height;
        int width;
        Vector2 Velocity;
        Color color;
        public Vector2 Position;
        public bool IsActive;
        public Bullet[] bullet = new Bullet[50];

        public Bullet (int height, int width, Color color)
        {
            Position.X = 0;
            Position.Y = 0;
            this.height = height;
            this.width = width;
            this.color = color;

            Velocity = Position;
        }

        public void Draw()
        {
            GfxTools.FullRectangle((int)Position.X - width / 2, (int)Position.Y - height / 2, height, width, color.R, color.G, color.B);
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
            Position.X += Velocity.X * GfxTools.window.deltaTime;
            Position.Y += Velocity.Y * GfxTools.window.deltaTime;

            for (int i = 0; i < BarrierManager.barriers.Length; i++)
            {
                if (Collide(BarrierManager.barriers[i].Position, BarrierManager.barriers[i].Height))
                    IsActive = false;
            }

            if((int)Position.Y - height / 2 < 0 || (int)Position.Y + height / 2 > GfxTools.window.height)
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
