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
            this.Width = 1280;//windowsize x
            this.Height = 720;//windowsize y
            this.Text = "MapEditer";
            this.StartPosition = FormStartPosition.CenterScreen;//画面中央表示
            this.BackColor = Color.FromArgb(0, 0, 0);//背景色
        }
    }
}
