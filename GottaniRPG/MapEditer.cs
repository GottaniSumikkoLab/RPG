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
            Paint += new PaintEventHandler(GridLine);
        }

        private void MyHandler(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < MapEditSystemData.pic_data[1].mapChipArray.Length; i++)
            {
                g.DrawImage(MapEditSystemData.pic_data[1].mapChipArray[i], new PointF(48f * i, 0));
            }
        }

        private void GridLine(object sender, PaintEventArgs e)
        {
            Graphics verticalline = e.Graphics;
            Graphics horizonalline = e.Graphics;

            for(int j = 0; j < this.Height / 48; j++)
            {
                horizonalline.DrawLine(new Pen(Color.White), 0, j*48, this.Width, j*48);
            }

            for (int i = 0; i < this.Width / 48; i++)
            {
                verticalline.DrawLine(new Pen(Color.White), i * 48, 0, i * 48, this.Height);
            }
        }
    }
}
