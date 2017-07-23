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
    public class CreateMapDialog : Form
    {
        public TextBox tate;
        public TextBox yoko;
        private TableLayoutPanel createMapDialog;
        public CreateMapDialog()
        {
            Form_init();

            createMapDialog = new TableLayoutPanel();
            createMapDialog.Dock = DockStyle.Fill;
            createMapDialog.ColumnCount = 1;
            createMapDialog.RowCount = 3;
            createMapDialog.Parent = this;

            tate = new TextBox();
            tate.Parent = createMapDialog;


            yoko = new TextBox();
            yoko.Parent = createMapDialog;

            Button OKButton = new Button();
            OKButton.Text = "OK";
            OKButton.DialogResult = DialogResult.OK;
            OKButton.Parent = createMapDialog;
        }
        private void Form_init()
        {
            this.Width = 320;//windowsize x
            this.Height = 200;//windowsize y
            this.Text = "マップを新規作成";
            this.StartPosition = FormStartPosition.CenterScreen;//画面中央表示
            this.BackColor = Color.FromArgb(255, 255, 255);//背景色
        }

    }
   
}
