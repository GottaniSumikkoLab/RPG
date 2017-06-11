using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GottaniRPG
{
    class MapEditSystemData
    {
        public const int pic_num = 2;
        public const string Tilesets = "tilesets/";
        public const string Dungeon_A = "Dungeon_A";
        public const string Png = ".png";
        public static Bitmap[] Dungeon_A_Arr = new Bitmap[pic_num];

        public static Map[] pic_data = new Map[pic_num];

        public static void LoadFile()
        {
            for (int i = 0; i < Dungeon_A_Arr.Length; i++)
            {
                Dungeon_A_Arr[i] = new Bitmap(Tilesets + Dungeon_A + (i + 1) + Png);
            }

            pic_data[0] = new Map(16, 12, Dungeon_A_Arr[0]);
            pic_data[1] = new Map(16, 12, Dungeon_A_Arr[1]);
        }
    }
}
