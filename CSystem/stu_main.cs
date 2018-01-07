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
    public partial class stu_main : Form
    {
        public stu_main()
        {
            InitializeComponent();
        }

        //查看课表按钮，点击后调用stu_check类的有参构造函数初始化一个学生查看课表窗体对象并显示
        private void button2_Click(object sender, EventArgs e)
        {
            //stu_check stucheck = new stu_check(UserHelper.userName);
            //stucheck.Show();
        }

        //查看成绩按钮，点击后调用stu_check类的有参构造函数初始化一个学生查看成绩窗体对象并显示
        private void button4_Click(object sender, EventArgs e)
        {
            //stu_gra stugra = new stu_gra(UserHelper.userName);
            //stugra.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
