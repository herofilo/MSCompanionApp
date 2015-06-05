using System.ComponentModel;
using System.Windows.Forms;

namespace jamoram62.tools.MSCompanion
{
    partial class MovieFilesContentsTextForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbDetail = new System.Windows.Forms.RichTextBox();
            this.rtbSummary = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMovieName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtbDetail);
            this.panel1.Controls.Add(this.rtbSummary);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 21);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1078, 615);
            this.panel1.TabIndex = 5;
            // 
            // rtbDetail
            // 
            this.rtbDetail.Location = new System.Drawing.Point(510, 39);
            this.rtbDetail.Name = "rtbDetail";
            this.rtbDetail.Size = new System.Drawing.Size(565, 565);
            this.rtbDetail.TabIndex = 5;
            this.rtbDetail.Text = "";
            // 
            // rtbSummary
            // 
            this.rtbSummary.Location = new System.Drawing.Point(9, 39);
            this.rtbSummary.Name = "rtbSummary";
            this.rtbSummary.Size = new System.Drawing.Size(495, 565);
            this.rtbSummary.TabIndex = 4;
            this.rtbSummary.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(507, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Detail File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Summary File:";
            // 
            // lblMovieName
            // 
            this.lblMovieName.AutoSize = true;
            this.lblMovieName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovieName.Location = new System.Drawing.Point(88, 0);
            this.lblMovieName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMovieName.Name = "lblMovieName";
            this.lblMovieName.Size = new System.Drawing.Size(52, 17);
            this.lblMovieName.TabIndex = 4;
            this.lblMovieName.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Movie name:";
            // 
            // MovieFilesContentsTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 638);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMovieName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MovieFilesContentsTextForm";
            this.Text = "MovieFilesContentsTextForm";
            this.Load += new System.EventHandler(this.MovieFilesContentsTextForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label label3;
        private Label label2;
        private Label lblMovieName;
        private Label label1;
        private RichTextBox rtbSummary;
        private RichTextBox rtbDetail;
    }
}