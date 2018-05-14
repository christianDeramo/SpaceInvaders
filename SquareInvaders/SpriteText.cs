using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders
{
    class SpriteText
    {
        SpriteObj[] sprites;
        string text;

        public Vector2 Position;
        public string Text { get { return text; } set { SetText(value); } }

        public SpriteText(Vector2 position, string text)
        {
            Position = position;
            sprites = new SpriteObj[10];
        }

        public void SetText(string text)
        {
            this.text = text;
            int numChar = text.Length;
            int charX = (int)Position.X;
            int charY = (int)Position.Y;
            int offset = 15;

            if (numChar > sprites.Length)
                numChar = sprites.Length;

            for (int i = 0; i < numChar; i++)
            {
                char number = text[i];
                sprites[i] = new SpriteObj("Asset/numbers_" + number + ".png", charX + i * offset, charY);
            }
        }

        public void Draw()
        {
            for (int i = 0; i < text.Length; i++)
            {
                sprites[i].Draw();
            }
        }
    }
}
