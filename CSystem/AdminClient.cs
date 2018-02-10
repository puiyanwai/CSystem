using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSystem.TeaFuncUI;
using Model;
using SystemBLL;
using Common;

namespace CSystem
{
    public partial class AdminClient : Form
    {
        private Teacher teacher;

        public AdminClient(Teacher user)
        {
            InitializeComponent();
            teacher = user ?? throw new ArgumentNullException(nameof(user));
            Customize();
            Displayinfo();
            Initialize();
        }

        private void Customize()
        {
            Text = $"欢迎{teacher.Name}!(工号{teacher.Id})";
        }

        private void Displayinfo()
        {
            DataTable dt = LoginManager.DisplayTeaInfo(teacher.Id);
            label5.Text = dt.Rows[0][0].ToString();  //name
            label6.Text = Convert.ToString(teacher.Id);  //id
            label8.Text = dt.Rows[0][3].ToString();  //brand
            label10.Text = dt.Rows[0][4].ToString();  //phone

            if (dt.Rows[0][2].ToString() == "0") label7.Text = "男";
            else label7.Text = "女";
        }

        private void Initialize()
        {
            var table = new DataTable();
            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID"),
                new DataColumn("名称"),
                new DataColumn("类别"),
                new DataColumn("时间"),
                new DataColumn("地点"),
                new DataColumn("人数"),
                new DataColumn("平时成绩比重")
            });
            classDataGridView.DataSource = table;
            classDataGridView.SelectionChanged += ClassDataGridView_SelectionChanged;
        }

        private void ClassDataGridView_SelectionChanged(object sender, EventArgs e)
        {
        }

        //更新dataGridview
        private void RefreshClassTable()
        {
            var taa = AdminManager.DisplayTeaCls(teacher.Id);
            taa.Columns[0].ColumnName = "编号";
            taa.Columns[1].ColumnName = "名称";
            taa.Columns[2].ColumnName = "任教教师";
            taa.Columns[3].ColumnName = "大类";
            taa.Columns[4].ColumnName = "时间";
            taa.Columns[5].ColumnName = "地点";
            taa.Columns[6].ColumnName = "容量";
            taa.Columns[7].ColumnName = "平时成绩比重";
            classDataGridView.DataSource = taa;

            //Display all students dataGridView ColumnName
            var dt = GradeManager.AllStudent();
            dt.Columns[0].ColumnName = "学号";
            dt.Columns[1].ColumnName = "姓名";
            dt.Columns[2].ColumnName = "性别";
            dt.Columns[3].ColumnName = "学院";
            dataGridView2.DataSource = dt;

            var dt1 = AdminManager.DisplayTea(teacher.Id);
            dt1.Columns[0].ColumnName = "工号";
            dt1.Columns[1].ColumnName = "姓名";
            dt1.Columns[2].ColumnName = "性别";
            dt1.Columns[3].ColumnName = "部门";
            dataGridView8.DataSource = dt1;

            toolStripComboBox1.Items.Clear();
            toolStripComboBox3.Items.Clear();
            toolStripComboBox4.Items.Clear();
            DataTable tab = AdminManager.DisplayCombobox(teacher.Id);
            int n = (int)AdminManager.ReturnTeaNum(teacher.Id);
            for (int i = 0; i < n; i++)
            {
                toolStripComboBox1.Items.Add(tab.Rows[i][0].ToString());
                toolStripComboBox3.Items.Add(tab.Rows[i][0].ToString());
                toolStripComboBox4.Items.Add(tab.Rows[i][0].ToString());
            }

            var d = SignManager.DisplaySignUp(teacher.Id);
            d.Columns[0].ColumnName = "签到编号";
            d.Columns[1].ColumnName = "学生学号";
            d.Columns[2].ColumnName = "学生姓名";
            d.Columns[3].ColumnName = "课程编号";
            d.Columns[4].ColumnName = "IP地址";
            d.Columns[5].ColumnName = "签到时间";
            d.Columns[6].ColumnName = "签到状态";
            dataGridView3.DataSource = d;

            var t = GradeManager.DisplayAllCourse(teacher.Id);
            t.Columns[0].ColumnName = "学号";
            t.Columns[1].ColumnName = "姓名";
            t.Columns[2].ColumnName = "学院";
            t.Columns[3].ColumnName = "课程编号";
            t.Columns[4].ColumnName = "平时成绩";
            t.Columns[5].ColumnName = "期末成绩";
            t.Columns[6].ColumnName = "总成绩";
            dataGridView1.DataSource = t;

            var ta = MessageManager.DisplayNotice();
            ta.Columns[0].ColumnName = "编号";
            ta.Columns[1].ColumnName = "标题";
            ta.Columns[2].ColumnName = "发布教师";
            ta.Columns[3].ColumnName = "时间";
            dataGridView5.DataSource = ta;
            dataGridView4.DataSource = ta;

            var tabb = MessageManager.DisplayTeaSend(teacher);
            tabb.Columns[0].ColumnName = "编号";
            tabb.Columns[1].ColumnName = "学生ID";
            tabb.Columns[2].ColumnName = "留言内容";
            tabb.Columns[3].ColumnName = "时间";
            dataGridView6.DataSource = tabb;

            var tta = MessageManager.DisplayTeaRece(teacher);
            tta.Columns[0].ColumnName = "编号";
            tta.Columns[1].ColumnName = "学生ID";
            tta.Columns[2].ColumnName = "留言内容";
            tta.Columns[3].ColumnName = "时间";
            dataGridView7.DataSource = tta;
        }

        //查询学生名单
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string option = "";
            if (toolStripComboBox2.Text.Trim() == "姓名")
            {
                option = "WHERE name like '%" + toolStripTextBox1.Text.Trim() + "%'";
            }
            else if (toolStripComboBox2.Text.Trim() == "性别")
            {
                option = "WHERE sex like '%" + toolStripTextBox1.Text.Trim() + "%'";
            }
            else if (toolStripComboBox2.Text.Trim() == "学院")
            {
                option = "WHERE college like '%" + toolStripTextBox1.Text.Trim() + "%'";
            }
            else { option = ""; }

            var dt = GradeManager.SelectStudent(option);
            dt.Columns[0].ColumnName = "学号";
            dt.Columns[1].ColumnName = "姓名";
            dt.Columns[2].ColumnName = "性别";
            dt.Columns[3].ColumnName = "学院";
            dataGridView2.DataSource = dt;
        }

        //修改个人信息
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            using (InfoDialog modifyDialog = new InfoDialog(teacher,
                label5.Text,
                Convert.ToInt32(label6.Text),
                teacher.Sex,
                label8.Text,
                label10.Text))
            modifyDialog.ShowDialog();
            Displayinfo();
        }

        //修改密码
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            using (PasswordDialog pwdDialog = new PasswordDialog(teacher))
                pwdDialog.ShowDialog();
        }

        //查看校历
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            DisplayPicture pi = new DisplayPicture();
            pi.ShowDialog();
        }

        //切换用户
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要切换用户吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            UserLogin Log = new UserLogin();
            Log.ShowDialog();
            Close();
        }

        //安全退出
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            Close();
        }

        //查询combobox内的教师的负责课程
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            string option = "";
            if (toolStripComboBox1.Text.Trim() != "")
            {
                option = " WHERE " + Convert.ToInt32(toolStripComboBox1.Text.Trim()) + " = teacherId ";
                DataTable dt = AdminManager.SelectTeaCls(option);
                dt.Columns[0].ColumnName = "编号";
                dt.Columns[1].ColumnName = "名称";
                dt.Columns[2].ColumnName = "任课教师";
                dt.Columns[3].ColumnName = "大类";
                dt.Columns[4].ColumnName = "时间";
                dt.Columns[5].ColumnName = "地点";
                dt.Columns[6].ColumnName = "容量";
                dt.Columns[7].ColumnName = "平时成绩比重";
                classDataGridView.DataSource = dt;
            }
            else RefreshClassTable();

        }

        //新增课程
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择教师",
                      "错误",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
            }
            else
            {
                using (ClassDialog addClassDialog = new ClassDialog(teacher))
                    addClassDialog.ShowDialog();
                RefreshClassTable();
            }
        }

        //删除课程
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int Cla;
            try
            {
                Cla = Convert.ToInt32(classDataGridView.CurrentRow.Cells[0].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (MessageBox.Show("确定要删除吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if ((int)CourseManager.CheckDelete(Cla) == 0)
            {
                if (CourseManager.DeleteCourse(Cla))
                {
                    MessageBox.Show(
                        "删除成功！",
                        "成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    RefreshClassTable();
                }
                else MessageBox.Show("删除失败！请重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("该课程已有学生选择，请删除选课记录后重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //查询不同教师负责课程的学生成绩
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string option = "";
            if (toolStripComboBox3.Text.Trim() != "")
            {
                option = " AND " + Convert.ToInt32(toolStripComboBox3.Text.Trim()) + " = teacherId ";
                DataTable dt = AdminManager.DisplayAllStuGra(option);
                dt.Columns[0].ColumnName = "课程编号";
                dt.Columns[1].ColumnName = "学生ID";
                dt.Columns[2].ColumnName = "学生姓名";
                dt.Columns[3].ColumnName = "学院";
                dt.Columns[4].ColumnName = "平时成绩";
                dt.Columns[5].ColumnName = "期末成绩";
                dt.Columns[6].ColumnName = "总成绩";
                dataGridView1.DataSource = dt;
            }
            else RefreshClassTable();

        }

        //增加修改成绩
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;
            var r = dataGridView1.CurrentRow.Cells;
            using (GradeDialog addgrade = new GradeDialog(teacher,
                Convert.ToInt32(r[3].Value),
                Convert.ToInt32(r[0].Value),
                r[1].Value as string,
                Convert.ToSingle(r[4].Value as string),
                Convert.ToSingle(r[5].Value as string),
                Convert.ToSingle(r[6].Value as string)
                ))
                addgrade.ShowDialog();
            string option = "";
            if (toolStripComboBox3.Text.Trim() != "")
            {
                option = " AND " + Convert.ToInt32(toolStripComboBox3.Text.Trim()) + " = teacherId ";
                DataTable dt = AdminManager.DisplayAllStuGra(option);
                dt.Columns[0].ColumnName = "课程编号";
                dt.Columns[1].ColumnName = "学生ID";
                dt.Columns[2].ColumnName = "学生姓名";
                dt.Columns[3].ColumnName = "学院";
                dt.Columns[4].ColumnName = "平时成绩";
                dt.Columns[5].ColumnName = "期末成绩";
                dt.Columns[6].ColumnName = "总成绩";
                dataGridView1.DataSource = dt;
            }
            else RefreshClassTable();
        }
    }
    
}
