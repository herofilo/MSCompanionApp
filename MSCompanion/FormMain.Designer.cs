using System.ComponentModel;
using System.Windows.Forms;

namespace jamoram62.tools.MSCompanion
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAddons = new System.Windows.Forms.DataGridView();
            this.cmsAddonGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsiAddonGridSanitize = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiSanConfigRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiSanConfigFull = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiAddonGridRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiAddonGridCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.pbSetup = new System.Windows.Forms.Button();
            this.gbAddons = new System.Windows.Forms.GroupBox();
            this.tbAddonSummary = new System.Windows.Forms.TextBox();
            this.tbMeshDataTotal = new System.Windows.Forms.Label();
            this.tbMeshDataEnabled = new System.Windows.Forms.Label();
            this.tbPackageEnabled = new System.Windows.Forms.Label();
            this.tbPackageTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMovies = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mtpUsedProps = new System.Windows.Forms.TabPage();
            this.lbUsedProps = new System.Windows.Forms.ListBox();
            this.cmsUsedProps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsiUsedPropsToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mtpMediaFiles = new System.Windows.Forms.TabPage();
            this.pbMediaManifestClean = new System.Windows.Forms.Button();
            this.clbMovieMediaFiles = new System.Windows.Forms.CheckedListBox();
            this.gbUsedAddons = new System.Windows.Forms.GroupBox();
            this.lblMovieAddonMeshSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbMovieAddons = new System.Windows.Forms.ListBox();
            this.cmsAddons = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiAddonsSetEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiAddonsRestoreEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiAddonsFilterProps = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiAddonsUnfilterProps = new System.Windows.Forms.ToolStripMenuItem();
            this.lbMovies = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ppmiMoviesDefinitionFileContents = new System.Windows.Forms.ToolStripMenuItem();
            this.movieDefinitionFilesTextViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiMovieBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.mLog = new System.Windows.Forms.TextBox();
            this.ofdSelectConfigurationBackup = new System.Windows.Forms.OpenFileDialog();
            this.ContextHelp = new System.Windows.Forms.HelpProvider();
            this.pbHelp = new System.Windows.Forms.Button();
            this.sfdMovieArchive = new System.Windows.Forms.SaveFileDialog();
            this.Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FriendlyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Publisher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeshDataSizeMbytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BodyCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BodyAnimationCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilterCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoundCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Installed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.InPropertiesFile = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MoviestormPackage = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Preview = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddons)).BeginInit();
            this.cmsAddonGrid.SuspendLayout();
            this.gbAddons.SuspendLayout();
            this.gbMovies.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.mtpUsedProps.SuspendLayout();
            this.cmsUsedProps.SuspendLayout();
            this.mtpMediaFiles.SuspendLayout();
            this.gbUsedAddons.SuspendLayout();
            this.cmsAddons.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAddons
            // 
            this.dgvAddons.AllowUserToAddRows = false;
            this.dgvAddons.AllowUserToDeleteRows = false;
            this.dgvAddons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAddons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Enabled,
            this.FriendlyName,
            this.Publisher,
            this.MeshDataSizeMbytes,
            this.PropCount,
            this.BodyCount,
            this.BodyAnimationCount,
            this.FilterCount,
            this.SoundCount,
            this.Installed,
            this.InPropertiesFile,
            this.MoviestormPackage,
            this.Preview});
            this.dgvAddons.ContextMenuStrip = this.cmsAddonGrid;
            this.dgvAddons.Location = new System.Drawing.Point(11, 19);
            this.dgvAddons.MultiSelect = false;
            this.dgvAddons.Name = "dgvAddons";
            this.dgvAddons.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAddons.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvAddons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAddons.Size = new System.Drawing.Size(761, 209);
            this.dgvAddons.TabIndex = 1;
            this.dgvAddons.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAddons_CellFormatting);
            this.dgvAddons.SelectionChanged += new System.EventHandler(this.dgvAddons_SelectionChanged);
            // 
            // cmsAddonGrid
            // 
            this.cmsAddonGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsiAddonGridSanitize,
            this.cmsiAddonGridRestore,
            this.cmsiAddonGridCopy});
            this.cmsAddonGrid.Name = "cmsAddonGrid";
            this.cmsAddonGrid.Size = new System.Drawing.Size(213, 70);
            this.cmsAddonGrid.Opening += new System.ComponentModel.CancelEventHandler(this.cmsAddonGrid_Opening);
            // 
            // cmsiAddonGridSanitize
            // 
            this.cmsiAddonGridSanitize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsiSanConfigRemove,
            this.cmsiSanConfigFull});
            this.cmsiAddonGridSanitize.Name = "cmsiAddonGridSanitize";
            this.cmsiAddonGridSanitize.Size = new System.Drawing.Size(212, 22);
            this.cmsiAddonGridSanitize.Text = "Sanitize Configuration File";
            // 
            // cmsiSanConfigRemove
            // 
            this.cmsiSanConfigRemove.Name = "cmsiSanConfigRemove";
            this.cmsiSanConfigRemove.Size = new System.Drawing.Size(298, 22);
            this.cmsiSanConfigRemove.Text = "Remove references to addons not installed";
            this.cmsiSanConfigRemove.Click += new System.EventHandler(this.cmsiAddonGridSanitize_Click);
            // 
            // cmsiSanConfigFull
            // 
            this.cmsiSanConfigFull.Name = "cmsiSanConfigFull";
            this.cmsiSanConfigFull.Size = new System.Drawing.Size(298, 22);
            this.cmsiSanConfigFull.Text = "Id plus add references to addons installed";
            this.cmsiSanConfigFull.Click += new System.EventHandler(this.cmsiAddonGridSanitize_Click);
            // 
            // cmsiAddonGridRestore
            // 
            this.cmsiAddonGridRestore.Name = "cmsiAddonGridRestore";
            this.cmsiAddonGridRestore.Size = new System.Drawing.Size(212, 22);
            this.cmsiAddonGridRestore.Text = "Restore Backup";
            this.cmsiAddonGridRestore.Click += new System.EventHandler(this.cmsiAddonGridRestore_Click);
            // 
            // cmsiAddonGridCopy
            // 
            this.cmsiAddonGridCopy.Name = "cmsiAddonGridCopy";
            this.cmsiAddonGridCopy.Size = new System.Drawing.Size(212, 22);
            this.cmsiAddonGridCopy.Text = "Copy to clipboard";
            this.cmsiAddonGridCopy.Click += new System.EventHandler(this.cmsiAddonGridCopy_Click);
            // 
            // pbSetup
            // 
            this.pbSetup.Location = new System.Drawing.Point(898, 12);
            this.pbSetup.Name = "pbSetup";
            this.pbSetup.Size = new System.Drawing.Size(75, 23);
            this.pbSetup.TabIndex = 0;
            this.pbSetup.Text = "Setup";
            this.pbSetup.UseVisualStyleBackColor = true;
            this.pbSetup.Click += new System.EventHandler(this.pbSetup_Click);
            // 
            // gbAddons
            // 
            this.gbAddons.Controls.Add(this.tbAddonSummary);
            this.gbAddons.Controls.Add(this.tbMeshDataTotal);
            this.gbAddons.Controls.Add(this.tbMeshDataEnabled);
            this.gbAddons.Controls.Add(this.tbPackageEnabled);
            this.gbAddons.Controls.Add(this.tbPackageTotal);
            this.gbAddons.Controls.Add(this.label4);
            this.gbAddons.Controls.Add(this.label3);
            this.gbAddons.Controls.Add(this.label2);
            this.gbAddons.Controls.Add(this.label1);
            this.gbAddons.Controls.Add(this.dgvAddons);
            this.gbAddons.Location = new System.Drawing.Point(5, 43);
            this.gbAddons.Name = "gbAddons";
            this.gbAddons.Size = new System.Drawing.Size(978, 258);
            this.gbAddons.TabIndex = 1;
            this.gbAddons.TabStop = false;
            this.gbAddons.Text = "AddOns";
            // 
            // tbAddonSummary
            // 
            this.tbAddonSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAddonSummary.Location = new System.Drawing.Point(778, 19);
            this.tbAddonSummary.Multiline = true;
            this.tbAddonSummary.Name = "tbAddonSummary";
            this.tbAddonSummary.ReadOnly = true;
            this.tbAddonSummary.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbAddonSummary.Size = new System.Drawing.Size(194, 209);
            this.tbAddonSummary.TabIndex = 14;
            this.tbAddonSummary.WordWrap = false;
            // 
            // tbMeshDataTotal
            // 
            this.tbMeshDataTotal.AutoSize = true;
            this.tbMeshDataTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMeshDataTotal.Location = new System.Drawing.Point(574, 239);
            this.tbMeshDataTotal.Name = "tbMeshDataTotal";
            this.tbMeshDataTotal.Size = new System.Drawing.Size(35, 13);
            this.tbMeshDataTotal.TabIndex = 13;
            this.tbMeshDataTotal.Text = "label5";
            // 
            // tbMeshDataEnabled
            // 
            this.tbMeshDataEnabled.AutoSize = true;
            this.tbMeshDataEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMeshDataEnabled.Location = new System.Drawing.Point(472, 239);
            this.tbMeshDataEnabled.Name = "tbMeshDataEnabled";
            this.tbMeshDataEnabled.Size = new System.Drawing.Size(41, 13);
            this.tbMeshDataEnabled.TabIndex = 12;
            this.tbMeshDataEnabled.Text = "label5";
            // 
            // tbPackageEnabled
            // 
            this.tbPackageEnabled.AutoSize = true;
            this.tbPackageEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPackageEnabled.Location = new System.Drawing.Point(113, 239);
            this.tbPackageEnabled.Name = "tbPackageEnabled";
            this.tbPackageEnabled.Size = new System.Drawing.Size(50, 13);
            this.tbPackageEnabled.TabIndex = 11;
            this.tbPackageEnabled.Text = "pkgEna";
            // 
            // tbPackageTotal
            // 
            this.tbPackageTotal.AutoSize = true;
            this.tbPackageTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPackageTotal.Location = new System.Drawing.Point(217, 239);
            this.tbPackageTotal.Name = "tbPackageTotal";
            this.tbPackageTotal.Size = new System.Drawing.Size(41, 13);
            this.tbPackageTotal.TabIndex = 10;
            this.tbPackageTotal.Text = "pkgTot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(534, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Total:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(314, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mesh Data Size (MB) Enabled:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Packages enabled:";
            // 
            // gbMovies
            // 
            this.gbMovies.Controls.Add(this.tabControl1);
            this.gbMovies.Controls.Add(this.gbUsedAddons);
            this.gbMovies.Controls.Add(this.lbMovies);
            this.gbMovies.Location = new System.Drawing.Point(5, 307);
            this.gbMovies.Name = "gbMovies";
            this.gbMovies.Size = new System.Drawing.Size(978, 200);
            this.gbMovies.TabIndex = 2;
            this.gbMovies.TabStop = false;
            this.gbMovies.Text = "Movies";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.mtpUsedProps);
            this.tabControl1.Controls.Add(this.mtpMediaFiles);
            this.tabControl1.Location = new System.Drawing.Point(499, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(473, 183);
            this.tabControl1.TabIndex = 2;
            // 
            // mtpUsedProps
            // 
            this.mtpUsedProps.Controls.Add(this.lbUsedProps);
            this.mtpUsedProps.Location = new System.Drawing.Point(4, 22);
            this.mtpUsedProps.Name = "mtpUsedProps";
            this.mtpUsedProps.Padding = new System.Windows.Forms.Padding(3);
            this.mtpUsedProps.Size = new System.Drawing.Size(465, 157);
            this.mtpUsedProps.TabIndex = 0;
            this.mtpUsedProps.Text = "Used Props";
            this.mtpUsedProps.UseVisualStyleBackColor = true;
            // 
            // lbUsedProps
            // 
            this.lbUsedProps.ContextMenuStrip = this.cmsUsedProps;
            this.lbUsedProps.FormattingEnabled = true;
            this.lbUsedProps.HorizontalScrollbar = true;
            this.lbUsedProps.Location = new System.Drawing.Point(6, 8);
            this.lbUsedProps.Name = "lbUsedProps";
            this.lbUsedProps.Size = new System.Drawing.Size(453, 147);
            this.lbUsedProps.TabIndex = 1;
            // 
            // cmsUsedProps
            // 
            this.cmsUsedProps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsiUsedPropsToClipboard});
            this.cmsUsedProps.Name = "cmsUsedProps";
            this.cmsUsedProps.Size = new System.Drawing.Size(172, 26);
            this.cmsUsedProps.Opening += new System.ComponentModel.CancelEventHandler(this.cmsUsedProps_Opening);
            // 
            // cmsiUsedPropsToClipboard
            // 
            this.cmsiUsedPropsToClipboard.Name = "cmsiUsedPropsToClipboard";
            this.cmsiUsedPropsToClipboard.Size = new System.Drawing.Size(171, 22);
            this.cmsiUsedPropsToClipboard.Text = "Copy to Clipboard";
            this.cmsiUsedPropsToClipboard.Click += new System.EventHandler(this.cmsiUsedPropsToClipboard_Click);
            // 
            // mtpMediaFiles
            // 
            this.mtpMediaFiles.Controls.Add(this.pbMediaManifestClean);
            this.mtpMediaFiles.Controls.Add(this.clbMovieMediaFiles);
            this.mtpMediaFiles.Location = new System.Drawing.Point(4, 22);
            this.mtpMediaFiles.Name = "mtpMediaFiles";
            this.mtpMediaFiles.Padding = new System.Windows.Forms.Padding(3);
            this.mtpMediaFiles.Size = new System.Drawing.Size(465, 157);
            this.mtpMediaFiles.TabIndex = 1;
            this.mtpMediaFiles.Text = "Media Files Found";
            this.mtpMediaFiles.UseVisualStyleBackColor = true;
            // 
            // pbMediaManifestClean
            // 
            this.pbMediaManifestClean.Location = new System.Drawing.Point(411, 131);
            this.pbMediaManifestClean.Name = "pbMediaManifestClean";
            this.pbMediaManifestClean.Size = new System.Drawing.Size(48, 23);
            this.pbMediaManifestClean.TabIndex = 2;
            this.pbMediaManifestClean.Text = "Clean";
            this.pbMediaManifestClean.UseVisualStyleBackColor = true;
            this.pbMediaManifestClean.Click += new System.EventHandler(this.pbMediaManifestClean_Click);
            // 
            // clbMovieMediaFiles
            // 
            this.clbMovieMediaFiles.FormattingEnabled = true;
            this.clbMovieMediaFiles.Location = new System.Drawing.Point(3, 3);
            this.clbMovieMediaFiles.Name = "clbMovieMediaFiles";
            this.clbMovieMediaFiles.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.clbMovieMediaFiles.Size = new System.Drawing.Size(456, 124);
            this.clbMovieMediaFiles.TabIndex = 0;
            // 
            // gbUsedAddons
            // 
            this.gbUsedAddons.Controls.Add(this.lblMovieAddonMeshSize);
            this.gbUsedAddons.Controls.Add(this.label5);
            this.gbUsedAddons.Controls.Add(this.lbMovieAddons);
            this.gbUsedAddons.Location = new System.Drawing.Point(264, 10);
            this.gbUsedAddons.Name = "gbUsedAddons";
            this.gbUsedAddons.Size = new System.Drawing.Size(229, 184);
            this.gbUsedAddons.TabIndex = 1;
            this.gbUsedAddons.TabStop = false;
            this.gbUsedAddons.Text = "Used Addons";
            // 
            // lblMovieAddonMeshSize
            // 
            this.lblMovieAddonMeshSize.AutoSize = true;
            this.lblMovieAddonMeshSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovieAddonMeshSize.Location = new System.Drawing.Point(113, 164);
            this.lblMovieAddonMeshSize.Name = "lblMovieAddonMeshSize";
            this.lblMovieAddonMeshSize.Size = new System.Drawing.Size(0, 13);
            this.lblMovieAddonMeshSize.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Mesh Data Size (MB):";
            // 
            // lbMovieAddons
            // 
            this.lbMovieAddons.ContextMenuStrip = this.cmsAddons;
            this.lbMovieAddons.FormattingEnabled = true;
            this.lbMovieAddons.Location = new System.Drawing.Point(6, 20);
            this.lbMovieAddons.Name = "lbMovieAddons";
            this.lbMovieAddons.Size = new System.Drawing.Size(213, 134);
            this.lbMovieAddons.TabIndex = 0;
            // 
            // cmsAddons
            // 
            this.cmsAddons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.cmsiAddonsSetEnabled,
            this.cmsiAddonsRestoreEnabled,
            this.cmsiAddonsFilterProps,
            this.cmsiAddonsUnfilterProps});
            this.cmsAddons.Name = "cmsAddons";
            this.cmsAddons.Size = new System.Drawing.Size(203, 136);
            this.cmsAddons.Opening += new System.ComponentModel.CancelEventHandler(this.cmsAddons_Opening);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // cmsiAddonsSetEnabled
            // 
            this.cmsiAddonsSetEnabled.Name = "cmsiAddonsSetEnabled";
            this.cmsiAddonsSetEnabled.Size = new System.Drawing.Size(202, 22);
            this.cmsiAddonsSetEnabled.Text = "Set Enabled Addons";
            this.cmsiAddonsSetEnabled.Click += new System.EventHandler(this.cmsiAddonsSetEnabled_Click);
            // 
            // cmsiAddonsRestoreEnabled
            // 
            this.cmsiAddonsRestoreEnabled.Name = "cmsiAddonsRestoreEnabled";
            this.cmsiAddonsRestoreEnabled.Size = new System.Drawing.Size(202, 22);
            this.cmsiAddonsRestoreEnabled.Text = "Restore Enabled Addons";
            this.cmsiAddonsRestoreEnabled.Visible = false;
            this.cmsiAddonsRestoreEnabled.Click += new System.EventHandler(this.cmsiAddonsRestoreEnabled_Click);
            // 
            // cmsiAddonsFilterProps
            // 
            this.cmsiAddonsFilterProps.Name = "cmsiAddonsFilterProps";
            this.cmsiAddonsFilterProps.Size = new System.Drawing.Size(202, 22);
            this.cmsiAddonsFilterProps.Text = "Filter Used Props";
            this.cmsiAddonsFilterProps.Click += new System.EventHandler(this.cmsiAddonsFilterProps_Click);
            // 
            // cmsiAddonsUnfilterProps
            // 
            this.cmsiAddonsUnfilterProps.Name = "cmsiAddonsUnfilterProps";
            this.cmsiAddonsUnfilterProps.Size = new System.Drawing.Size(202, 22);
            this.cmsiAddonsUnfilterProps.Text = "Reset Filter Used Props";
            this.cmsiAddonsUnfilterProps.Click += new System.EventHandler(this.cmsiAddonsUnfilterProps_Click);
            // 
            // lbMovies
            // 
            this.lbMovies.ContextMenuStrip = this.contextMenuStrip1;
            this.lbMovies.FormattingEnabled = true;
            this.lbMovies.Location = new System.Drawing.Point(7, 20);
            this.lbMovies.Name = "lbMovies";
            this.lbMovies.Size = new System.Drawing.Size(251, 173);
            this.lbMovies.TabIndex = 0;
            this.lbMovies.SelectedIndexChanged += new System.EventHandler(this.lbMovies_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ppmiMoviesDefinitionFileContents,
            this.movieDefinitionFilesTextViewToolStripMenuItem,
            this.cmsiMovieBackup});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(250, 70);
            // 
            // ppmiMoviesDefinitionFileContents
            // 
            this.ppmiMoviesDefinitionFileContents.Enabled = false;
            this.ppmiMoviesDefinitionFileContents.Name = "ppmiMoviesDefinitionFileContents";
            this.ppmiMoviesDefinitionFileContents.Size = new System.Drawing.Size(249, 22);
            this.ppmiMoviesDefinitionFileContents.Text = "Movie Definition Files (Tree view)";
            this.ppmiMoviesDefinitionFileContents.Visible = false;
            this.ppmiMoviesDefinitionFileContents.Click += new System.EventHandler(this.ppmiMoviesDefinitionFileContents_Click);
            // 
            // movieDefinitionFilesTextViewToolStripMenuItem
            // 
            this.movieDefinitionFilesTextViewToolStripMenuItem.Enabled = false;
            this.movieDefinitionFilesTextViewToolStripMenuItem.Name = "movieDefinitionFilesTextViewToolStripMenuItem";
            this.movieDefinitionFilesTextViewToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.movieDefinitionFilesTextViewToolStripMenuItem.Text = "Movie Definition Files (Text view)";
            this.movieDefinitionFilesTextViewToolStripMenuItem.Visible = false;
            this.movieDefinitionFilesTextViewToolStripMenuItem.Click += new System.EventHandler(this.ppmiMoviesDefinitionFileContents_Click);
            // 
            // cmsiMovieBackup
            // 
            this.cmsiMovieBackup.Name = "cmsiMovieBackup";
            this.cmsiMovieBackup.Size = new System.Drawing.Size(249, 22);
            this.cmsiMovieBackup.Text = "Create Movie Backup Archive";
            this.cmsiMovieBackup.Click += new System.EventHandler(this.cmsiMovieBackup_Click);
            // 
            // mLog
            // 
            this.mLog.Location = new System.Drawing.Point(5, 513);
            this.mLog.Multiline = true;
            this.mLog.Name = "mLog";
            this.mLog.ReadOnly = true;
            this.mLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mLog.Size = new System.Drawing.Size(836, 60);
            this.mLog.TabIndex = 3;
            // 
            // pbHelp
            // 
            this.pbHelp.Image = ((System.Drawing.Image)(resources.GetObject("pbHelp.Image")));
            this.pbHelp.Location = new System.Drawing.Point(867, 14);
            this.pbHelp.Name = "pbHelp";
            this.pbHelp.Size = new System.Drawing.Size(25, 23);
            this.pbHelp.TabIndex = 4;
            this.pbHelp.UseVisualStyleBackColor = true;
            this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
            // 
            // sfdMovieArchive
            // 
            this.sfdMovieArchive.DefaultExt = "zip";
            this.sfdMovieArchive.Filter = "ZIP Archives|*.zip";
            this.sfdMovieArchive.Title = "Select destination of movie archive";
            // 
            // Enabled
            // 
            this.Enabled.DataPropertyName = "Enabled";
            this.Enabled.HeaderText = "Enabled";
            this.Enabled.Name = "Enabled";
            this.Enabled.ReadOnly = true;
            this.Enabled.Width = 52;
            // 
            // FriendlyName
            // 
            this.FriendlyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FriendlyName.DataPropertyName = "FriendlyName";
            this.FriendlyName.HeaderText = "Name";
            this.FriendlyName.Name = "FriendlyName";
            this.FriendlyName.ReadOnly = true;
            this.FriendlyName.Width = 60;
            // 
            // Publisher
            // 
            this.Publisher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Publisher.DataPropertyName = "Publisher";
            this.Publisher.HeaderText = "Publisher";
            this.Publisher.Name = "Publisher";
            this.Publisher.ReadOnly = true;
            this.Publisher.Width = 75;
            // 
            // MeshDataSizeMbytes
            // 
            this.MeshDataSizeMbytes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MeshDataSizeMbytes.DataPropertyName = "MeshDataSizeMbytes";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.MeshDataSizeMbytes.DefaultCellStyle = dataGridViewCellStyle8;
            this.MeshDataSizeMbytes.HeaderText = "Mesh Data Size (MB)";
            this.MeshDataSizeMbytes.Name = "MeshDataSizeMbytes";
            this.MeshDataSizeMbytes.ReadOnly = true;
            this.MeshDataSizeMbytes.Width = 101;
            // 
            // PropCount
            // 
            this.PropCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PropCount.DataPropertyName = "PropCount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.NullValue = null;
            this.PropCount.DefaultCellStyle = dataGridViewCellStyle9;
            this.PropCount.HeaderText = "Props #";
            this.PropCount.Name = "PropCount";
            this.PropCount.ReadOnly = true;
            this.PropCount.Width = 64;
            // 
            // BodyCount
            // 
            this.BodyCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.BodyCount.DataPropertyName = "BodyCount";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            this.BodyCount.DefaultCellStyle = dataGridViewCellStyle10;
            this.BodyCount.HeaderText = "BodyParts #";
            this.BodyCount.Name = "BodyCount";
            this.BodyCount.ReadOnly = true;
            this.BodyCount.Width = 83;
            // 
            // BodyAnimationCount
            // 
            this.BodyAnimationCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.BodyAnimationCount.DataPropertyName = "BodyAnimationCount";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N0";
            dataGridViewCellStyle11.NullValue = null;
            this.BodyAnimationCount.DefaultCellStyle = dataGridViewCellStyle11;
            this.BodyAnimationCount.HeaderText = "Body Anim#";
            this.BodyAnimationCount.Name = "BodyAnimationCount";
            this.BodyAnimationCount.ReadOnly = true;
            this.BodyAnimationCount.Width = 82;
            // 
            // FilterCount
            // 
            this.FilterCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.FilterCount.DataPropertyName = "FilterCount";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N0";
            dataGridViewCellStyle12.NullValue = null;
            this.FilterCount.DefaultCellStyle = dataGridViewCellStyle12;
            this.FilterCount.HeaderText = "Filters #";
            this.FilterCount.Name = "FilterCount";
            this.FilterCount.ReadOnly = true;
            this.FilterCount.Width = 64;
            // 
            // SoundCount
            // 
            this.SoundCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SoundCount.DataPropertyName = "SoundCount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N0";
            dataGridViewCellStyle13.NullValue = null;
            this.SoundCount.DefaultCellStyle = dataGridViewCellStyle13;
            this.SoundCount.HeaderText = "Sounds #";
            this.SoundCount.Name = "SoundCount";
            this.SoundCount.ReadOnly = true;
            this.SoundCount.Width = 72;
            // 
            // Installed
            // 
            this.Installed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Installed.DataPropertyName = "Installed";
            this.Installed.HeaderText = "Installed";
            this.Installed.Name = "Installed";
            this.Installed.ReadOnly = true;
            this.Installed.Width = 52;
            // 
            // InPropertiesFile
            // 
            this.InPropertiesFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.InPropertiesFile.DataPropertyName = "InPropertiesFile";
            this.InPropertiesFile.HeaderText = "In Config.";
            this.InPropertiesFile.Name = "InPropertiesFile";
            this.InPropertiesFile.ReadOnly = true;
            this.InPropertiesFile.Width = 52;
            // 
            // MoviestormPackage
            // 
            this.MoviestormPackage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MoviestormPackage.DataPropertyName = "MoviestormPackage";
            this.MoviestormPackage.HeaderText = "MS Pack.";
            this.MoviestormPackage.Name = "MoviestormPackage";
            this.MoviestormPackage.ReadOnly = true;
            this.MoviestormPackage.Width = 54;
            // 
            // Preview
            // 
            this.Preview.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Preview.DataPropertyName = "Preview";
            this.Preview.HeaderText = "Preview";
            this.Preview.Name = "Preview";
            this.Preview.ReadOnly = true;
            this.Preview.Width = 51;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 576);
            this.Controls.Add(this.pbHelp);
            this.Controls.Add(this.mLog);
            this.Controls.Add(this.gbMovies);
            this.Controls.Add(this.gbAddons);
            this.Controls.Add(this.pbSetup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ContextHelp.SetHelpKeyword(this, "100");
            this.ContextHelp.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ContextHelp.SetShowHelp(this, true);
            this.Text = "Moviestorm Companion Toolbox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddons)).EndInit();
            this.cmsAddonGrid.ResumeLayout(false);
            this.gbAddons.ResumeLayout(false);
            this.gbAddons.PerformLayout();
            this.gbMovies.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.mtpUsedProps.ResumeLayout(false);
            this.cmsUsedProps.ResumeLayout(false);
            this.mtpMediaFiles.ResumeLayout(false);
            this.gbUsedAddons.ResumeLayout(false);
            this.gbUsedAddons.PerformLayout();
            this.cmsAddons.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button pbSetup;
        private GroupBox gbAddons;
        private GroupBox gbMovies;
        private ListBox lbMovies;
        private TextBox mLog;
        private GroupBox gbUsedAddons;
        private ListBox lbMovieAddons;
        private CheckedListBox clbMovieMediaFiles;
        private Button pbMediaManifestClean;
        private ListBox lbUsedProps;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ppmiMoviesDefinitionFileContents;
        private ToolStripMenuItem movieDefinitionFilesTextViewToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage mtpUsedProps;
        private TabPage mtpMediaFiles;
        private ContextMenuStrip cmsUsedProps;
        private ToolStripMenuItem cmsiUsedPropsToClipboard;
        private ContextMenuStrip cmsAddons;
        private ToolStripMenuItem cmsiAddonsSetEnabled;
        private ToolStripMenuItem cmsiAddonsRestoreEnabled;
        private ToolStripMenuItem cmsiAddonsFilterProps;
        private ToolStripMenuItem cmsiAddonsUnfilterProps;
        private ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label tbPackageTotal;
        private Label tbPackageEnabled;
        private Label tbMeshDataTotal;
        private Label tbMeshDataEnabled;
        private TextBox tbAddonSummary;
        private Label lblMovieAddonMeshSize;
        private Label label5;
        private DataGridView dgvAddons;
        private ContextMenuStrip cmsAddonGrid;
        private ToolStripMenuItem cmsiAddonGridSanitize;
        private ToolStripMenuItem cmsiAddonGridRestore;
        private OpenFileDialog ofdSelectConfigurationBackup;
        private ToolStripMenuItem cmsiAddonGridCopy;
        private HelpProvider ContextHelp;
        private Button pbHelp;
        private ToolStripMenuItem cmsiMovieBackup;
        private SaveFileDialog sfdMovieArchive;
        private ToolStripMenuItem cmsiSanConfigRemove;
        private ToolStripMenuItem cmsiSanConfigFull;
        private DataGridViewCheckBoxColumn Enabled;
        private DataGridViewTextBoxColumn FriendlyName;
        private DataGridViewTextBoxColumn Publisher;
        private DataGridViewTextBoxColumn MeshDataSizeMbytes;
        private DataGridViewTextBoxColumn PropCount;
        private DataGridViewTextBoxColumn BodyCount;
        private DataGridViewTextBoxColumn BodyAnimationCount;
        private DataGridViewTextBoxColumn FilterCount;
        private DataGridViewTextBoxColumn SoundCount;
        private DataGridViewCheckBoxColumn Installed;
        private DataGridViewCheckBoxColumn InPropertiesFile;
        private DataGridViewCheckBoxColumn MoviestormPackage;
        private DataGridViewCheckBoxColumn Preview;
    }
}

