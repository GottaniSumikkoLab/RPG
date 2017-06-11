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
            MapEditSystemData.LoadFile();
            this.Width = 1280;//windowsize x
            this.Height = 720;//windowsize y
            this.Text = "MapEditer";
            this.StartPosition = FormStartPosition.CenterScreen;//画面中央表示
            this.BackColor = Color.FromArgb(0, 0, 0);//背景色

            Paint += new PaintEventHandler(MyHandler);
        }

        private void MyHandler(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < MapEditSystemData.pic_data[1].mapChipArray.Length; i++)
            {
                g.DrawImage(MapEditSystemData.pic_data[1].mapChipArray[i], new PointF(48f * i, 0));
            }
        }
    }
}
