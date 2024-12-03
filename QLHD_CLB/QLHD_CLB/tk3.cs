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
    public partial class tk3 : Form
    {
        public tk3()
        {
            InitializeComponent();
        }

        private void tk3_Load(object sender, EventArgs e)
        {
            CrystalReport3 rpt = new CrystalReport3();
            crystalReportViewer3.ReportSource = rpt;
            rpt.SetDatabaseLogon("sa", "123", @"PHAMTHUAN\MSSQLSERVER01", "QuanLyCauLacBo");
            crystalReportViewer3.DisplayToolbar = false;
            crystalReportViewer3.DisplayStatusBar = false;
            crystalReportViewer3.Refresh();
            crystalReportViewer3.ShowGroupTreeButton = false;
            crystalReportViewer3.EnableDrillDown = false;
            crystalReportViewer3.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
        }
    }
}
