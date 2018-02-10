using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSystem.TeaFuncUI
{
    public partial class DisplayPicture : Form
    {
        public DisplayPicture()
        {
            InitializeComponent();

            Image img = Image.FromFile(@"D:\masterpiece\masterpiece\1.jpg");
            pictureBox1.Image = img;
            
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog file1 = new SaveFileDialog();
            file1.Filter = "*.jpg";
            if(file1.ShowDialog() == DialogResult.OK)
            {
                //img.Save();
            }
        }
    }
}
