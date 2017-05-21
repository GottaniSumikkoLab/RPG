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
        SoundPlayer soundPlayer = new SoundPlayer();
        Dictionary<int, string> nameList = new Dictionary<int, string>();
        public Form1()
        {
            InitializeComponent();

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //再生終了イベントが不要なら↓の2行は不要
            soundPlayer.Init(this);
            soundPlayer.PlayEnd += SoundPlayer_PlayEnd; //再生終了イベント

            OpenSound("sound/coin07.mp3", "coin");
            OpenSound("sound/crrect_answer3.mp3", "crrect");
            OpenSound("sound/diving1.mp3", "diving");
        }

        void OpenSound(string file, string name)
        {
            var id = soundPlayer.Open(file, name);
            nameList.Add(id, name); //idと名前を保存
        }

        private void SoundPlayer_PlayEnd(int id)
        {
            var name = nameList[id]; //idから名前を取得
            switch (name)
            {
                case "crrect": //1回でクローズ
                    soundPlayer.Close(name);
                    nameList.Remove(id);
                    break;
                case "diving": //ループ
                    soundPlayer.Seek(name, "0");
                    soundPlayer.Play(name);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //コイーン！を再生
            soundPlayer.Seek("coin", "0");
            soundPlayer.Play("coin");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //正解！を1回だけ再生
            soundPlayer.Play("crrect");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ブクブクブクをループ再生
            soundPlayer.Play("diving");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ステータス表示
            var sb = new StringBuilder();
            foreach (var name in nameList.Values)
            {
                var status = soundPlayer.Status(name);
                var pos = soundPlayer.Position(name);
                sb.AppendLine($"{name} {status} {pos}");
            }
            //textBox1.Text = sb.ToString();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //忘れずに開放処理
            soundPlayer.CloseAll();
            soundPlayer.Dispose();
        }
    }
}
