using System.ComponentModel;
using System.Windows.Forms;

namespace jamoram62.tools.MSCompanion
{
    partial class MovieFilesContentsTreeViewForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblMovieName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvMovieDetail = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.tvMovieSummary = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Movie name:";
            // 
            // lblMovieName
            // 
            this.lblMovieName.AutoSize = true;
            this.lblMovieName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovieName.Location = new System.Drawing.Point(99, 9);
            this.lblMovieName.Name = "lblMovieName";
            this.lblMovieName.Size = new System.Drawing.Size(52, 17);
            this.lblMovieName.TabIndex = 1;
            this.lblMovieName.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tvMovieDetail);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tvMovieSummary);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(7, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1062, 645);
            this.panel1.TabIndex = 2;
            // 
            // tvMovieDetail
            // 
            this.tvMovieDetail.Location = new System.Drawing.Point(473, 32);
            this.tvMovieDetail.Name = "tvMovieDetail";
            this.tvMovieDetail.Size = new System.Drawing.Size(580, 610);
            this.tvMovieDetail.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(470, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Detail File:";
            // 
            // tvMovieSummary
            // 
            this.tvMovieSummary.Location = new System.Drawing.Point(5, 32);
            this.tvMovieSummary.Name = "tvMovieSummary";
            this.tvMovieSummary.Size = new System.Drawing.Size(462, 610);
            this.tvMovieSummary.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Summary File:";
            // 
            // MovieFilesContentsTreeViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 686);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMovieName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MovieFilesContentsTreeViewForm";
            this.Text = "Movie File Contents";
            this.Load += new System.EventHandler(this.MovieFilesContents_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label lblMovieName;
        private Panel panel1;
        private TreeView tvMovieSummary;
        private Label label2;
        private TreeView tvMovieDetail;
        private Label label3;
    }
}