using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSystem
{
    public class ExitConfirmForm : Form
    {
        protected bool NeedConfirmOnExit { set; get; }
        protected string ExitConfirmTitle { set; get; }
        protected string ExitConfirmText { set; get; }

        public ExitConfirmForm()
        {
            NeedConfirmOnExit = true;
            ExitConfirmTitle = "关闭窗口";
            ExitConfirmText = "确定关闭窗口?所有未提交更改将无法恢复!";
            FormClosing += ExitConfirmForm_FormClosing;
        }

        private void ExitConfirmForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (NeedConfirmOnExit &&
                MessageBox.Show(ExitConfirmText, ExitConfirmTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
