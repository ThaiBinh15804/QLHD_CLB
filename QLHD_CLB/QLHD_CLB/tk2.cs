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
    public partial class tk2 : Form
    {
        public tk2()
        {
            InitializeComponent();
        }

        private void tk2_Load(object sender, EventArgs e)
        {
            CrystalReport2 rpt = new CrystalReport2();
            crystalReportViewer2.ReportSource = rpt;
            rpt.SetDatabaseLogon("sa", "123", @"PHAMTHUAN\MSSQLSERVER01", "QuanLyCauLacBo");
            crystalReportViewer2.DisplayToolbar = false;
            crystalReportViewer2.DisplayStatusBar = false;
            crystalReportViewer2.Refresh();
            crystalReportViewer2.ShowGroupTreeButton = false;
            crystalReportViewer2.EnableDrillDown = false;
            crystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
        }
    }
}
