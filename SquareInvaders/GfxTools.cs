using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders
{
    static class GfxTools
    {
        public static Window window;

        public static void Init(Window win)
        {
            window = win;
        }

        public static void PutPixel(byte r, byte g, byte b, int x, int y)
        {
            int position = (window.width * y + x) * 3;
            window.bitmap[position] = r;
            window.bitmap[position + 1] = g;
            window.bitmap[position + 2] = b;
        }

        public static void DrawSprite(Sprite sprite, int x, int y)
        {
            for (int i = 0; i < sprite.height; i++)
            {
                for (int j = 0; j < sprite.width; j++)
                {
                    int canvasX = (x + j);
                    int canvasY = (y + i);

                    if (canvasX < 0 || canvasX >= window.width || canvasY < 0 || canvasY >= window.height)
                    {
                        continue;
                    }

                    int spriteByteIndex = ((i * sprite.width) + j) * 4;

                    int spriteR = sprite.bitmap[spriteByteIndex];
                    int spriteG = sprite.bitmap[spriteByteIndex + 1];
                    int spriteB = sprite.bitmap[spriteByteIndex + 2];
                    int spriteA = sprite.bitmap[spriteByteIndex + 3];

                    float alpha = spriteA / 255f;

                    int windowByteIndex = ((canvasY * window.width) + canvasX) * 3;
                    int windowR = window.bitmap[windowByteIndex];
                    int windowG = window.bitmap[windowByteIndex + 1];
                    int windowB = window.bitmap[windowByteIndex + 2];

                    window.bitmap[windowByteIndex] = (byte)((spriteR * alpha) + (windowR * (1 - alpha)));
                    window.bitmap[windowByteIndex + 1] = (byte)((spriteG * alpha) + (windowG * (1 - alpha)));
                    window.bitmap[windowByteIndex + 2] = (byte)((spriteB * alpha) + (windowB * (1 - alpha)));
                }
            }
        }

        public static void HorizontalLine(int x, int y, int width, byte r, byte g, byte b)
        {
            for (int j = x; j <= x + width; j++)
            {
                PutPixel(r, g, b, j, y);
            }
        }

        public static void VerticalLine(int x, int y, int height, byte r, byte g, byte b)
        {
            for (int j = y; j <= y + height; j++)
            {
                PutPixel(r, g, b, x, j);
            }
        }

        public static void EmptyRectangle(int x, int y, int height, int width, byte r, byte g, byte b)
        {
            HorizontalLine(x, y, width, r, g, b);
            VerticalLine(x, y, height, r, g, b);
            HorizontalLine(x, y + height, width, r, g, b);
            VerticalLine(x + width, y, height, r, g, b);
        }

        public static void FullRectangle(int x, int y, int height, int width, byte r, byte g, byte b)
        {
            for (int i = y; i < y + height; i++)
            {
                HorizontalLine(x, i, width, r, g, b);
            }
        }
        
        public static void ClearScreen()
        {
            for (int i = 0; i < window.bitmap.Length; i++)
            {
                window.bitmap[i] = 0;
            }
        }
    }
}
