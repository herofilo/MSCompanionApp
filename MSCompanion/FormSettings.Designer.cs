using System.ComponentModel;
using System.Windows.Forms;

namespace jamoram62.tools.MSCompanion
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.edAppPath = new System.Windows.Forms.TextBox();
            this.pbSelAppPath = new System.Windows.Forms.Button();
            this.pbSelUserPath = new System.Windows.Forms.Button();
            this.edUserPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbSave = new System.Windows.Forms.Button();
            this.pbCancel = new System.Windows.Forms.Button();
            this.pbSelBackupPath = new System.Windows.Forms.Button();
            this.edAppBackupFilesPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pbSelTempPath = new System.Windows.Forms.Button();
            this.edAppTemporaryPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ContextHelp = new System.Windows.Forms.HelpProvider();
            this.pbHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Moviestorm Application Path: ";
            // 
            // edAppPath
            // 
            this.edAppPath.Location = new System.Drawing.Point(208, 19);
            this.edAppPath.Margin = new System.Windows.Forms.Padding(4);
            this.edAppPath.Name = "edAppPath";
            this.edAppPath.ReadOnly = true;
            this.edAppPath.Size = new System.Drawing.Size(425, 23);
            this.edAppPath.TabIndex = 1;
            // 
            // pbSelAppPath
            // 
            this.pbSelAppPath.Location = new System.Drawing.Point(640, 19);
            this.pbSelAppPath.Name = "pbSelAppPath";
            this.pbSelAppPath.Size = new System.Drawing.Size(62, 23);
            this.pbSelAppPath.TabIndex = 2;
            this.pbSelAppPath.Text = "Select";
            this.pbSelAppPath.UseVisualStyleBackColor = true;
            this.pbSelAppPath.Click += new System.EventHandler(this.pbSelAppPath_Click);
            // 
            // pbSelUserPath
            // 
            this.pbSelUserPath.Location = new System.Drawing.Point(640, 50);
            this.pbSelUserPath.Name = "pbSelUserPath";
            this.pbSelUserPath.Size = new System.Drawing.Size(62, 23);
            this.pbSelUserPath.TabIndex = 5;
            this.pbSelUserPath.Text = "Select";
            this.pbSelUserPath.UseVisualStyleBackColor = true;
            this.pbSelUserPath.Click += new System.EventHandler(this.pbSelUserPath_Click);
            // 
            // edUserPath
            // 
            this.edUserPath.Location = new System.Drawing.Point(208, 50);
            this.edUserPath.Margin = new System.Windows.Forms.Padding(4);
            this.edUserPath.Name = "edUserPath";
            this.edUserPath.ReadOnly = true;
            this.edUserPath.Size = new System.Drawing.Size(425, 23);
            this.edUserPath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Moviestorm User Data: ";
            // 
            // pbSave
            // 
            this.pbSave.Location = new System.Drawing.Point(559, 151);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(75, 23);
            this.pbSave.TabIndex = 6;
            this.pbSave.Text = "Save";
            this.pbSave.UseVisualStyleBackColor = true;
            this.pbSave.Click += new System.EventHandler(this.pbSave_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.Location = new System.Drawing.Point(640, 151);
            this.pbCancel.Name = "pbCancel";
            this.pbCancel.Size = new System.Drawing.Size(75, 23);
            this.pbCancel.TabIndex = 7;
            this.pbCancel.Text = "Cancel";
            this.pbCancel.UseVisualStyleBackColor = true;
            this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
            // 
            // pbSelBackupPath
            // 
            this.pbSelBackupPath.Location = new System.Drawing.Point(640, 81);
            this.pbSelBackupPath.Name = "pbSelBackupPath";
            this.pbSelBackupPath.Size = new System.Drawing.Size(62, 23);
            this.pbSelBackupPath.TabIndex = 10;
            this.pbSelBackupPath.Text = "Select";
            this.pbSelBackupPath.UseVisualStyleBackColor = true;
            this.pbSelBackupPath.Click += new System.EventHandler(this.pbSelBackupPath_Click);
            // 
            // edAppBackupFilesPath
            // 
            this.edAppBackupFilesPath.Location = new System.Drawing.Point(208, 81);
            this.edAppBackupFilesPath.Margin = new System.Windows.Forms.Padding(4);
            this.edAppBackupFilesPath.Name = "edAppBackupFilesPath";
            this.edAppBackupFilesPath.ReadOnly = true;
            this.edAppBackupFilesPath.Size = new System.Drawing.Size(425, 23);
            this.edAppBackupFilesPath.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Backup Files Folder:";
            // 
            // pbSelTempPath
            // 
            this.pbSelTempPath.Location = new System.Drawing.Point(640, 112);
            this.pbSelTempPath.Name = "pbSelTempPath";
            this.pbSelTempPath.Size = new System.Drawing.Size(62, 23);
            this.pbSelTempPath.TabIndex = 13;
            this.pbSelTempPath.Text = "Select";
            this.pbSelTempPath.UseVisualStyleBackColor = true;
            this.pbSelTempPath.Click += new System.EventHandler(this.pbSelTempPath_Click);
            // 
            // edAppTemporaryPath
            // 
            this.edAppTemporaryPath.Location = new System.Drawing.Point(208, 112);
            this.edAppTemporaryPath.Margin = new System.Windows.Forms.Padding(4);
            this.edAppTemporaryPath.Name = "edAppTemporaryPath";
            this.edAppTemporaryPath.ReadOnly = true;
            this.edAppTemporaryPath.Size = new System.Drawing.Size(425, 23);
            this.edAppTemporaryPath.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Temporary Folder:";
            // 
            // pbHelp
            // 
            this.pbHelp.Image = ((System.Drawing.Image)(resources.GetObject("pbHelp.Image")));
            this.pbHelp.Location = new System.Drawing.Point(528, 151);
            this.pbHelp.Name = "pbHelp";
            this.pbHelp.Size = new System.Drawing.Size(25, 23);
            this.pbHelp.TabIndex = 14;
            this.pbHelp.UseVisualStyleBackColor = true;
            this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 181);
            this.Controls.Add(this.pbHelp);
            this.Controls.Add(this.pbSelTempPath);
            this.Controls.Add(this.edAppTemporaryPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbSelBackupPath);
            this.Controls.Add(this.edAppBackupFilesPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbCancel);
            this.Controls.Add(this.pbSave);
            this.Controls.Add(this.pbSelUserPath);
            this.Controls.Add(this.edUserPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbSelAppPath);
            this.Controls.Add(this.edAppPath);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ContextHelp.SetHelpKeyword(this, "200");
            this.ContextHelp.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.ContextHelp.SetShowHelp(this, true);
            this.ShowIcon = false;
            this.Text = "Application Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private FolderBrowserDialog folderBrowserDialog;
        private TextBox edAppPath;
        private Button pbSelAppPath;
        private Button pbSelUserPath;
        private TextBox edUserPath;
        private Label label2;
        private Button pbSave;
        private Button pbCancel;
        private Button pbSelBackupPath;
        private TextBox edAppBackupFilesPath;
        private Label label3;
        private Button pbSelTempPath;
        private TextBox edAppTemporaryPath;
        private Label label4;
        private HelpProvider ContextHelp;
        private Button pbHelp;
    }
}