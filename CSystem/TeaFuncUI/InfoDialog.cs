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
    public partial class InfoDialog : ExitConfirmForm
    {
        private Teacher teacher;
        private Student student;
        private bool newInfoMode = false;

        //合法性监测
        private bool ValidateInfo()
        {
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

        //教师修改个人信息
        public InfoDialog(Teacher user, string name,int id, SexType sex, string brand, string phone)
        {
            InitializeComponent();
            newInfoMode = true;
            teacher = user ?? throw new ArgumentNullException(nameof(user));
            nameTextBox.Text = name ?? string.Empty;
            idTextBox.Text = Convert.ToString(id);
            collegeTextBox.Text = brand ?? string.Empty;
            phoneTextBox.Text = phone ?? string.Empty;

            if (sex == SexType.Male) maleRadioButton.Checked = true;
            else if (sex == SexType.Female) femaleRadioButton.Checked = true;
        }

        //学生修改个人信息
        public InfoDialog(Student user, string name, int id, SexType sex, string college, string phone)
        {
            InitializeComponent();
            newInfoMode = false;
            student = user ?? throw new ArgumentNullException(nameof(user));
            nameTextBox.Text = name ?? string.Empty;
            idTextBox.Text = Convert.ToString(id);
            collegeTextBox.Text = college ?? string.Empty;
            phoneTextBox.Text = phone ?? string.Empty;

            if (sex == SexType.Male) maleRadioButton.Checked = true;
            else if (sex == SexType.Female) femaleRadioButton.Checked = true;
        }

        //更新教师个人信息
        private void UpdateTeaInfo()
        {
            if (!ValidateInfo())
                return;
            if (LoginManager.UpdateTeaInfo(teacher,
                nameTextBox.Text,
                maleRadioButton.Checked ? SexType.Male : SexType.Female,
                collegeTextBox.Text,
                phoneTextBox.Text
                ))
            {
                MessageBox.Show(
                    "成功修改个人信息",
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


        //更新学生个人信息
        private void UpdateStuInfo()
        {
            if (!ValidateInfo())
                return;
            if (LoginManager.UpdateStuInfo(student,
                nameTextBox.Text,
                maleRadioButton.Checked ? SexType.Male : SexType.Female,
                collegeTextBox.Text,
                phoneTextBox.Text
                ))
            {
                MessageBox.Show(
                    "成功修改个人信息",
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (newInfoMode)
                UpdateTeaInfo();
            else
                UpdateStuInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
