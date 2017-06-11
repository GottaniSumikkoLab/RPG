using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GottaniRPG
{
    class Map
    {
        private int pic_size_x = 48;
        private int pic_size_y = 48;
        public Bitmap[] mapChipArray;
        public Map(int x, int y, Bitmap pic)
        {
            int sum = x * y;
            mapChipArray = new Bitmap[sum];

            for (int i = 0; i < sum; i++)
            {
                mapChipArray[i] = new Bitmap(pic_size_x,pic_size_y);
                Graphics graphics = Graphics.FromImage(mapChipArray[i]);
                graphics.DrawImage(pic, new Rectangle(0, 0, pic_size_x, pic_size_y),
                    new Rectangle(pic_size_x * (i % x), pic_size_y * (i / y), pic_size_x, pic_size_y), GraphicsUnit.Pixel);
            }
        }
    }
}
