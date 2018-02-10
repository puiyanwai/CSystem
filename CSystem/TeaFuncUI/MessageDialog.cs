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
    public partial class MessageDialog : ExitConfirmForm
    {
        private Teacher teacher;
        private Student student;
        private bool Mode = false;  //教师是true  学生是

        //教师端看是否有与该教师留言过的学生（没有留言过）
        public MessageDialog(Teacher user, int studentId, string name)
        {
            InitializeComponent();
            Mode = true;
            teacher = user ?? throw new ArgumentNullException(nameof(user));
            label2.Text = Convert.ToString(studentId);
            label4.Text = name ?? string.Empty;
            richTextBox1.Text = "此前没有留言记录，若有记录会显示最新的一条留言记录。";
            label7.Text = string.Empty;
            label10.Text = DateTime.Now.ToString();
        }

        //留言过
        public MessageDialog(Teacher user,int studentId,string name,string info,string time)
        {
            InitializeComponent();
            Mode = true;
            teacher = user ?? throw new ArgumentNullException(nameof(user));
            label2.Text = Convert.ToString(studentId);
            label4.Text = name ?? string.Empty;
            richTextBox1.Text = info ?? string.Empty;
            label7.Text = time ?? string.Empty;
            label10.Text = DateTime.Now.ToString();
        }

        //学生端看是否有与该学生留言过的教师（没有留言过）
        public MessageDialog(Student user, int teacherId, string name)
        {
            InitializeComponent();
            Mode = false;
            student = user ?? throw new ArgumentNullException(nameof(user));
            label2.Text = Convert.ToString(teacherId);
            label4.Text = name ?? string.Empty;
            richTextBox1.Text = "此前没有留言记录，若有记录会显示最新的一条留言记录。";
            label7.Text = string.Empty;
            label10.Text = DateTime.Now.ToString();
        }

        //留言过
        public MessageDialog(Student user,int teacherId,string name,string info,string time)
        {
            InitializeComponent();
            Mode = false;
            student = user ?? throw new ArgumentNullException(nameof(user));
            label2.Text = Convert.ToString(teacherId);
            label4.Text = name ?? string.Empty;
            richTextBox1.Text = info ?? string.Empty;
            label7.Text = time ?? string.Empty;
            label10.Text = DateTime.Now.ToString();
        }

        //教师给学生留言
        private void ToStu()
        {
            int type = 1;
            if (MessageManager.AddMessage(teacher, Convert.ToInt32(label2.Text), type, label10.Text, richTextBox2.Text))
            {
                MessageBox.Show(
                    "留言成功！",
                    "成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                NeedConfirmOnExit = false;
                Close();
            }
            else
            {
                MessageBox.Show("留言失败,请重新留言！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //学生给教师留言
        private void ToTea()
        {
            int type = 0;
            if (MessageManager.AddMessage(student, Convert.ToInt32(label2.Text), type, label10.Text, richTextBox2.Text))
            {
                MessageBox.Show(
                    "留言成功！",
                    "成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                NeedConfirmOnExit = false;
                Close();
            }
            else
            {
                MessageBox.Show("留言失败,请重新留言！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //生成留言
        private void button1_Click(object sender, EventArgs e)
        {
            if (Mode)
                ToStu();
            else ToTea();
        }
    }
}
