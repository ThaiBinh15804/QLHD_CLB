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
    public partial class tk1 : Form
    {
        public tk1()
        {
            InitializeComponent();
        }

        private void tk1_Load(object sender, EventArgs e)
        {
            CrystalReport1 rpt = new CrystalReport1();
            crystalReportViewer1.ReportSource = rpt;
            rpt.SetDatabaseLogon("sa","123", @"PHAMTHUAN\MSSQLSERVER01", "QuanLyCauLacBo");
            crystalReportViewer1.DisplayToolbar = false;
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.ShowGroupTreeButton = false;
            crystalReportViewer1.EnableDrillDown = false;
            crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;

        }
    }
}
