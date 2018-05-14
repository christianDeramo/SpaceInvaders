using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SquareInvaders
{
    class Animation
    {
        SpriteObj owner;
        Sprite[] sprites;
        float fps;
        float counter;
        int numFrames;
        int currentIndex;

        public bool Loop { get; set; }
        public bool IsPlaying { get; private set; }

        public int CurrentFrame
        {
            get { return currentIndex; }
            set { owner.SetSprite(sprites[currentIndex]); }
        }

        public Animation(Sprite[] sprite, float fps, SpriteObj owner)
        {
            this.owner = owner;
            this.fps = fps;
            sprites = new Sprite[sprite.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = sprite[i];
            }

            numFrames = sprite.Length;
            currentIndex = 0;
            counter = 0;
        }
        
        public void Update()
        {
            counter += GfxTools.window.deltaTime;

            if (counter >= 1 / fps)
            {
                currentIndex++;
                counter = 0;
            }

            if (currentIndex >= numFrames || Loop)
                Restart();
        }

        public void Start()
        {
            IsPlaying = true;
        }

        public void Restart()
        {
            currentIndex = 0;
            IsPlaying = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Stop()
        {
            currentIndex = 0;
            IsPlaying = false;
        }
    }
}
