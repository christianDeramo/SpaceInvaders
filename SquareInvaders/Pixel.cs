using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders
{
    class Pixel
    {
        int size;
        Color color;
        float gravity;

        public Vector2 Position;
        public Vector2 Velocity;
        public bool IsVisible;

        public float Y { get { return Velocity.Y; } set { Velocity.Y = value; } }

        public Pixel (Vector2 position, int pixelSize, Color color)
        {
            size = pixelSize;
            this.color = color;
            gravity = 250.0f;
            Position = position;
            IsVisible = true;
        }

        public void Draw ()
        {
            GfxTools.FullRectangle((int)Position.X, (int)Position.Y, size, size, color.R, color.G, color.B);
        }

        public void Traslate(float x, float y)
        {
            Position.X += x;
            Position.Y += y;
        }

        public void Update()
        {
            Position.X += (Velocity.X * (RandomGenerator.GetRandom(2, 20))) * GfxTools.window.deltaTime;
            Velocity.Y += gravity * GfxTools.window.deltaTime;
            Position.Y += (Velocity.Y * (RandomGenerator.GetRandom(2, 10))) * GfxTools.window.deltaTime;
        }


    }
}
