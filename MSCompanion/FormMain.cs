using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace jamoram62.tools.MSCompanion
{
    public partial class FormMain : Form
    {
        // private Dictionary<string, string> _cachedRestoreInfo = null;

        private AddonGridData _addonGridDataInfo;

        private bool _shouldRestorePropertiesFile = false;
        private string _propertiesFileBackupName = "";

        // private Dictionary<AddOnBasicInfo, bool> _cachedAddonList = null; 
        private MovieAddonList _cachedMovieAddonList = null;
        private MovieMediaFileManifest _cachedMovieManifest = null;

        /// <summary>
        /// Addon name to filter used props
        /// </summary>
        private string _usedPropAddonFilter = "";

        private string _version
        {
            get
            {
                Version version = Assembly.GetCallingAssembly().GetName().Version;
                return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            }
        }

        // --------------------------------------------------------------------------------------------------

        #region Initialization
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = "Moviestorm Companion Toolbox    (version: " + _version + ")";
            SetToolTips();

            if (!Globals.Initialization(mLog))
            {
                // MessageBox.Show("Something's gone wrong. Can't find Moviestorm folders. ")
                FormSettings settingsForm = new FormSettings();
                if ((settingsForm.ShowDialog() != DialogResult.OK) || !Globals.Initialization(mLog))
                    this.Close();
            }

            ContextHelp.HelpNamespace = Globals.HelpFilename;

            AddonStatusRefresh();
            MoviesRefresh();

        }


        private void SetToolTips()
        {
            ToolTip formToolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            formToolTip.AutoPopDelay = 5000;
            formToolTip.InitialDelay = 1000;
            formToolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            formToolTip.ShowAlways = true;

            // Set up the ToolTip text for the components in the form

            formToolTip.SetToolTip(
                dgvAddons,
                "Information about installed packages."
                );


            formToolTip.SetToolTip(
                lbMovies,
                "List of movies found in the Moviestorm user data folder. Right-click: options"
                );

            formToolTip.SetToolTip(
                lbMovieAddons,
                "List of addons actually used for producing the movie currently selected. Right-click: options"
                );

            formToolTip.SetToolTip(
                lbUsedProps,
                "List of props used for producing the movie currently selected. Right-click: options"
                );

            formToolTip.SetToolTip(
                clbMovieMediaFiles,
                "Media files (images, video and audio clips, dialogue, etc) found in the movie folders. Non-checked items are not actually used in the movie"
                );
            formToolTip.SetToolTip(pbMediaManifestClean,
                "Delete those media files that are not currently used or referred to");
            // formToolTip.SetToolTip(clbAddons, "List of addons installed and its current status (checked=enabled)");
            formToolTip.SetToolTip(lbMovies,
                "List of user movies found");

        }

        #endregion Initialization


        #region SetupChange
        // ------------------------------------------------------------------------------------------------

        private void pbSetup_Click(object sender, EventArgs e)
        {
            string oldUserDataPath = Globals.ConfigurationInfo.MoviestormUserDataPath;
            FormSettings settingsForm = new FormSettings();

            if ((settingsForm.ShowDialog() != DialogResult.OK) || (oldUserDataPath == Globals.ConfigurationInfo.MoviestormUserDataPath))
                return;

            Globals.InitPostConfigurationLoad();

            AddonStatusRefresh();
            MoviesRefresh();
        }

        #endregion SetupChange

        // .......................................................................................................

        /// <summary>
        /// Show enabled status info about addons
        /// </summary>
        public void AddonStatusRefresh()
        {
            dgvAddons.AutoGenerateColumns = false;
            // dgvAddons.DataSource = null;
            tbPackageTotal.Text = tbPackageEnabled.Text = tbMeshDataTotal.Text = tbMeshDataEnabled.Text = "";


            _addonGridDataInfo = new AddonGridData(Globals.AddonEnabledMap);

            dgvAddons.DataSource = _addonGridDataInfo.AddonGridDataList;

            tbPackageTotal.Text = _addonGridDataInfo.PackageCount.ToString();
            tbPackageEnabled.Text = _addonGridDataInfo.PackageEnabledCount.ToString();
            tbMeshDataTotal.Text = _addonGridDataInfo.MeshDataMbytes.ToString("##.##");
            tbMeshDataEnabled.Text = _addonGridDataInfo.MeshDataMbytesEnabled.ToString("##.##");

            // tbAddonSummary.Text = gridData.AddonGridDataList[0].GetSummary();
        }

        // -------------------------------------------------------------------------------------------------------

        public void MoviesRefresh()
        {
            lbMovies.Items.Clear();
            lbMovieAddons.Items.Clear();

            foreach (string movieName in Globals.MovieList)
                lbMovies.Items.Add(movieName);
        }


        /// <summary>
        /// Selected movie has changed
        /// </summary>
        private void lbMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cachedMovieAddonList = null; _cachedMovieManifest = null;
            string movieName = Globals.MovieList[lbMovies.SelectedIndex];
            _usedPropAddonFilter = "";

            double movieAddonMeshSize = RefreshMovieAddons(movieName, Globals.PackageSet);
            lblMovieAddonMeshSize.Text = movieAddonMeshSize.ToString("###.##");

            RefreshUsedProps(movieName, _usedPropAddonFilter);
            RefreshMovieMediaFiles(movieName);

        }


        private void ppmiMoviesDefinitionFileContents_Click(object sender, EventArgs e)
        {
#if _NEVERDEFINED_
            string moviePath = Globals.GetMoviePath(Globals.MovieList[lbMovies.SelectedIndex]);
            if (sender == ppmiMoviesDefinitionFileContents)
            {
                MovieFilesContentsTreeViewForm movieFilesForm = new MovieFilesContentsTreeViewForm(mLog, moviePath);
                movieFilesForm.Show();
            }
            else
            {
                MovieFilesContentsTextForm movieFilesForm = new MovieFilesContentsTextForm(mLog, moviePath);
                movieFilesForm.Show();
            }
#endif
        }



        /// <summary>
        /// Refresh the list of Addons used and/or referenced by the selected movie
        /// </summary>
        /// <param name="pMovieName">Name of the movie</param>
        private double RefreshMovieAddons(string pMovieName, AddonPackageSet pPackageSet)
        {
            lbMovieAddons.Items.Clear();

            MovieAddonList movieAddonList = new MovieAddonList(Globals.GetMoviePath(pMovieName));
            double meshTotalSize = 0.0;

            foreach (AddOnBasicInfo item in movieAddonList.AddonList)
            {
                lbMovieAddons.Items.Add(string.Format("{0}  ({1})", item.FriendlyName, item.Publisher));
                AddonPackage package = pPackageSet.FindPackageByFriendlyName(item.FriendlyName, item.Publisher);
                if ((package != null) && (package.MeshDataSizeMbytes.HasValue))
                    meshTotalSize += package.MeshDataSizeMbytes.Value;
            }
            _cachedMovieAddonList = movieAddonList;
            
            return meshTotalSize;
            
        }


        private void RefreshUsedProps(string pMovieName, string pAddonFilter = "")
        {
            lbUsedProps.Items.Clear();

            MoviePropList moviePropList = new MoviePropList(Globals.GetMoviePath(pMovieName));

            List<string> keyList = moviePropList.PropList.Keys.ToList();
            keyList.Sort();

            foreach (string propName in keyList)
            {
                string packageName;
                if (!Globals.MapPropsToPackages.TryGetValue(propName, out packageName))
                    packageName = "";
                else
                {
                    AddonPackage package = Globals.PackageSet.FindPackageByName(packageName);
                    if ((package != null) && (!string.IsNullOrEmpty(package.FriendlyName)))
                        packageName = package.FriendlyName;
                }
                if ((pAddonFilter != "") && (packageName != pAddonFilter))
                    continue;

                lbUsedProps.Items.Add(
                    string.Format(
                    "{0} {1} [{2}]", moviePropList.PropList[propName], propName, packageName
                    )
                    );
            }
        }


        private void RefreshMovieMediaFiles(string pMovieName)
        {

            clbMovieMediaFiles.Items.Clear();

            MovieMediaFileManifest movieManifest = new MovieMediaFileManifest(Globals.GetMoviePath(pMovieName));

            foreach (MovieMediaFile file in movieManifest.FileList)
            {
                clbMovieMediaFiles.Items.Add(file.FullRelativeName);
                clbMovieMediaFiles.SetItemChecked(clbMovieMediaFiles.Items.Count - 1, file.IsUsed);
            }
            _cachedMovieManifest = movieManifest;
        }



        // --------------------------------------------------------------------------------------------

        private void pbMediaManifestClean_Click(object sender, EventArgs e)
        {
            if (_cachedMovieManifest == null)
                return;

            if (
                MessageBox.Show(
                    "You are to delete those media files actually not used by the movie. This operation is not reversible. Please confirm.",
                    "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            Globals.LogIt("Deleting files not used");
            _cachedMovieManifest.DeleteUnused();

            _cachedMovieManifest = null;
            string movieName = Globals.MovieList[lbMovies.SelectedIndex];

            Globals.LogIt("Refreshing the list of media files");
            RefreshMovieMediaFiles(movieName);
        }


        // ------------------------------------------------------------------------------------------------------

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_shouldRestorePropertiesFile)
                return;

            e.Cancel = (MessageBox.Show("The list of addons enabled has been modified and hasn't been restored."
                                        + Environment.NewLine + "Please confirm exiting the application",
                "Exit confirmation",
                MessageBoxButtons.YesNo) == DialogResult.No);
        }



        private void cmsUsedProps_Opening(object sender, CancelEventArgs e)
        {
            if (lbUsedProps.Items.Count == 0)
                e.Cancel = true;
        }


        private void cmsiUsedPropsToClipboard_Click(object sender, EventArgs e)
        {
            StringBuilder propsList = new StringBuilder();
            foreach (string item in lbUsedProps.Items)
            {
                propsList.AppendLine(item);
            }
            Clipboard.SetText(propsList.ToString());
        }



        // ---------------------------------------------------------------------------------------------

        private void cmsAddons_Opening(object sender, CancelEventArgs e)
        {
            if (lbMovieAddons.Items.Count == 0)
                e.Cancel = true;

        }


        /// <summary>
        /// Enable only those addons used in the currently selected movie (can be restored when finished)
        /// </summary>
        private void cmsiAddonsSetEnabled_Click(object sender, EventArgs e)
        {
            if (_cachedMovieAddonList == null)
                return;

            if (
                MessageBox.Show(
                    "Do you really mean to enable only those addons used in the selected movie?",
                    "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            _propertiesFileBackupName = Globals.PropertiesFile.UpdateAddonReferences(_cachedMovieAddonList, _addonGridDataInfo);
            if(!string.IsNullOrEmpty(_propertiesFileBackupName))
                _shouldRestorePropertiesFile = true;

            Globals.LogIt("Configuration backup file created: {0}", _propertiesFileBackupName);

            cmsiAddonsSetEnabled.Visible = false;
            cmsiAddonsRestoreEnabled.Visible = true;

            Globals.RefreshAddonListAndStatus();
            AddonStatusRefresh();
        }


        /// <summary>
        /// Restore enabled addons to its original state
        /// </summary>
        private void cmsiAddonsRestoreEnabled_Click(object sender, EventArgs e)
        {
            if (!Globals.PropertiesFile.Restore(_propertiesFileBackupName))
            {

                return;
            }

            cmsiAddonsSetEnabled.Visible = true;
            cmsiAddonsRestoreEnabled.Visible = false;

            Globals.RefreshAddonListAndStatus();
            AddonStatusRefresh();

            Globals.LogIt("Configuration file restored from backup: {0}", _propertiesFileBackupName);
            _propertiesFileBackupName = "";
            _shouldRestorePropertiesFile = false;

        }




        private void cmsiAddonsFilterProps_Click(object sender, EventArgs e)
        {
            if (_cachedMovieAddonList == null || (lbMovieAddons.SelectedIndex < 0))
                return;

            string movieName = Globals.MovieList[lbMovies.SelectedIndex];
            _usedPropAddonFilter =
                ((string)lbMovieAddons.Items[lbMovieAddons.SelectedIndex]).Split('(')[0].Trim();

            RefreshUsedProps(movieName, _usedPropAddonFilter);
        }

        private void cmsiAddonsUnfilterProps_Click(object sender, EventArgs e)
        {
            if (_cachedMovieAddonList == null)
                return;

            if (_usedPropAddonFilter == "")
                return;

            string movieName = Globals.MovieList[lbMovies.SelectedIndex];
            _usedPropAddonFilter = "";
            RefreshUsedProps(movieName, _usedPropAddonFilter);
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder movieAddonsList = new StringBuilder();
            foreach (string item in lbMovieAddons.Items)
            {
                movieAddonsList.AppendLine(item);
            }
            Clipboard.SetText(movieAddonsList.ToString());
        }

        private void dgvAddons_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAddons.SelectedRows.Count > 0)
            {
                tbAddonSummary.Text =
                    _addonGridDataInfo.AddonGridDataList[dgvAddons.SelectedRows[0].Index].Summary;
            }
        }


        // ------------------------------------------------------------------------------------------------

        private void cmsAddonGrid_Opening(object sender, CancelEventArgs e)
        {
            cmsiAddonGridSanitize.Enabled = (!_shouldRestorePropertiesFile && (dgvAddons.Rows.Count > 0));
        }


        private void cmsiAddonGridSanitize_Click(object sender, EventArgs e)
        {
            bool? appendNotReferenced;
            string confirmationText = "";

            if (sender == cmsiSanConfigRemove)
            {
                appendNotReferenced = false;
                confirmationText = "This option will remove any reference to addons currently not installed " +
                                   Environment.NewLine +
                                   "from the configuration file (a backup file will be created). Please confirm.";

            }
            else if (sender == cmsiSanConfigFull)
            {
                appendNotReferenced = true;
                confirmationText = "This option will:" + Environment.NewLine +
                                   "   1) create a backup of the configuration file" + Environment.NewLine +
                                   "   2) remove any reference to addons currently not installed from the configuration file" +
                                   Environment.NewLine +
                                   "   3) append references to addons installed and not referenced in the configuration file" +
                                   Environment.NewLine +
                                   "Please confirm.";
            }
            else
                return;


            if (
                MessageBox.Show(confirmationText, "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            string backupFileName = Globals.PropertiesFile.UpdateAddonReferences(null, _addonGridDataInfo, appendNotReferenced.Value);
            Globals.LogIt("Done. Backup file name: {0}", backupFileName);

            Globals.RefreshAddonListAndStatus();
            AddonStatusRefresh();
        }




        private void cmsiAddonGridRestore_Click(object sender, EventArgs e)
        {
            ofdSelectConfigurationBackup.FileName = "";

            ofdSelectConfigurationBackup.InitialDirectory = MovieStormPropertiesFile.BackupFilesFolder; //  Globals.ConfigurationInfo.AppBackupFilesFolder;
            if (ofdSelectConfigurationBackup.ShowDialog() != DialogResult.OK)
                return;
            string fileName = ofdSelectConfigurationBackup.FileName;
            if (!Globals.PropertiesFile.Restore(fileName))
                return;

            Globals.LogIt("Configuration file restored from backup: {0}", fileName);

            Globals.RefreshAddonListAndStatus();
            AddonStatusRefresh();
        }

        private void cmsiAddonGridCopy_Click(object sender, EventArgs e)
        {
            dgvAddons.MultiSelect = true;
            dgvAddons.SelectAll();
            DataObject dataObj = dgvAddons.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
            dgvAddons.MultiSelect = false;
        }


        private void dgvAddons_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Color cellColor = Color.Black;
            if (!_addonGridDataInfo.AddonGridDataList[e.RowIndex].Installed)
                cellColor = (_addonGridDataInfo.AddonGridDataList[e.RowIndex].Preview) ? Color.Brown : Color.Red;
            else if (!_addonGridDataInfo.AddonGridDataList[e.RowIndex].InPropertiesFile)
                cellColor = Color.BlueViolet;
            e.CellStyle.ForeColor = cellColor;
        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            try
            {
                if (Globals.HelpFileUri != "")
                    Help.ShowHelp(this, Globals.HelpFileUri, HelpNavigator.TopicId, "100");
            }
            catch { }
        }

        // -------------------------------------------------------------------------------------------------

        private void cmsiMovieBackup_Click(object sender, EventArgs e)
        {
            if (lbMovies.SelectedIndex < 0)
                return;
            if (string.IsNullOrEmpty(sfdMovieArchive.InitialDirectory))
                sfdMovieArchive.InitialDirectory = Globals.ConfigurationInfo.AppBackupFilesFolder;

            sfdMovieArchive.FileName = lbMovies.Items[lbMovies.SelectedIndex] + ".zip";
            

            if (sfdMovieArchive.ShowDialog() != DialogResult.OK)
                return;

            string moviePath = Path.Combine(Globals.MoviesPath, lbMovies.Items[lbMovies.SelectedIndex].ToString());
            string errorText;
            if(Movie.CreateArchive(moviePath, sfdMovieArchive.FileName, out errorText))
                Globals.LogIt("Movie archive created: {0}", sfdMovieArchive.FileName);


        }


        // ------------------------------------------------------------------------------------------------


    }
}
