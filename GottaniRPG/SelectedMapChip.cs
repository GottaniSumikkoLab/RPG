using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GottaniRPG
{
    class SelectedMapChip
    {
        public Bitmap MapChip;
        public string tileset_name;
        public int MapChip_index;
        public SelectedMapChip(Bitmap mapchip, string name, int index)
        {
            MapChip = mapchip;
            tileset_name = name;
            MapChip_index = index;
        }
    }
}
