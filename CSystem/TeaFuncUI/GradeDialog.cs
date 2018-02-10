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
    public partial class GradeDialog : ExitConfirmForm
    {
        private Teacher teacher;

        public GradeDialog(Teacher user, int classId, int studentId, string name, float usualgra, float finalgra, float totalgra)
        {
            InitializeComponent();
            teacher = user ?? throw new ArgumentNullException(nameof(user));
            label12.Text = Convert.ToString(classId);
            label13.Text = Convert.ToString(studentId);
            label14.Text = name ?? string.Empty;
            textBox3.Text = Convert.ToString(usualgra);
            textBox4.Text = Convert.ToString(finalgra);
            label8.Text = Convert.ToString(totalgra) ?? "0.00";

            //classId = Convert.ToInt32(label12.Text.Trim());
            DataTable t = GradeManager.ReturnProportion(classId);
            label10.Text = t.Rows[0][0].ToString();
        }

        //添加成绩
        private void AddGrade()
        {
            if (textBox3.Text.Trim() == string.Empty && textBox4.Text.Trim() == string.Empty)
            {
                label8.Text = "0.00";
            }
            else label8.Text = (double.Parse(textBox3.Text) * double.Parse(label10.Text) + double.Parse(textBox4.Text) * (1- double.Parse(label10.Text))).ToString();
            if (GradeManager.UpdateClass(
                Convert.ToInt32(label13.Text.Trim()),
                Convert.ToInt32(label12.Text.Trim()),
                Convert.ToDouble(textBox3.Text.Trim()),
                Convert.ToDouble(textBox4.Text.Trim()),
                Convert.ToDouble(label8.Text.Trim())
                ))
            {
                MessageBox.Show(
                    "成功添加/修改成绩",
                    "成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                NeedConfirmOnExit = false;
                Close();
            }
            else
            {
                MessageBox.Show("添加/修改失败,请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddGrade();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
