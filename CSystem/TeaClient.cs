using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace CSystem
{
    public partial class TeaClient : Form
    {
        private Teacher teacher;

        public TeaClient(Teacher user)
        {
            teacher = user ?? throw new ArgumentNullException(nameof(user));

            InitializeComponent();

            Customize();
        }

        private void Customize()
        {
            Text = $"欢迎{teacher.Name}!(工号{teacher.Id})";
        }
    }
}
