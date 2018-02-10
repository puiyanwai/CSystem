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
using Common;

namespace CSystem
{
    public partial class TeacherRegisterForm : ExitConfirmForm
    {

        public TeacherRegisterForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示注册窗口
        /// </summary>
        /// <returns>注册情况</returns>
        public static DialogResult ShowRegisterDialog()
        {
            using (var form = new TeacherRegisterForm())
                return form.ShowDialog();
        }

        private bool ValidateInfo()
        {
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("请填写密码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("请填写姓名", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(collegeTextBox.Text))
            {
                MessageBox.Show("请填写部门", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!maleRadioButton.Checked && !femaleRadioButton.Checked)
            {
                MessageBox.Show("请填写性别", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Utilities.IsVaildPhoneNumber(phoneTextBox.Text))
            {
                MessageBox.Show("请填写合法手机号码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInfo())
                return;

            int id = LoginManager.TeaRegister(
                nameTextBox.Text,
                maleRadioButton.Checked ? SexType.Male : SexType.Female,
                collegeTextBox.Text,
                phoneTextBox.Text,
                passwordTextBox.Text
                );

            if (id > 0)
            {
                MessageBox.Show($"请牢记工号: {Utilities.DbIdConvertToTeaId(id)}",
                    "注册成功!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                NeedConfirmOnExit = false;
                Close();
            }
            else
            {
                MessageBox.Show("注册失败,请联系系统管理员", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void canccelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
