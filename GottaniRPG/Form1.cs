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
    public partial class Form1 : Form
    {
        
        private SoundPlayer soundPlayer;
        Dictionary<int, string> nameList = new Dictionary<int, string>();
        private TextBox textBox1;
        private TableLayoutPanel tlp;
        private Label lb;
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        private string aliasName = "MediaFile";
        private int key;

        public Form1(SoundPlayer soundPlayer)
        {
            this.soundPlayer = soundPlayer;
            InitializeComponent();

            tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 1;
            tlp.RowCount = 6;

            lb = new Label();
            lb.Parent = tlp;

            Button button1 = new Button();
            button1.Text = "サウンド再生";
            button1.Click += new EventHandler(button1_Click);
            button1.Dock = DockStyle.Fill;
            button1.Parent = tlp;

            Button button2 = new Button();
            button2.Text = "サウンド停止";
            button2.Click += new EventHandler(button2_Click);
            button2.Dock = DockStyle.Fill;
            button2.Parent = tlp;

            Button button3 = new Button();
            button3.Text = "画面モード変更";
            button3.Click += new EventHandler(button3_Click);
            button3.Dock = DockStyle.Fill;
            button3.Parent = tlp;

            textBox1 = new TextBox();
            textBox1.Parent = tlp;

            tlp.Parent = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //再生するファイル名
            string fileName = "sound/nc150815.wav";

            string cmd;
            //ファイルを開く
            cmd = "open \"" + fileName + "\" type mpegvideo alias " + aliasName;
            if (mciSendString(cmd, null, 0, IntPtr.Zero) != 0)
                return;

            // 音量を小さくする
            mciSendString("setaudio " + aliasName + " volume to 100", null, 0, IntPtr.Zero);

            //再生する
            cmd = "play " + aliasName;
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string cmd;
            //再生しているWAVEを停止する
            cmd = "stop " + aliasName;
            mciSendString(cmd, null, 0, IntPtr.Zero);
            //閉じる
            cmd = "close " + aliasName;
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
           
            switch (key)
            {
                case 0:
                    {
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.WindowState = FormWindowState.Maximized;//window->Full
                        key = 1;
                        break;
                    }//case 0 end
                case 1:
                    {
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.WindowState = FormWindowState.Normal;//Full->window
                        key = 0;
                        break;
                    }//case 1 end
            }//switch end
        }//btn_click end
    }
}
