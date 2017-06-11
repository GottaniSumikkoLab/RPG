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
        public const int pic_num = 31;

        public static Map[] pic_data = new Map[pic_num];

        public static void LoadFile()
        {
            pic_data[0]  = new Map(16, 12, "Dungeon_A1");
            pic_data[1]  = new Map(16, 12, "Dungeon_A2");
            pic_data[2]  = new Map(16, 15, "Dungeon_A4");
            pic_data[3]  = new Map(8 , 16, "Dungeon_A5");
            pic_data[4]  = new Map(16, 16, "Dungeon_B" );
            pic_data[5]  = new Map(16, 16, "Dungeon_C" );
            pic_data[6]  = new Map(16, 12, "Inside_A1" );
            pic_data[7]  = new Map(16, 12, "Inside_A2" );
            pic_data[8]  = new Map(16, 15, "Inside_A4" );
            pic_data[9]  = new Map(8 , 16, "Inside_A5" );
            pic_data[10] = new Map(16, 16, "Inside_B"  );
            pic_data[11] = new Map(16, 16, "Inside_C"  );
            pic_data[12] = new Map(16, 12, "Outside_A1");
            pic_data[13] = new Map(16, 12, "Outside_A2");
            pic_data[14] = new Map(16, 8 , "Outside_A3");
            pic_data[15] = new Map(16, 15, "Outside_A4");
            pic_data[16] = new Map( 8, 16, "Outside_A5");
            pic_data[17] = new Map(16, 16, "Outside_B" );
            pic_data[18] = new Map(16, 16, "Outside_C" );
            pic_data[19] = new Map(16, 15, "SF_Inside_A4");
            pic_data[20] = new Map(16, 16, "SF_Inside_B");
            pic_data[21] = new Map(16, 16, "SF_Inside_C");
            pic_data[22] = new Map(16, 8 , "SF_Outside_A3");
            pic_data[23] = new Map(16, 15, "SF_Outside_A4");
            pic_data[24] = new Map(8 , 16, "SF_Outside_A5");
            pic_data[25] = new Map(16, 16, "SF_Outside_B");
            pic_data[26] = new Map(16, 16, "SF_Outside_C");
            pic_data[27] = new Map(16, 12, "World_A1");
            pic_data[28] = new Map(16, 12, "World_A2");
            pic_data[29] = new Map(16, 16, "World_B");
            pic_data[30] = new Map(16, 16, "World_C");
        }
    }
}
