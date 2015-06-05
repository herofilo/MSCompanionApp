using System;
using System.Windows.Forms;

namespace jamoram62.tools.MSCompanion
{
    public partial class FormSettings : Form
    {
        private AppConfiguration _oldConfiguration;


        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            if(Globals.ConfigurationInfo == null)
                Globals.ConfigurationInfo = new AppConfiguration();
            _oldConfiguration = (AppConfiguration) Globals.ConfigurationInfo.Clone();
            edAppPath.Text = Globals.ConfigurationInfo.MoviestormAppPath;
            edUserPath.Text = Globals.ConfigurationInfo.MoviestormUserDataPath;
            edAppBackupFilesPath.Text = Globals.ConfigurationInfo.AppBackupFilesFolder;
            edAppTemporaryPath.Text = Globals.ConfigurationInfo.TmpPath;

            ContextHelp.HelpNamespace = Globals.HelpFilename;
        }



        private void pbSelAppPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = edAppPath.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                edAppPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void pbSelUserPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = edUserPath.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                edUserPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void pbSelBackupPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = edAppBackupFilesPath.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                edAppBackupFilesPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void pbSelTempPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = edAppTemporaryPath.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                edAppTemporaryPath.Text = folderBrowserDialog.SelectedPath;
        }



        private void pbSave_Click(object sender, EventArgs e)
        {
            Globals.ConfigurationInfo.MoviestormAppPath = edAppPath.Text;
            Globals.ConfigurationInfo.MoviestormUserDataPath = edUserPath.Text;
            Globals.ConfigurationInfo.AppBackupFilesFolder = edAppBackupFilesPath.Text;
            Globals.ConfigurationInfo.TmpPath = edAppTemporaryPath.Text;

            string checkErrorText;
            if (!Globals.ConfigurationInfo.CheckUserPath(out checkErrorText))
            {
                if (MessageBox.Show("Moviestorm User Folder invalid: " + checkErrorText +
                                    Environment.NewLine + "Continue anyway?", "Error checking Moviestorm User path",
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            Globals.ConfigurationInfo.SaveConfiguration();
            DialogResult = DialogResult.OK;
        }

        private void pbCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            try
            {
                if (Globals.HelpFileUri != "")
                    Help.ShowHelp(this, Globals.HelpFileUri, HelpNavigator.TopicId, "200");
            }
            catch { }
        }


    }
}
