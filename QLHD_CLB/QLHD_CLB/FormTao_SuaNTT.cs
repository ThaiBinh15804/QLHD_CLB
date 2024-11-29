﻿using System;
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
    public partial class FormTao_SuaNTT : Form
    {
        private FormGiaoDien parentForm;

        public FormTao_SuaNTT()
        {
            InitializeComponent();
        }

        public FormTao_SuaNTT(FormGiaoDien _parentForm)
        {
            InitializeComponent();
            parentForm = _parentForm;
        }

        private void FormTao_SuaNTT_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            parentForm.container(new FormNhaTaiTro(parentForm));
        }
    }
}
