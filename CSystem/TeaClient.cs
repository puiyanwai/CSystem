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
    public partial class TeaClient : Form
    {
        private Teacher teacher;

        public TeaClient(Teacher user)
        {
            teacher = user ?? throw new ArgumentNullException(nameof(user));

            InitializeComponent();
            Initialize();
            Customize();
            RefreshClassTable();
            Displayinfo();
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
            var cls = CourseManager.GetAssociatedClasses(teacher);
            DataTable table = classDataGridView.DataSource as DataTable;
            table.Rows.Clear();
            foreach (var c in cls)
            {
                var r = table.NewRow();
                r.ItemArray = new object[] {
                    c.Id,
                    c.Name,  //=="SB"?"SB!":"!SB"
                    c.Category,
                    c.Time,
                    c.Place,
                    c.Capacity,   //<10?"<10":">=10"
                    c.UsualProportion
                };
                table.Rows.Add(r);
            }
            classDataGridView.DataSource = table;

            //Display all students dataGridView ColumnName
            var dt = GradeManager.AllStudent();
            dt.Columns[0].ColumnName = "学号";
            dt.Columns[1].ColumnName = "姓名";
            dt.Columns[2].ColumnName = "性别";
            dt.Columns[3].ColumnName = "学院";
            dataGridView2.DataSource = dt;

            toolStripComboBox3.Items.Clear();
            toolStripComboBox4.Items.Clear();
            DataTable tab = GradeManager.DisplayCombobox(teacher.Id);
            int n = (int)GradeManager.ReturnNum(teacher.Id);
            for (int i = 0; i < n; i++)
            {
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

        //添加课程
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (ClassDialog addClassDialog = new ClassDialog(teacher))
                addClassDialog.ShowDialog();
            RefreshClassTable();
        }

        //删除课程
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int Cla;
            try
            {
                Cla = Convert.ToInt32(classDataGridView.CurrentRow.Cells[0].Value);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (MessageBox.Show("确定要删除吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if ((int)CourseManager.CheckDelete(Cla) == 0)
            {
                if(CourseManager.DeleteCourse(Cla))
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

        //修改课程
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (classDataGridView.CurrentRow == null)
                return;
            var r = classDataGridView.CurrentRow.Cells;
            using (ClassDialog modifyDialog = new ClassDialog(teacher,
                Convert.ToInt32(r[0].Value),
                r[1].Value as string,
                r[2].Value as string,
                r[3].Value as string,
                r[4].Value as string,
                Convert.ToInt32(r[5].Value),
                Convert.ToSingle(r[6].Value)))
                modifyDialog.ShowDialog();
            RefreshClassTable();
        }

        //空
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        //筛选查询学生名单
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

        //筛选查询不同课程的不同学生
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string option = "";
            if (toolStripComboBox3.Text.Trim() != "")
            {
                option = " AND " + Convert.ToInt32(toolStripComboBox3.Text.Trim()) + " = classId ";
                DataTable dt = GradeManager.SelectDifferentGrade(teacher.Id, option);
                dt.Columns[0].ColumnName = "学号";
                dt.Columns[1].ColumnName = "姓名";
                dt.Columns[2].ColumnName = "学院";
                dt.Columns[3].ColumnName = "课程编号";
                dt.Columns[4].ColumnName = "平时成绩";
                dt.Columns[5].ColumnName = "期末成绩";
                dt.Columns[6].ColumnName = "总成绩";
                dataGridView1.DataSource = dt;
            }
            else RefreshClassTable();

        }

        //添加学生成绩
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
            if (toolStripComboBox3.Text.Trim() != "")
            {
                string option = " AND " + Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value) + " = classId ";
                DataTable t = GradeManager.SelectDifferentGrade(teacher.Id, option);
                t.Columns[0].ColumnName = "学号";
                t.Columns[1].ColumnName = "姓名";
                t.Columns[2].ColumnName = "学院";
                t.Columns[3].ColumnName = "课程编号";
                t.Columns[4].ColumnName = "平时成绩";
                t.Columns[5].ColumnName = "期末成绩";
                t.Columns[6].ColumnName = "总成绩";
                dataGridView1.DataSource = t;
            }
            else RefreshClassTable();
        }

        //筛选学生签到记录
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            string option = "";
            if (toolStripComboBox4.Text.Trim() != "")
            {
                option = " AND Sign.classId = " + Convert.ToInt32(toolStripComboBox4.Text.Trim()) +
                    "AND ChooseCls.classId = " + Convert.ToInt32(toolStripComboBox4.Text.Trim());
                DataTable dt = SignManager.DisplayDifferentSignUp(teacher.Id, option);
                dt.Columns[0].ColumnName = "签到编号";
                dt.Columns[1].ColumnName = "学生学号";
                dt.Columns[2].ColumnName = "学生姓名";
                dt.Columns[3].ColumnName = "IP地址";
                dt.Columns[4].ColumnName = "签到时间";
                dt.Columns[5].ColumnName = "签到状态";
                dataGridView3.DataSource = dt;
            }
            else RefreshClassTable();

        }

        //删除学生签到记录
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            int id;
            try
            {
                id = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (MessageBox.Show("确定要删除吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (SignManager.DeleteSignUpRecord(id))
            {
                MessageBox.Show("删除成功！");
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
            RefreshClassTable();
        }

        //更新教师个人信息
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

        //查看校历
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            DisplayPicture pi = new DisplayPicture();
            pi.ShowDialog();
        }

        //修改密码
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            using (PasswordDialog pwdDialog =new PasswordDialog(teacher))
                pwdDialog.ShowDialog();
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

        //发布通知
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            using (NoticeDialog addNoticeDialog = new NoticeDialog(teacher))
                addNoticeDialog.ShowDialog();
            RefreshClassTable();
        }

        //修改通知
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow == null)
                return;
            if(Convert.ToInt32(dataGridView5.CurrentRow.Cells[2].Value) != Utilities.TeaIdConvertToDbId(teacher.Id))
            {
                MessageBox.Show("不能修改其他教师发布的通知！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable tab = MessageManager.ReturnBody(Convert.ToInt32(dataGridView5.CurrentRow.Cells[0].Value));
                var r = dataGridView5.CurrentRow.Cells;
                using (NoticeDialog modifyDialog = new NoticeDialog(teacher,
                    Convert.ToInt32(r[0].Value),
                    r[1].Value as string,
                    tab.Rows[0][0].ToString()))
                    modifyDialog.ShowDialog();
                RefreshClassTable();
            }
        }

        //删除通知
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow == null)
                return;
            if (Convert.ToInt32(dataGridView5.CurrentRow.Cells[2].Value) != Utilities.TeaIdConvertToDbId(teacher.Id))
            {
                MessageBox.Show("不能删除其他教师发布的通知！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("确定要删除该通知吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                int id = Convert.ToInt32(dataGridView5.CurrentRow.Cells[0].Value);
                if (MessageManager.DeleteNotice(teacher,id))
                {
                    MessageBox.Show(
                        "删除成功！",
                        "成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    RefreshClassTable();
                }
                else MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //查看通知详情
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow == null)
                return;
            var r = dataGridView5.CurrentRow.Cells;
            DataTable tab = MessageManager.ReturnBody(Convert.ToInt32(r[0].Value));
            using (NoticeDetailDialog detail = new NoticeDetailDialog(
                Convert.ToInt32(r[0].Value),
                r[1].Value as string,
                tab.Rows[0][0].ToString(),
                r[3].Value as string))
            detail.ShowDialog();
        }

        //教师留言
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
                return;
            var r = dataGridView2.CurrentRow.Cells;
            if ((int)MessageManager.ReturnTeaMes(teacher, Convert.ToInt32(r[0].Value)) == 0)
            { 
                using (MessageDialog mess = new MessageDialog(teacher,
                    Convert.ToInt32(r[0].Value),
                    r[1].Value as string))
                 mess.ShowDialog();
            }
            else
            {
                var dt = MessageManager.DisplayTeaMes(teacher, Convert.ToInt32(r[0].Value));
                using (MessageDialog mess = new MessageDialog(teacher,
                    Convert.ToInt32(r[0].Value),
                    r[1].Value as string,
                    dt.Rows[0][3].ToString(),
                    dt.Rows[0][4].ToString()))
                mess.ShowDialog();
            }
            RefreshClassTable();
        }

        //消息盒子回复留言
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            if (dataGridView7.CurrentRow == null)
                return;
            var r = dataGridView7.CurrentRow.Cells;
            var dt = MessageManager.DisplayTeaMes(teacher, Convert.ToInt32(r[1].Value));   //get info,time
            var t = MessageManager.SelectedStu(Convert.ToInt32(r[1].Value));        //get students'name
            using (MessageDialog mess = new MessageDialog(teacher,
                Convert.ToInt32(r[1].Value),
                t.Rows[0][0].ToString(),
                dt.Rows[0][3].ToString(),
                dt.Rows[0][4].ToString()))
            mess.ShowDialog();
            RefreshClassTable();
        }
    }
}
