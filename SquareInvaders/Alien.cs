using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders
{
    class Alien
    {
        int height;
        int width;
        int distToSide;
        int numPixel;
        float nextShot;
        Pixel[] sprite;

        public bool IsVisible;
        public bool IsAlive;
        public bool CanShoot;
        public byte[] pixelArr;
        public Color color;
        public Vector2 AlienPosition;
        public Vector2 Velocity;

        public Alien (Vector2 position, Color color, int height, int width)
        {
            this.height = height;
            this.width = width;
            AlienPosition.X = position.X + this.height / 2;
            AlienPosition.Y = position.Y + this.height / 2;
            IsVisible = true;
            IsAlive = true;
            distToSide = 20;
            byte[] pixelArr = {  0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                                 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                                 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
                                 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1,
                                 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1,
                                 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0 };

            for (int i = 0; i < pixelArr.Length; i++)
            {
                if (pixelArr[i] == 1)
                    numPixel++;
            }

            sprite = new Pixel[numPixel];

            int verticalPixel = 8;
            int horizontalPixel = 11;
            int pixelSize = height / verticalPixel;
            this.width = horizontalPixel * pixelSize;

            float startPosX = AlienPosition.X - (float)height / 2;
            float posY = AlienPosition.Y - height / 2;

            int sp = 0;
            for (int i = 0; i < pixelArr.Length; i++)
            {
                if (i != 0 && i % horizontalPixel == 0)
                    posY += pixelSize;

                if (pixelArr[i] != 0)
                {
                    float pixelX = startPosX + (i % horizontalPixel) * (pixelSize);
                    sprite[sp] = new Pixel(new Vector2(pixelX, posY), pixelSize, color);
                    sprite[sp].Velocity = sprite[sp].Position.Sub(AlienPosition);
                    sp++;
                }
            }

            Velocity = new Vector2(100, 0);

            this.color = color;

            nextShot = RandomGenerator.GetRandom(5, 10);
        }

        public bool Update(ref float overflowX)
        {
            if (CanShoot)
            {
                nextShot -= GfxTools.window.deltaTime;
                if (nextShot <= 0)
                {
                    nextShot = RandomGenerator.GetRandom(5, 10);
                    EnemyManager.Shoot(this);
                }
            }

            float deltaX = Velocity.X * GfxTools.window.deltaTime;
            float deltaY = Velocity.Y * GfxTools.window.deltaTime;
            AlienPosition.X += deltaX;
            AlienPosition.Y += deltaY;

            float maxX = AlienPosition.X + height / 2;
            float minX = AlienPosition.X - height / 2;

            if (maxX > GfxTools.window.width - distToSide)
            {
                overflowX = maxX - (GfxTools.window.width - distToSide);
                return true;
            }

            else if (minX < distToSide)
            {
                overflowX = minX - distToSide;
                return true;
            }

            TraslateSprite(new Vector2(deltaX, deltaY));

            if (IsVisible && !IsAlive)
            {
                for (int i = 0; i < sprite.Length; i++)
                {
                    sprite[i].Update();
                    if (sprite[i].Position.Y >= GfxTools.window.height - 20)
                    {
                        sprite[i].IsVisible = false;
                    }
                }
            }

            return false;
        }

        public void Draw()
        {
            for (int i = 0; i < sprite.Length; i++)
            {
                if (sprite[i].IsVisible)
                    sprite[i].Draw();
            }
        }

        public bool OnHit()
        {
            IsAlive = false;
            EnemyManager.OnAlienDeath();
            return true;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public void TraslateSprite(Vector2 transVect)
        {
            for (int i = 0; i < sprite.Length; i++)
            {
                sprite[i].Traslate(transVect.X, transVect.Y);
            }
        }

        public void Traslate (Vector2 movement)
        {
            AlienPosition.X += movement.X;
            AlienPosition.Y += movement.Y;
        }

        public void SetVelocity(Vector2 newVelociity)
        {
            Velocity.X = newVelociity.X;
            Velocity.Y = newVelociity.Y;
        }
    }
}
