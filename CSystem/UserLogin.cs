using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSystem
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 验证信息是否完整
        /// </summary>
        private bool ValidateInfo()
        {
            if (authTypeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择身份",
                      "错误",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(userTextBox.Text))
            {
                MessageBox.Show("请输入账号",
                      "错误",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("请输入密码",
                      "错误",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInfo())
                return;

            string username = userTextBox.Text.Trim();
            string pwd = passwordTextBox.Text.Trim();

            SystemBLL.UserLogin stulog = new SystemBLL.UserLogin();
            SystemBLL.UserLogin tealog = new SystemBLL.UserLogin();

            switch (authTypeComboBox.SelectedIndex)
            {
                case 0:
                    if (stulog.StuLogin(username, pwd))
                    {
                        UserHelper.userName = userTextBox.Text.Trim();
                        UserHelper.password = passwordTextBox.Text.Trim();
                        this.Hide();
                        stu_main f = new stu_main();
                        f.Show();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误，请重新输入！", "错误");
                        userTextBox.Text = "";
                        passwordTextBox.Text = "";
                        userTextBox.Focus();
                    }
                    break;
                case 1:
                    if (tealog.TeaLogin(username, pwd))
                    {
                        UserHelper.userName = userTextBox.Text.Trim();
                        UserHelper.password = passwordTextBox.Text.Trim();
                        this.Hide();
                        tea_main f = new tea_main();
                        f.Show();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误，请重新输入！", "错误");
                        userTextBox.Text = "";
                        passwordTextBox.Text = "";
                        userTextBox.Focus();
                    }
                    break;
                default:
                    MessageBox.Show("请先选择身份",
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
            }
            // TODO : login
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            switch (authTypeComboBox.SelectedIndex)
            {
                case 0:
                    StudentRegisterForm.ShowRegisterDialog();
                    break;
                case 1:
                    TeacherRegisterForm.ShowRegisterDialog();
                    break;
                default:
                    MessageBox.Show("请先选择身份",
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
