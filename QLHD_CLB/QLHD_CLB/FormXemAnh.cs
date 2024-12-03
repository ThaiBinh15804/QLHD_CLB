using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHD_CLB
{
    public partial class FormXemAnh : Form
    {
        public FormXemAnh()
        {
            InitializeComponent();
        }

        public FormXemAnh(Image image)
        {
            InitializeComponent();

            guna2PictureBox1.Image = image;
        }

        private void FormXemAnh_Load(object sender, EventArgs e)
        {

        }
    }
}
