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
        public const string Tilesets = "tilesets/";
        public const string Dungeon_A = "Dungeon_A";
        public const string Png = ".png";
        public static Bitmap[] Dungeon_A_Arr = new Bitmap[2];

        public static void LoadFile()
        {
            for (int i = 0; i < Dungeon_A_Arr.Length; i++)
            {
                Dungeon_A_Arr[i] = new Bitmap(Tilesets + Dungeon_A + (i + 1) + Png);
            }
        }
    }
}
