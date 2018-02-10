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
using Common;

namespace CSystem.TeaFuncUI
{
    public partial class PasswordDialog : ExitConfirmForm
    {
        private Teacher teacher;
        private Student student;
        private bool newPwdMode = false;

        public PasswordDialog(Teacher user)
        {
            InitializeComponent();
            newPwdMode = true;
            teacher = user ?? throw new ArgumentNullException(nameof(user));
        }
        public PasswordDialog(Student user)
        {
            InitializeComponent();
            newPwdMode = false;
            student = user ?? throw new ArgumentNullException(nameof(user));
        }

        private bool ValidateInfo()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("旧密码不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("新密码不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("旧密码输入不一致，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void TeaPwd()
        {
            if (!ValidateInfo())
                return;
            if(Convert.ToInt32(LoginManager.CheckTeaPwd(teacher, textBox1.Text)) >0 )
            {
                if (LoginManager.UpdateTeaPwd(teacher, textBox3.Text))
                {
                    MessageBox.Show(
                        "成功修改密码！",
                        "成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    NeedConfirmOnExit = false;
                    Close();
                }
                else
                {
                    MessageBox.Show("修改失败,请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("旧密码输入错误,请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void StuPwd()
        {
            if (!ValidateInfo())
                return;
            if (Convert.ToInt32(LoginManager.CheckStuPwd(student, textBox1.Text)) > 0)
            {
                if (LoginManager.UpdateStuPwd(student, textBox3.Text))
                {
                    MessageBox.Show(
                        "成功修改密码！",
                        "成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    NeedConfirmOnExit = false;
                    Close();
                }
                else
                {
                    MessageBox.Show("修改失败,请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("旧密码输入错误,请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (newPwdMode)
                TeaPwd();
            else
                StuPwd();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
