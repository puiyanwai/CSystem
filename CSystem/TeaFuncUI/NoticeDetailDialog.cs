using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemBLL;
using Model;

namespace CSystem.TeaFuncUI
{
    public partial class NoticeDetailDialog : ExitConfirmForm
    {
        public NoticeDetailDialog(int id,string title,string info,string time)
        {
            InitializeComponent();

            label7.Text = Convert.ToString(id) ?? string.Empty;
            label2.Text = title ?? string.Empty;
            richTextBox1.Text = info ?? string.Empty;
            label5.Text = time ?? string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
