using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GottaniRPG
{
    class Map
    {
        private int pic_size_x = 48;
        private int pic_size_y = 48;
        public string name;
        public Bitmap[] mapChipArray;
        public Map(int x, int y, string name)
        {
            this.name = name;
            int sum = x * y;

            Bitmap large_map = new Bitmap("tilesets/" + name + ".png");

            mapChipArray = new Bitmap[sum];

            for (int row = 0; row < y; row++)
            {
                for (int col = 0; col < x; col++)
                {
                    mapChipArray[x*row + col] = new Bitmap(pic_size_x, pic_size_y);
                    Graphics graphics = Graphics.FromImage(mapChipArray[x * row + col]);
                    graphics.DrawImage(large_map, new Rectangle(0, 0, pic_size_x, pic_size_y),
                        new Rectangle(pic_size_x * (col), pic_size_y * (row), pic_size_x, pic_size_y), GraphicsUnit.Pixel);
                }
            }
        }
    }
}
