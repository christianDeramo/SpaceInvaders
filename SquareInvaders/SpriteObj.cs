using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders
{
    class SpriteObj
    {
        private Sprite sprite;
        private Vector2 position;

        public Sprite Sprite { get { return sprite; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public int width { get { return sprite.width; } }
        public int height { get { return sprite.height; } }

        public SpriteObj(string fileName, int x = 0, int y = 0)
        {
            sprite = new Sprite(fileName);
            position.X = x;
            position.Y = y;
        }

        public SpriteObj(string fileName, Vector2 position):
            this(fileName, (int)position.X, (int)position.Y)
        {
        }

        public SpriteObj(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public void Draw()
        {
            GfxTools.DrawSprite(sprite, (int)position.X, (int)position.Y);
        }

        public void Traslate(Vector2 newPos)
        {
            position.X += newPos.X;
            position.Y += newPos.Y;
        }

        public void Traslate (float x, float y)
        {
            position.X += x;
            position.Y += y;
        }

        public void SetSprite (Sprite newSprite)
        {
            sprite = newSprite;
        }
    }
}
