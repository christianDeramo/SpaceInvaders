using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders
{
    class Rect
    {
        Vector2 position;
        int width;
        int height;
        Color color;

        public Rect(float xPos, float yPos, int w, int h, Color col)
        {
            position.X = xPos;
            position.Y = yPos;
            width = w;
            height = h;
            color = col;
        }

        public void Draw()
        {
            GfxTools.FullRectangle((int)position.X, (int)position.Y, width, height, color.R, color.G, color.B);
        }
    }
}
