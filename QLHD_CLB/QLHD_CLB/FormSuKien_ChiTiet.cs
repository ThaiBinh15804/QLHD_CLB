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
    public partial class FormSuKien_ChiTiet : Form
    {
        private FormGiaoDien parentForm;
        public FormSuKien_ChiTiet()
        {
            InitializeComponent();
        }

        public FormSuKien_ChiTiet(FormGiaoDien _parentForm)
        {
            InitializeComponent();
            parentForm = _parentForm;
        }

        private void FormSuKien_ChiTiet_Load(object sender, EventArgs e)
        {

        }
    }
}
