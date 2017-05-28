using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GottaniRPG
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SoundPlayer soundPlayer = new SoundPlayer();
            Form1 form = new Form1(soundPlayer);
            soundPlayer.Init(form);
            
            Application.Run(form);
        }
    }
}
