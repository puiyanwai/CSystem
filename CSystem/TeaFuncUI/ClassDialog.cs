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
    public partial class ClassDialog : ExitConfirmForm
    {
        private Teacher teacher;
        private bool newClassMode = false;
        private int classId = -1;

        /// <summary>
        /// 新增课程模式
        /// </summary>
        /// <param name="user"></param>
        public ClassDialog(Teacher user)
        {
            InitializeComponent();
            newClassMode = true;
            teacher = user ?? throw new ArgumentNullException(nameof(user));
        }

        /// <summary>
        /// 修改课程模式
        /// </summary>
        /// <param name="user"></param>
        /// <param name="clsid"></param>
        /// <param name="name"></param>
        /// <param name="cat"></param>
        /// <param name="time"></param>
        /// <param name="place"></param>
        /// <param name="cap"></param>
        /// <param name="up"></param>

        public ClassDialog(Teacher user, int clsid, string name, string cat, string time, string place, int cap, float up)
        {
            InitializeComponent();
            newClassMode = false;
            classId = clsid;

            teacher = user ?? throw new ArgumentNullException(nameof(user));
            nameTextBox.Text = name ?? string.Empty;
            categoryTextBox.Text = cat ?? string.Empty;
            timeTextBox.Text = time ?? string.Empty;
            placeTextBox.Text = place ?? string.Empty;
            capabilityNumericUpDown.Value = cap;
            usualProNumericUpDown.Value = (decimal)up;
        }

        private void updateClass()
        {
            if (CourseManager.ModifyCourse(teacher,
                classId,
                nameTextBox.Text,
                categoryTextBox.Text,
                timeTextBox.Text,
                placeTextBox.Text,
                (int)capabilityNumericUpDown.Value,
                (float)usualProNumericUpDown.Value))
            {
                MessageBox.Show(
                    "成功修改课程",
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

        private void addNewClass()
        {
            if (CourseManager.InsertCourse(teacher,
                nameTextBox.Text,
                categoryTextBox.Text,
                timeTextBox.Text,
                placeTextBox.Text,
                (int)capabilityNumericUpDown.Value,
                (float)usualProNumericUpDown.Value))
            {
                MessageBox.Show(
                    "成功添加课程",
                    "成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                NeedConfirmOnExit = false;
                Close();
            }
            else
            {
                MessageBox.Show("添加失败,请重新添加！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (newClassMode)
                addNewClass();
            else
                updateClass();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
