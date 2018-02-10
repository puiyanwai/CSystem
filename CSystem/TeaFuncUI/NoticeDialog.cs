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
    public partial class NoticeDialog : ExitConfirmForm
    {
        private Teacher teacher;
        private bool newNoticeMode = false;
        private int noticeId = -1;

        //发布通知
        public NoticeDialog(Teacher user)
        {
            InitializeComponent();
            newNoticeMode = true;
            teacher = user ?? throw new ArgumentNullException(nameof(user));
            label4.Text = DateTime.Now.ToString();
        }

        //修改通知
        public NoticeDialog(Teacher user, int id, string title, string info)
        {
            InitializeComponent();
            newNoticeMode = false;
            noticeId = id;
            teacher = user ?? throw new ArgumentNullException(nameof(user));

            textBox1.Text = title ?? string.Empty;
            richTextBox1.Text = info ?? string.Empty;
            label4.Text = DateTime.Now.ToString();
        }

        private void AddNotice()
        {
            if (MessageManager.AddNotice(teacher, label4.Text, textBox1.Text, richTextBox1.Text))
            {
                MessageBox.Show(
                    "成功发布通知",
                    "成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                NeedConfirmOnExit = false;
                Close();
            }
            else
            {
                MessageBox.Show("发布失败,请重新发布！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateNotice()
        {
            if (MessageManager.UpdateNotice(teacher, noticeId, label4.Text, textBox1.Text, label4.Text))
            {
                MessageBox.Show(
                    "成功修改通知",
                    "成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                NeedConfirmOnExit = false;
                Close();
            }
            else
            {
                MessageBox.Show("修改失败,请重新修改！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (newNoticeMode)
                AddNotice();
            else
                UpdateNotice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
