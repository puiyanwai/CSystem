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
    public partial class TeacherRegisterForm : Form
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

        private void registerButton_Click(object sender, EventArgs e)
        {

        }

        private void canccelButton_Click(object sender, EventArgs e)
        {
            UserLogin userlog = new UserLogin();
            userlog.Show();
        }
    }
}
