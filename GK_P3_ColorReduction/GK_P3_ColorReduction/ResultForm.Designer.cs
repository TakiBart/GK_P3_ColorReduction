﻿namespace GK_P3_ColorReduction
{
    partial class ResultForm
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
            this.ResultPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ResultPB)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultPB
            // 
            this.ResultPB.BackColor = System.Drawing.Color.White;
            this.ResultPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultPB.Location = new System.Drawing.Point(0, 0);
            this.ResultPB.Name = "ResultPB";
            this.ResultPB.Size = new System.Drawing.Size(664, 501);
            this.ResultPB.TabIndex = 0;
            this.ResultPB.TabStop = false;
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(664, 501);
            this.Controls.Add(this.ResultPB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ResultForm";
            this.Text = "ResultForm";
            this.ResizeBegin += new System.EventHandler(this.ResultForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.ResultForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.ResultPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox ResultPB;
    }
}