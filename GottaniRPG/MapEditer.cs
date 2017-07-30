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
        private Panel Edit;
        private TableLayoutPanel UI;
        private TableLayoutPanel TilesName;
        private Panel MapChip;

        private Label tilesName;

        private PictureBox[] pb_arr = new PictureBox[256];

        private int TilesIndex = 0;

        private MenuStrip menustrip;

        private SelectedMapChip selectedMapChip;

        public int MapSizeX = 0;
        public int MapSizeY = 0;
        public int MapLayer = 3;

        public int[,] Layercnt;

        public Bitmap EditMap = new Bitmap(300, 300);

        public struct MapChipData
        {
            public string tileset_name;
            public int mapChip_index;

            public MapChipData(string x, int y)
            {
                tileset_name = x;
                mapChip_index = y;
            }
        }
        public MapChipData[,,] EditMapArray;

        private bool EditMapMoveFlag = false;
        private bool MouseMoveFlag = false;
        private Point FormerMousePos = new Point(0, 0);
        private Point EditMapWorldPos = new Point(0, 0);


        public MapEditer()
        {
            MESysData.LoadFile();

            Form_init();

            Load += new EventHandler(MenuBar);
          
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
            this.SuspendLayout();
            Edit_or_UI = new TableLayoutPanel();
            AddColumnStyles(Edit_or_UI, 2, new int[] { 80, 20});
            Edit_or_UI.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Edit_or_UI.Dock = DockStyle.Fill;
            Edit_or_UI.ColumnCount = 2;
            Edit_or_UI.RowCount = 1;

            Edit = new EditPanel();
            Edit.Dock = DockStyle.Fill;
            Edit.BackColor = Color.FromArgb(0,0,0);
            Edit.Paint += new PaintEventHandler(DrawMap);
            Edit.MouseDown += new MouseEventHandler(EditMap_MouseDown);
            Edit.MouseMove += new MouseEventHandler(EditMap_MouseMove);
            Edit.MouseUp += new MouseEventHandler(EditMap_MouseUp);
            

            UI = new TableLayoutPanel();
            AddRowStyles(UI, 2, new int[] { 6, 94});
            UI.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            UI.Dock = DockStyle.Fill;
            UI.ColumnCount = 1;
            UI.RowCount = 3;

            TilesName = new TableLayoutPanel();
            AddColumnStyles(TilesName, 3, new int[] { 20, 60, 20});
            TilesName.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            TilesName.Dock = DockStyle.Fill;
            TilesName.ColumnCount = 3;
            TilesName.RowCount = 1;
            Button TilesName_left = new Button();
            TilesName_left.Click += new EventHandler(MapChipList_Left_button);
            tilesName = new Label();
            tilesName.Text = MESysData.pic_data[0].name;
            Button TilesName_right = new Button();
            TilesName_right.Click += new EventHandler(MapChipList_Right_button);
            TilesName_left.Parent = TilesName;
            tilesName.Parent = TilesName;
            TilesName_right.Parent = TilesName;

            MapChip = new Panel();
            MapChip.Dock = DockStyle.Fill;
            MapChip.BackColor = Color.FromArgb(160, 160, 160);
            MapChip.AutoScroll = true;
            MapChip.MouseClick += new MouseEventHandler(MapChipList_mouseClick);
            MapChip.Paint += new PaintEventHandler(PaintMapChipList);

            TilesName.Parent = UI;
            MapChip.Parent = UI;
            Edit.Parent = Edit_or_UI;
            UI.Parent = Edit_or_UI;
            Edit_or_UI.Parent = this;
            this.ResumeLayout();
        }

        private void MenuBar(object sender,EventArgs e)
        {
            this.menustrip = new MenuStrip();

            this.SuspendLayout();
            this.menustrip.SuspendLayout();

            ToolStripMenuItem filemenuitem = new ToolStripMenuItem();
            filemenuitem.Text = "ファイル(&F)";
            filemenuitem.ShortcutKeys = Keys.Control | Keys.F;
            filemenuitem.ShowShortcutKeys = true;
            this.menustrip.Items.Add(filemenuitem);

            ToolStripMenuItem CreateMapitem = new ToolStripMenuItem();
            CreateMapitem.Text = "新規作成(&C)";
            CreateMapitem.ShortcutKeys = Keys.Control | Keys.C;
            CreateMapitem.ShowShortcutKeys = true;
            CreateMapitem.Click += new EventHandler(CreateMapToolStripMenuItem_Click);
            filemenuitem.DropDownItems.Add(CreateMapitem);

            ToolStripMenuItem openmenuitem = new ToolStripMenuItem();
            openmenuitem.Text = "開く(&O)";
            openmenuitem.ShortcutKeys = Keys.Control | Keys.O;
            openmenuitem.ShowShortcutKeys = true;
            openmenuitem.Click += new EventHandler(OpenToolStripMenuItem_Click);
            filemenuitem.DropDownItems.Add(openmenuitem);

            ToolStripMenuItem savemenuitem = new ToolStripMenuItem();
            savemenuitem.Text = "名前を付けて保存(&N)";
            savemenuitem.ShortcutKeys = Keys.Control | Keys.N;
            savemenuitem.ShowShortcutKeys = true;
            savemenuitem.Click += new EventHandler(SaveToolStripMenuItem_Click);
            filemenuitem.DropDownItems.Add(savemenuitem);

            ToolStripMenuItem exitmenuitem = new ToolStripMenuItem();
            exitmenuitem.Text = "終了(&X)";
            exitmenuitem.ShortcutKeys = Keys.Control | Keys.X;
            exitmenuitem.ShowShortcutKeys = true;
            filemenuitem.DropDownItems.Add(exitmenuitem);
        
            this.Controls.Add(this.menustrip);
            this.MainMenuStrip = this.menustrip;
            this.menustrip.ResumeLayout(false);
            this.menustrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CreateMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMapDialog f = new CreateMapDialog();
            int s;
            int t;
            if (f.ShowDialog(this) == DialogResult.OK) {
                if (int.TryParse(f.tate.Text, out s) && int.TryParse(f.yoko.Text, out t))
                {
                    MapSizeX = s;
                    MapSizeY = t;
                    EditMapArray = new MapChipData[MapSizeX,MapSizeY,MapLayer];
                    Layercnt = new int[MapSizeX, MapSizeY];

                    for(int i = 0; i < MapSizeX; i++)
                    {
                        for(int j = 0; j < MapSizeY; j++)
                        {
                            for(int k = 0; k < MapLayer; k++)
                            {
                                EditMapArray[i, j, k] = new MapChipData("", -1);
                            }//k
                        }//j
                    }//i

                    EditMap = new Bitmap(MapSizeX * MESysData.MapChipSize, MapSizeY * MESysData.MapChipSize);
                    GridLine(EditMap);
                    Edit.Invalidate();
                }//if TryParse
                else
                {
                    MessageBox.Show("正しい値を入力してください。",
                                    "エラー",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }//else
            }//if DialogResult
            f.Dispose();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(ofd.FileName);
            }

            ofd.Dispose();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "newmapdata.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter nf = new System.IO.StreamWriter(sfd.FileName);
                for (int i = 0; i < MapSizeX; i++)
                {
                    for (int j = 0; j < MapSizeY; j++)
                    {
                        for (int k = 0; k < MapLayer; k++)
                        {
                            nf.WriteLine(EditMapArray[i, j, k].tileset_name +","+ EditMapArray[i, j, k].mapChip_index +","+ i + "," + j + "," + k);
                        }//k
                    }//j
                }//i
                nf.Close();
            }//if
            sfd.Dispose();
        }

        private void AddColumnStyles(TableLayoutPanel tlp, int num, int[] per)
        {
            this.SuspendLayout();
            for (int i = 0; i < num; i++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                tlp.ColumnStyles[i] = new ColumnStyle(SizeType.Percent, per[i]);
            }
            this.ResumeLayout();
        }

        private void AddRowStyles(TableLayoutPanel tlp, int num,int[] per)
        {
            this.SuspendLayout();
            for (int i = 0; i < num; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent));
                tlp.RowStyles[i] = new RowStyle(SizeType.Percent, per[i]);
            }
            this.ResumeLayout();
        }

        private void PaintMapChipList(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(MapChip.AutoScrollPosition.X, MapChip.AutoScrollPosition.Y);
            Graphics g = e.Graphics;
            for (int i = 0; i < MESysData.pic_data[TilesIndex % MESysData.pic_num].mapChipArray.Length; i++)
            {
                g.DrawImage(MESysData.pic_data[TilesIndex % MESysData.pic_num].mapChipArray[i], new PointF(3 + (54f * (i % 4)), 3 + (54f * (i / 4))));
            }
            int rows = MESysData.pic_data[TilesIndex % MESysData.pic_num].mapChipArray.Count(n => n != null) / 4;
            MapChip.AutoScrollMinSize = new Size(MapChip.Width, 54 * rows + 3);
        }

        private void DrawMap(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle srcRect = new Rectangle(EditMapWorldPos.X, EditMapWorldPos.Y, Edit.Size.Width, Edit.Size.Height);
            Rectangle desRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);
            g.DrawImage(EditMap, desRect, srcRect, GraphicsUnit.Pixel);
        }

        private void GridLine(Bitmap img)
        {
            Graphics verticalline = Graphics.FromImage(img);
            Graphics horizonalline = Graphics.FromImage(img);

            for(int j = 0; j < img.Height / MESysData.MapChipSize; j++)
            {
                horizonalline.DrawLine(new Pen(Color.White), 0, j* MESysData.MapChipSize, img.Width, j* MESysData.MapChipSize);
            }

            for (int i = 0; i < img.Width / MESysData.MapChipSize; i++)
            {
                verticalline.DrawLine(new Pen(Color.White), i * MESysData.MapChipSize, 0, i * MESysData.MapChipSize, img.Height);
            }
        }

        private void MapChipList_Left_button(object sender, EventArgs e)
        {
            TilesIndex--;
            if (TilesIndex < 0) TilesIndex += MESysData.pic_num;
            tilesName.Text = MESysData.pic_data[TilesIndex % MESysData.pic_num].name;
            MapChip.Refresh();
        }

        private void MapChipList_Right_button(object sender, EventArgs e)
        {
            TilesIndex++;
            tilesName.Text = MESysData.pic_data[TilesIndex % MESysData.pic_num].name;
            MapChip.Refresh();
        }

        public Point ScreenToGrid_MapChipPanel(Point pos)
        {
            Point ret = new Point();
            ret.X = pos.X / 54;
            ret.Y = pos.Y / 54;
            return ret;
        }

        public Point ScreenToGrid_EditMap(Point pos)
        {
            Point ret = new Point();
            ret.X = pos.X / MESysData.MapChipSize;
            ret.Y = pos.Y / MESysData.MapChipSize;
            return ret;
        }

        void MapChipList_mouseClick(object sender, MouseEventArgs e)
        {
            Point sum = new Point();
            sum.X = MapChip.PointToClient(Cursor.Position).X - MapChip.AutoScrollPosition.X;
            sum.Y = MapChip.PointToClient(Cursor.Position).Y - MapChip.AutoScrollPosition.Y;
            sum = ScreenToGrid_MapChipPanel(sum);
            selectedMapChip = new SelectedMapChip(MESysData.pic_data[TilesIndex % MESysData.pic_num].mapChipArray[sum.X + 4 * sum.Y],
                                                  MESysData.pic_data[TilesIndex % MESysData.pic_num].name,
                                                  sum.X + 4 * sum.Y);
        }

        private void EditMap_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    EditMapMoveFlag = true;
                    break;
                case MouseButtons.Right:
                    MouseMoveFlag = true;
                    DrawMapChip();
                    break;
            }

            FormerMousePos = Edit.PointToClient(Cursor.Position);
            
        }

        private void EditMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (EditMapMoveFlag)
            {
                EditMapWorldPos.X -= e.X - FormerMousePos.X;
                EditMapWorldPos.Y -= e.Y - FormerMousePos.Y;
                EditMapWorldPos.X = Utils.Clamp<int>(EditMapWorldPos.X, 0, EditMap.Size.Width - Edit.Size.Width);
                EditMapWorldPos.Y = Utils.Clamp<int>(EditMapWorldPos.Y, 0, EditMap.Size.Height - Edit.Size.Height);
                Edit.Refresh();
                FormerMousePos.X = e.X;
                FormerMousePos.Y = e.Y;
            }

            if (MouseMoveFlag)
            {
                DrawMapChip();
            }

        }

        private void EditMap_MouseUp(object sender, MouseEventArgs e)
        {
            EditMapMoveFlag = false;
            MouseMoveFlag = false;
        }

        private void DrawMapChip()
        {
            
            Point tmp = new Point();
            tmp.X = EditMapWorldPos.X + Edit.PointToClient(Cursor.Position).X;
            tmp.Y = EditMapWorldPos.Y + Edit.PointToClient(Cursor.Position).Y;
            tmp = ScreenToGrid_EditMap(tmp);
            if (Layercnt[tmp.X,tmp.Y] >= MapLayer)
            {
                MessageBox.Show("レイヤー上限です。",
                                    "エラー",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                EditMapMoveFlag = false;
                MouseMoveFlag = false;

            }//if
            else
            {
                Graphics g = Graphics.FromImage(EditMap);
                g.DrawImage(selectedMapChip.MapChip, tmp.X * MESysData.MapChipSize, tmp.Y * MESysData.MapChipSize);
                Edit.Refresh();
                g.Dispose();

                EditMapArray[tmp.X, tmp.Y, Layercnt[tmp.X, tmp.Y]] = new MapChipData(selectedMapChip.tileset_name, selectedMapChip.MapChip_index);
                Layercnt[tmp.X,tmp.Y]++;
            }//else
        }//method DrawMapChip
    }//class MapEditer
}//namespace
