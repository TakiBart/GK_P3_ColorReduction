namespace GK_P3_ColorReduction
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.KBGB = new System.Windows.Forms.GroupBox();
            this.KBNUD = new System.Windows.Forms.NumericUpDown();
            this.KGGB = new System.Windows.Forms.GroupBox();
            this.KGNUD = new System.Windows.Forms.NumericUpDown();
            this.KRGB = new System.Windows.Forms.GroupBox();
            this.KRNUD = new System.Windows.Forms.NumericUpDown();
            this.LoadImageBtn = new System.Windows.Forms.Button();
            this.AlgorithmGB = new System.Windows.Forms.GroupBox();
            this.AlgorithmCB = new System.Windows.Forms.ComboBox();
            this.KGB = new System.Windows.Forms.GroupBox();
            this.KNUD = new System.Windows.Forms.NumericUpDown();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.WorkImagePB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.KBGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KBNUD)).BeginInit();
            this.KGGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KGNUD)).BeginInit();
            this.KRGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KRNUD)).BeginInit();
            this.AlgorithmGB.SuspendLayout();
            this.KGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkImagePB)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.KBGB);
            this.splitContainer1.Panel1.Controls.Add(this.KGGB);
            this.splitContainer1.Panel1.Controls.Add(this.KRGB);
            this.splitContainer1.Panel1.Controls.Add(this.LoadImageBtn);
            this.splitContainer1.Panel1.Controls.Add(this.AlgorithmGB);
            this.splitContainer1.Panel1.Controls.Add(this.KGB);
            this.splitContainer1.Panel1.Controls.Add(this.GenerateBtn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.WorkImagePB);
            this.splitContainer1.Size = new System.Drawing.Size(821, 493);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 0;
            // 
            // KBGB
            // 
            this.KBGB.Controls.Add(this.KBNUD);
            this.KBGB.Location = new System.Drawing.Point(100, 158);
            this.KBGB.Name = "KBGB";
            this.KBGB.Size = new System.Drawing.Size(41, 45);
            this.KBGB.TabIndex = 6;
            this.KBGB.TabStop = false;
            this.KBGB.Text = "Kb";
            // 
            // KBNUD
            // 
            this.KBNUD.AutoSize = true;
            this.KBNUD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KBNUD.Location = new System.Drawing.Point(3, 16);
            this.KBNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KBNUD.Name = "KBNUD";
            this.KBNUD.Size = new System.Drawing.Size(35, 20);
            this.KBNUD.TabIndex = 3;
            this.KBNUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // KGGB
            // 
            this.KGGB.Controls.Add(this.KGNUD);
            this.KGGB.Location = new System.Drawing.Point(56, 158);
            this.KGGB.Name = "KGGB";
            this.KGGB.Size = new System.Drawing.Size(41, 45);
            this.KGGB.TabIndex = 6;
            this.KGGB.TabStop = false;
            this.KGGB.Text = "Kg";
            // 
            // KGNUD
            // 
            this.KGNUD.AutoSize = true;
            this.KGNUD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KGNUD.Location = new System.Drawing.Point(3, 16);
            this.KGNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KGNUD.Name = "KGNUD";
            this.KGNUD.Size = new System.Drawing.Size(35, 20);
            this.KGNUD.TabIndex = 3;
            this.KGNUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // KRGB
            // 
            this.KRGB.Controls.Add(this.KRNUD);
            this.KRGB.Location = new System.Drawing.Point(12, 158);
            this.KRGB.Name = "KRGB";
            this.KRGB.Size = new System.Drawing.Size(41, 45);
            this.KRGB.TabIndex = 5;
            this.KRGB.TabStop = false;
            this.KRGB.Text = "Kr";
            // 
            // KRNUD
            // 
            this.KRNUD.AutoSize = true;
            this.KRNUD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KRNUD.Location = new System.Drawing.Point(3, 16);
            this.KRNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KRNUD.Name = "KRNUD";
            this.KRNUD.Size = new System.Drawing.Size(35, 20);
            this.KRNUD.TabIndex = 3;
            this.KRNUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // LoadImageBtn
            // 
            this.LoadImageBtn.Location = new System.Drawing.Point(12, 419);
            this.LoadImageBtn.Name = "LoadImageBtn";
            this.LoadImageBtn.Size = new System.Drawing.Size(132, 62);
            this.LoadImageBtn.TabIndex = 5;
            this.LoadImageBtn.Text = "Wczytaj obraz...";
            this.LoadImageBtn.UseVisualStyleBackColor = true;
            this.LoadImageBtn.Click += new System.EventHandler(this.LoadImageBtn_Click);
            // 
            // AlgorithmGB
            // 
            this.AlgorithmGB.Controls.Add(this.AlgorithmCB);
            this.AlgorithmGB.Location = new System.Drawing.Point(12, 41);
            this.AlgorithmGB.Name = "AlgorithmGB";
            this.AlgorithmGB.Size = new System.Drawing.Size(139, 54);
            this.AlgorithmGB.TabIndex = 1;
            this.AlgorithmGB.TabStop = false;
            this.AlgorithmGB.Text = "Algorytm";
            // 
            // AlgorithmCB
            // 
            this.AlgorithmCB.FormattingEnabled = true;
            this.AlgorithmCB.Items.AddRange(new object[] {
            "Rozproszenie średnie",
            "Uporządkowane drżenie (losowe)",
            "Uporządkowane drżenie (pozycja piksela)",
            "Propagacja błędu",
            "Algorytm popularnościowy"});
            this.AlgorithmCB.Location = new System.Drawing.Point(6, 19);
            this.AlgorithmCB.Name = "AlgorithmCB";
            this.AlgorithmCB.Size = new System.Drawing.Size(126, 21);
            this.AlgorithmCB.TabIndex = 1;
            // 
            // KGB
            // 
            this.KGB.Controls.Add(this.KNUD);
            this.KGB.Location = new System.Drawing.Point(12, 101);
            this.KGB.Name = "KGB";
            this.KGB.Size = new System.Drawing.Size(66, 51);
            this.KGB.TabIndex = 4;
            this.KGB.TabStop = false;
            this.KGB.Text = "K";
            // 
            // KNUD
            // 
            this.KNUD.Location = new System.Drawing.Point(6, 19);
            this.KNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KNUD.Name = "KNUD";
            this.KNUD.Size = new System.Drawing.Size(54, 20);
            this.KNUD.TabIndex = 3;
            this.KNUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(12, 12);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(132, 23);
            this.GenerateBtn.TabIndex = 0;
            this.GenerateBtn.Text = "Generuj";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // WorkImagePB
            // 
            this.WorkImagePB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkImagePB.BackColor = System.Drawing.Color.White;
            this.WorkImagePB.Location = new System.Drawing.Point(3, 3);
            this.WorkImagePB.Name = "WorkImagePB";
            this.WorkImagePB.Size = new System.Drawing.Size(634, 480);
            this.WorkImagePB.TabIndex = 0;
            this.WorkImagePB.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 493);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "ColorReduction";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.KBGB.ResumeLayout(false);
            this.KBGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KBNUD)).EndInit();
            this.KGGB.ResumeLayout(false);
            this.KGGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KGNUD)).EndInit();
            this.KRGB.ResumeLayout(false);
            this.KRGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KRNUD)).EndInit();
            this.AlgorithmGB.ResumeLayout(false);
            this.KGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkImagePB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox WorkImagePB;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.ComboBox AlgorithmCB;
        private System.Windows.Forms.GroupBox AlgorithmGB;
        private System.Windows.Forms.GroupBox KGB;
        private System.Windows.Forms.NumericUpDown KNUD;
        private System.Windows.Forms.Button LoadImageBtn;
        private System.Windows.Forms.GroupBox KBGB;
        private System.Windows.Forms.NumericUpDown KBNUD;
        private System.Windows.Forms.GroupBox KGGB;
        private System.Windows.Forms.NumericUpDown KGNUD;
        private System.Windows.Forms.GroupBox KRGB;
        private System.Windows.Forms.NumericUpDown KRNUD;
    }
}

