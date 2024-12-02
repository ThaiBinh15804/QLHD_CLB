namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.Charts.WinForms.ChartFont chartFont1 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont2 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont3 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont4 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid1 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick1 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont5 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid2 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick2 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont6 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid3 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel1 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont7 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick3 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont8 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.LPoint lPoint1 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint2 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint3 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint4 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint5 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint6 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint7 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint8 = new Guna.Charts.WinForms.LPoint();
            this.gunaChart1 = new Guna.Charts.WinForms.GunaChart();
            this.gunaBarDataset1 = new Guna.Charts.WinForms.GunaBarDataset();
            this.gunaLineDataset1 = new Guna.Charts.WinForms.GunaLineDataset();
            this.SuspendLayout();
            // 
            // gunaChart1
            // 
            this.gunaChart1.Datasets.AddRange(new Guna.Charts.Interfaces.IGunaDataset[] {
            this.gunaBarDataset1,
            this.gunaLineDataset1});
            chartFont1.FontName = "Arial";
            this.gunaChart1.Legend.LabelFont = chartFont1;
            this.gunaChart1.Location = new System.Drawing.Point(94, 125);
            this.gunaChart1.Name = "gunaChart1";
            this.gunaChart1.Size = new System.Drawing.Size(580, 379);
            this.gunaChart1.TabIndex = 0;
            chartFont2.FontName = "Arial";
            chartFont2.Size = 12;
            chartFont2.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.gunaChart1.Title.Font = chartFont2;
            chartFont3.FontName = "Arial";
            this.gunaChart1.Tooltips.BodyFont = chartFont3;
            chartFont4.FontName = "Arial";
            chartFont4.Size = 9;
            chartFont4.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            this.gunaChart1.Tooltips.TitleFont = chartFont4;
            this.gunaChart1.XAxes.Display = false;
            this.gunaChart1.XAxes.GridLines = grid1;
            chartFont5.FontName = "Arial";
            tick1.Font = chartFont5;
            this.gunaChart1.XAxes.Ticks = tick1;
            this.gunaChart1.YAxes.Display = false;
            this.gunaChart1.YAxes.GridLines = grid2;
            chartFont6.FontName = "Arial";
            tick2.Font = chartFont6;
            this.gunaChart1.YAxes.Ticks = tick2;
            this.gunaChart1.ZAxes.GridLines = grid3;
            chartFont7.FontName = "Arial";
            pointLabel1.Font = chartFont7;
            this.gunaChart1.ZAxes.PointLabels = pointLabel1;
            chartFont8.FontName = "Arial";
            tick3.Font = chartFont8;
            this.gunaChart1.ZAxes.Ticks = tick3;
            // 
            // gunaBarDataset1
            // 
            lPoint1.Label = "Mua đồ 1";
            lPoint1.Y = 20D;
            lPoint2.Label = "Mua đồ 2";
            lPoint2.Y = 212D;
            lPoint3.Label = "Sop";
            lPoint3.Y = 20D;
            lPoint4.Label = "sss";
            lPoint4.Y = 100D;
            this.gunaBarDataset1.DataPoints.AddRange(new Guna.Charts.WinForms.LPoint[] {
            lPoint1,
            lPoint2,
            lPoint3,
            lPoint4});
            this.gunaBarDataset1.IndexLabel = "rêt";
            this.gunaBarDataset1.Label = "Bar1";
            this.gunaBarDataset1.TargetChart = this.gunaChart1;
            // 
            // gunaLineDataset1
            // 
            this.gunaLineDataset1.BorderColor = System.Drawing.Color.Empty;
            lPoint5.Label = "a";
            lPoint5.Y = 123D;
            lPoint6.Label = "r";
            lPoint6.Y = 234D;
            lPoint7.Label = "r";
            lPoint7.Y = 12D;
            lPoint8.Label = "e";
            lPoint8.Y = 216D;
            this.gunaLineDataset1.DataPoints.AddRange(new Guna.Charts.WinForms.LPoint[] {
            lPoint5,
            lPoint6,
            lPoint7,
            lPoint8});
            this.gunaLineDataset1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gunaLineDataset1.IndexLabel = "owww";
            this.gunaLineDataset1.Label = "Line1";
            this.gunaLineDataset1.TargetChart = this.gunaChart1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 611);
            this.Controls.Add(this.gunaChart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.Charts.WinForms.GunaChart gunaChart1;
        private Guna.Charts.WinForms.GunaBarDataset gunaBarDataset1;
        private Guna.Charts.WinForms.GunaLineDataset gunaLineDataset1;
    }
}

