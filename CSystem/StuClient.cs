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
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using CSystem.TeaFuncUI;

namespace CSystem
{
    public partial class StuClient : Form
    {

        private Student student;

        public StuClient(Student user)
        {
            student = user ?? throw new ArgumentNullException(nameof(user));

            InitializeComponent();
            Initialize();
            Customize();
            Fill();
            DisplayInfo();
        }

        private void Customize()
        {
            Text = $"欢迎{student.Name}!(学号{student.Id})";
        }

        //获取登录、签到的IP地址
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        //显示个人信息
        private void DisplayInfo()
        {
            DataTable dt = LoginManager.DisplayStuInfo(student.Id);
            label5.Text = dt.Rows[0][0].ToString();  //name
            label6.Text = Convert.ToString(student.Id);  //id
            label8.Text = dt.Rows[0][3].ToString();  //college
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
                new DataColumn("任教教师"),
                new DataColumn("类别"),
                new DataColumn("时间"),
                new DataColumn("地点"),
                new DataColumn("人数"),
                new DataColumn("平时成绩比重")
            });
            dataGridView1.DataSource = table;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        //更新dataGridView
        public void Fill()
        {
            //Display all classes dataGridView ColumnName
            var d = CourseManager.AllCourse();
            d.Columns[0].ColumnName = "课程ID";
            d.Columns[1].ColumnName = "课程名称";
            d.Columns[2].ColumnName = "任课教师";
            d.Columns[3].ColumnName = "任课教师ID";
            d.Columns[4].ColumnName = "大类";
            d.Columns[5].ColumnName = "上课时间";
            d.Columns[6].ColumnName = "上课地点";
            d.Columns[7].ColumnName = "课程容量";
            d.Columns[8].ColumnName = "平时成绩比重";
            dataGridView1.DataSource = d;

            //Display selected classes dataGridView ColumnName
            RefreshSelectedClasses();

            //Display students' grades in dataGridView
            var tab = GradeManager.DisplayStudentGrade(student.Id);
            tab.Columns[0].ColumnName = "课程ID";
            tab.Columns[1].ColumnName = "课程名称";
            tab.Columns[2].ColumnName = "任课教师";
            tab.Columns[3].ColumnName = "平时成绩";
            tab.Columns[4].ColumnName = "期末成绩";
            tab.Columns[5].ColumnName = "总成绩";
            dataGridView3.DataSource = tab;

            var table = SignManager.DisplaySignList(student.Id);
            table.Columns[0].ColumnName = "课程ID";
            table.Columns[1].ColumnName = "课程名称";
            table.Columns[2].ColumnName = "任课教师ID";
            table.Columns[3].ColumnName = "任课教师";
            table.Columns[4].ColumnName = "大类";
            table.Columns[5].ColumnName = "上课时间";
            table.Columns[6].ColumnName = "上课地点";
            dataGridView4.DataSource = table;

            var Tab = SignManager.IPaddress(student.Id);
            Tab.Columns[0].ColumnName = "编号";
            Tab.Columns[1].ColumnName = "课程编号";
            Tab.Columns[2].ColumnName = "IP地址";
            Tab.Columns[3].ColumnName = "签到时间";
            Tab.Columns[4].ColumnName = "签到状态";
            dataGridView5.DataSource = Tab;

            var ta = MessageManager.DisplayTea();
            ta.Columns[0].ColumnName = "ID";
            ta.Columns[1].ColumnName = "姓名";
            ta.Columns[2].ColumnName = "性别";
            ta.Columns[3].ColumnName = "部门";
            dataGridView10.DataSource = ta;

            var datat = MessageManager.DisplayNotice();
            datat.Columns[0].ColumnName = "编号";
            datat.Columns[1].ColumnName = "标题";
            datat.Columns[2].ColumnName = "发布教师";
            datat.Columns[3].ColumnName = "发布时间";
            dataGridView8.DataSource = datat;
            dataGridView6.DataSource = datat;

            var tabb = MessageManager.DisplayStuSend(student);
            tabb.Columns[0].ColumnName = "编号";
            tabb.Columns[1].ColumnName = "教师ID";
            tabb.Columns[2].ColumnName = "留言内容";
            tabb.Columns[3].ColumnName = "时间";
            dataGridView9.DataSource = tabb;

            var tta = MessageManager.DisplayStuRece(student);
            tta.Columns[0].ColumnName = "编号";
            tta.Columns[1].ColumnName = "教师ID";
            tta.Columns[2].ColumnName = "留言内容";
            tta.Columns[3].ColumnName = "时间";
            dataGridView7.DataSource = tta;
        }

        //显示课表
        private void RefreshSelectedClasses()
        {
            var t = CourseManager.DisplayCourse(student.Id);
            t.Columns[0].ColumnName = "课程ID";
            t.Columns[1].ColumnName = "课程名称";
            t.Columns[2].ColumnName = "任课教师";
            t.Columns[3].ColumnName = "大类";
            t.Columns[4].ColumnName = "上课时间";
            t.Columns[5].ColumnName = "上课地点";
            t.Columns[6].ColumnName = "课程容量";
            t.Columns[7].ColumnName = "平时成绩比重";
            dataGridView2.DataSource = t;

            var drc = t.Rows.Cast<DataRow>();
            var cc = drc.Select(row => new Class()
            {
                Id = (int)row.ItemArray[0],
                Name = row.ItemArray[1] as string,
                TeacherName = row.ItemArray[2] as string,
                Category = row.ItemArray[3] as string,
                Time = row.ItemArray[4] as string,
                Place = row.ItemArray[5] as string,
                Capacity = (int)row.ItemArray[6],
                UsualProportion = Convert.ToSingle(row.ItemArray[7])
            });

            var data = new object[5][] { new object[8], new object[8], new object[8], new object[8], new object[8] };
            char[] day = { '天', '一', '二', '三', '四', '五', '六' };
            foreach (var group in cc.GroupBy(c => c.Time[3]).OrderBy(g => g.Key))
            {
                int rid = group.Key - '1';
                foreach (var cls in group)
                {
                    data[rid][1 + Array.IndexOf(day, cls.Time[2])] = $"{cls.Name}/{cls.TeacherName}/{cls.Place}";
                }
            }
            var table = new DataTable();
            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("时间"),
                new DataColumn("周日"),
                new DataColumn("周一"),
                new DataColumn("周二"),
                new DataColumn("周三"),
                new DataColumn("周四"),
                new DataColumn("周五"),
                new DataColumn("周六")
            });
            data[0][0] = "第一大节";
            data[1][0] = "第二大节";
            data[2][0] = "第三大节";
            data[3][0] = "第四大节";
            data[4][0] = "第五大节";
            for (int i = 0; i < 5; i++)
            {
                var row = table.NewRow();
                row.ItemArray = data[i];
                table.Rows.Add(row);
            }
            stuClassTableDataGrid.DataSource = table;
        }

        //选课合法性检查
        private bool ValidateInfo()
        {
            string time = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选择有效行", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value) <= 0)
            {
                MessageBox.Show("容量已满！请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if((int)CourseManager.CheckTime(student.Id, time) != 0)
            {
                MessageBox.Show("该课程与所选课程上课时间冲突，请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        //选课功能
        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateInfo())
                return;

            int cls = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            int tea = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
            string clsName = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);

            if (CourseManager.ChooseCourse(student, cls, tea, clsName))
            {
                int num = Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value) - 1;
                CourseManager.UpdateCourse(num, cls);

                MessageBox.Show(
                     "选课成功",
                     "成功",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                Fill();
            }
            else MessageBox.Show("选课失败！请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //退课功能
        private void button1_Click(object sender, EventArgs e)
        {
            int Cla;
            try
            {
                Cla = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
                Console.WriteLine(Cla);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (MessageBox.Show("确定要退课吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if ((int)SignManager.CheckSignUp(student.Id, Cla) == 0)
            {
                if(CourseManager.DropCourse(Cla))
                {
                    MessageBox.Show(
                        "退课成功！",
                        "成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    int num = Convert.ToInt32(dataGridView2.CurrentRow.Cells[7].Value) + 1;
                    CourseManager.UpdateCourse(num, Cla);
                    Fill();
                }
                else MessageBox.Show("该课程已有成绩，退课失败！请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("已有签到记录，退课失败！请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //筛选课程
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string option = "";
            if (toolStripComboBox3.Text.Trim() == "课程ID")
            {
                option = " AND Class.id = " + Convert.ToInt32(toolStripTextBox1.Text.Trim()) + " ";
            }
            else if (toolStripComboBox3.Text.Trim() == "课程名称")
            {
                option = " AND Class.name like '%" + toolStripTextBox1.Text.Trim() + "%' ";
            }
            else if (toolStripComboBox3.Text.Trim() == "任教教师")
            {
                option = " AND Teacher.name like '%" + toolStripTextBox1.Text.Trim() + "%' ";
            }
            else if (toolStripComboBox3.Text.Trim() == "大类")
            {
                option = " AND category like '%" + toolStripTextBox1.Text.Trim() + "%' ";
            }
            else if (toolStripComboBox3.Text.Trim() == "时间")
            {
                option = " AND time like '%" + toolStripTextBox1.Text.Trim() + "%' ";
            }
            else if (toolStripComboBox3.Text.Trim() == "地点")
            {
                option = " AND place like '%" + toolStripTextBox1.Text.Trim() + "%' ";
            }
            else { option = ""; }

            var dt = CourseManager.SearchClass(option);
            dt.Columns[0].ColumnName = "课程ID";
            dt.Columns[1].ColumnName = "课程名称";
            dt.Columns[2].ColumnName = "任课教师";
            dt.Columns[3].ColumnName = "任课教师ID";
            dt.Columns[4].ColumnName = "大类";
            dt.Columns[5].ColumnName = "上课时间";
            dt.Columns[6].ColumnName = "上课地点";
            dt.Columns[7].ColumnName = "课程容量";
            dt.Columns[8].ColumnName = "平时成绩比重";
            dataGridView1.DataSource = dt;
        }

        //退出
        private void button3_Click(object sender, EventArgs e)
        {
            UserLogin welcome = new UserLogin();
            welcome.ShowDialog();
            Close();
        }

        //签到功能
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow == null)
                return;
            int clsid = Convert.ToInt32(dataGridView4.CurrentRow.Cells[0].Value);
            string ip = GetLocalIPAddress();
            string time = DateTime.Now.ToString();
            string status = "成功";
            if ((int)SignManager.CheckSignUp(student.Id, clsid) == 0)
            {
                if(SignManager.SignUp(student,student.Name, clsid, ip, time, status))
                {
                    MessageBox.Show(
                        "签到成功！",
                        "成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    Fill();
                }
                else MessageBox.Show("签到失败！请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("该节课已签到！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //修改个人信息
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            using (InfoDialog modifyDialog = new InfoDialog(student,
                label5.Text,
                Convert.ToInt32(label6.Text),
                student.Sex,
                label8.Text,
                label10.Text))
                modifyDialog.ShowDialog();
            DisplayInfo();
        }

        //修改密码
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (PasswordDialog pwdDialog = new PasswordDialog(student))
                pwdDialog.ShowDialog();
        }

        //查看校历
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            DisplayPicture pi = new DisplayPicture();
            pi.ShowDialog();
        }

        //切换用户
        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要切换用户吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            Close();
            UserLogin Log = new UserLogin();
            Log.ShowDialog();
        }

        //退出
        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            Close();
        }

        //查询教师名单
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string option = "";
            if (toolStripComboBox2.Text.Trim() == "姓名")
            {
                option = " WHERE name like '%" + toolStripTextBox1.Text.Trim() + "%'";
            }
            else if (toolStripComboBox2.Text.Trim() == "性别")
            {
                option = " WHERE sex like '%" + toolStripTextBox1.Text.Trim() + "%'";
            }
            else if (toolStripComboBox2.Text.Trim() == "部门")
            {
                option = " WHERE brand like '%" + toolStripTextBox1.Text.Trim() + "%'";
            }
            else { option = ""; }

            var dt = MessageManager.SelectTea(option);
            dt.Columns[0].ColumnName = "学号";
            dt.Columns[1].ColumnName = "姓名";
            dt.Columns[2].ColumnName = "性别";
            dt.Columns[3].ColumnName = "部门";
            dataGridView2.DataSource = dt;
        }

        //查看通知详情
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView8.CurrentRow == null)
                return;
            var r = dataGridView8.CurrentRow.Cells;
            DataTable tab = MessageManager.ReturnBody(Convert.ToInt32(r[0].Value));
            using (NoticeDetailDialog detail = new NoticeDetailDialog(
                Convert.ToInt32(r[0].Value),
                r[1].Value as string,
                tab.Rows[0][0].ToString(),
                r[3].Value as string))
            detail.ShowDialog();
        }

        //学生留言
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (dataGridView10.CurrentRow == null)
                return;
            var r = dataGridView10.CurrentRow.Cells;
            if ((int)MessageManager.ReturnStuMes(student, Convert.ToInt32(r[0].Value)) == 0)
            {
                using (MessageDialog mess = new MessageDialog(student,
                    Convert.ToInt32(r[0].Value),
                    r[1].Value as string))
                mess.ShowDialog();
            }
            else
            {
                var dt = MessageManager.DisplayStuMes(student, Convert.ToInt32(r[0].Value));
                using (MessageDialog mess = new MessageDialog(student,
                    Convert.ToInt32(r[0].Value),
                    r[1].Value as string,
                    dt.Rows[0][3].ToString(),
                    dt.Rows[0][4].ToString()))
                mess.ShowDialog();
            }
            Fill();
        }

        //消息盒子回复留言
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            if (dataGridView7.CurrentRow == null)
                return;
            var r = dataGridView7.CurrentRow.Cells;
            var dt = MessageManager.DisplayStuMes(student, Convert.ToInt32(r[1].Value));   //get info,time
            var t = MessageManager.SelectedTea(Convert.ToInt32(r[1].Value));        //get teachers'name
            using (MessageDialog mess = new MessageDialog(student,
                Convert.ToInt32(r[1].Value),
                t.Rows[0][0].ToString(),
                dt.Rows[0][3].ToString(),
                dt.Rows[0][4].ToString()))
                mess.ShowDialog();
            Fill();
        }
    }
}
