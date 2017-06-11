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
        private TableLayoutPanel Edit_or_UI;
        private TableLayoutPanel Edit;
        private TableLayoutPanel UI;
        private TableLayoutPanel MapName;
        private TableLayoutPanel Page;
        private TableLayoutPanel MapChip;

        private Label mapName;
        private PictureBox[] pb_arr = new PictureBox[256];

        private int MapIndex = 0;

        public MapEditer()
        {
            MapEditSystemData.LoadFile();

            Form_init();

            Paint += new PaintEventHandler(MyHandler);

            CreateUI();
            
        }

        private void Form_init()
        {
            this.Width = 1280;//windowsize x
            this.Height = 720;//windowsize y
            this.Text = "MapEditer";
            this.StartPosition = FormStartPosition.CenterScreen;//画面中央表示
            this.BackColor = Color.FromArgb(255, 255, 255);//背景色
        }

        private void CreateUI()
        {
            Edit_or_UI = new TableLayoutPanel();
            AddColumnStyles(Edit_or_UI, 2, new int[] { 80, 20});
            Edit_or_UI.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Edit_or_UI.Dock = DockStyle.Fill;
            Edit_or_UI.ColumnCount = 2;
            Edit_or_UI.RowCount = 1;

            Edit = new TableLayoutPanel();
            Edit.Dock = DockStyle.Fill;
            Edit.ColumnCount = 22;
            Edit.RowCount = 15;
            Edit.BackColor = Color.FromArgb(0,0,0);
            Edit.Paint += new PaintEventHandler(GridLine);

            UI = new TableLayoutPanel();
            AddRowStyles(UI, 2, new int[] { 6, 94});
            UI.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            UI.Dock = DockStyle.Fill;
            UI.ColumnCount = 1;
            UI.RowCount = 3;

            MapName = new TableLayoutPanel();
            AddColumnStyles(MapName, 3, new int[] { 20, 60, 20});
            MapName.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            MapName.Dock = DockStyle.Fill;
            MapName.ColumnCount = 3;
            MapName.RowCount = 1;
            Button MapName_left = new Button();
            MapName_left.Click += new EventHandler(Left_button);
            mapName = new Label();
            mapName.Text = MapEditSystemData.pic_data[0].name;
            Button MapName_right = new Button();
            MapName_right.Click += new EventHandler(Right_button);
            MapName_left.Parent = MapName;
            mapName.Parent = MapName;
            MapName_right.Parent = MapName;

            MapChip = new TableLayoutPanel();
            AddColumnStyles(MapChip, 4, new int[] { 25, 25, 25, 25});

            MapChip.Dock = DockStyle.Fill;
            MapChip.ColumnCount = 4;
            MapChip.RowCount = 11;
            MapChip.BackColor = Color.FromArgb(160, 160, 160);
            MapChip.AutoScroll = true;

            for (int i = 0; i < pb_arr.Length; i++)
            {
                pb_arr[i] = new PictureBox();
            }
            for (int i = 0; i < MapEditSystemData.pic_data[0].mapChipArray.Length; i++)
            {
                pb_arr[i].Image = MapEditSystemData.pic_data[0].mapChipArray[i];
                pb_arr[i].Parent = MapChip;
            }

            MapName.Parent = UI;
            MapChip.Parent = UI;
            Edit.Parent = Edit_or_UI;
            UI.Parent = Edit_or_UI;
            Edit_or_UI.Parent = this;
        }
        private void AddColumnStyles(TableLayoutPanel tlp, int num, int[] per)
        {
            for (int i = 0; i < num; i++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                tlp.ColumnStyles[i] = new ColumnStyle(SizeType.Percent, per[i]);
            }
        }

        private void AddRowStyles(TableLayoutPanel tlp, int num,int[] per)
        {
            for (int i = 0; i < num; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent));
                tlp.RowStyles[i] = new RowStyle(SizeType.Percent, per[i]);
            }
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
        private void Left_button(object sender, EventArgs e)
        {
            MapIndex--;
            if (MapIndex < 0) MapIndex += 31;
            mapName.Text = MapEditSystemData.pic_data[MapIndex % 31].name;
            for (int i = 0; i < MapEditSystemData.pic_data[MapIndex % 31].mapChipArray.Length; i++)
            {
               pb_arr[i].Image = MapEditSystemData.pic_data[MapIndex % 31].mapChipArray[i];
            }
            for (int j = pb_arr.Length-1; j >= MapEditSystemData.pic_data[MapIndex % 31].mapChipArray.Length; j--)
            {
                pb_arr[j].Image = null;
            }
        }
        private void Right_button(object sender, EventArgs e)
        {
            MapIndex++;
            mapName.Text = MapEditSystemData.pic_data[MapIndex % 31].name;
            for (int i = 0; i < MapEditSystemData.pic_data[MapIndex % 31].mapChipArray.Length; i++)
            {
                pb_arr[i].Image = MapEditSystemData.pic_data[MapIndex % 31].mapChipArray[i];
            }
            for (int j = pb_arr.Length-1; j >= MapEditSystemData.pic_data[MapIndex % 31].mapChipArray.Length; j--)
            {
                pb_arr[j].Image = null;
            }
        }
    }
}
