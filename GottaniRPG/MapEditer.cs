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
    public partial class MapEditer : Form
    {
        public MapEditer()
        {
            this.Width = 640;//windowsize x
            this.Height = 480;//windowsize y
            this.StartPosition = FormStartPosition.CenterScreen;//画面中央表示
            this.BackColor = Color.FromArgb(0, 0, 0);//背景色
        }
    }
}
