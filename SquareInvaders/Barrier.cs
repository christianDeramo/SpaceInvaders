using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders
{
    class Barrier
    {
        SpriteObj barrier;
        Vector2 position;
        int width;
        int height;

        public int Height { get { return height; } set { } }
        public Vector2 Position { get { return position; } set { } }

        public Barrier(string fileName, Vector2 startPosition)
        {
            barrier = new SpriteObj(fileName, startPosition);
            width = barrier.width;
            height = barrier.height;
            position.X = startPosition.X + width / 2;
            position.Y = startPosition.Y + height / 2;
        }

        public void Draw()
        {
            barrier.Draw();
        }

        public bool PixelCollision(Vector2 center, float ray, bool erase = false)
        {
            bool collision = false;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2 pixelPos = new Vector2 (position.X - width/2 + x, position.Y - height/2 + y);
                    float pixToBullet = pixelPos.Sub(center).GetLenght();
                    if (pixToBullet <= 15)
                    {
                        collision = true;
                        int pixelAlphaIndex = (y * width + x) * 4 + 3;
                        barrier.Sprite.bitmap[pixelAlphaIndex] = 0;
                    }

                }
            }
            
            return collision;
        }
    }
}
