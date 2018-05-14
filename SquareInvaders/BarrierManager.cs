using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareInvaders
{
    static class BarrierManager
    {
        static public Barrier[] barriers;
        
        public static void Init(Vector2 startPosition)
        {
            barriers = new Barrier[5];
            int offset = 200;
            for (int i = 0; i < barriers.Length; i++)
            {
                barriers[i] = new Barrier("Asset/barrier.png", startPosition);
                startPosition.X += offset;
            }
        }
        


        public static void Draw()
        {
            for (int i = 0; i < barriers.Length; i++)
            {
                barriers[i].Draw();
            }
        }
        
    }
}
