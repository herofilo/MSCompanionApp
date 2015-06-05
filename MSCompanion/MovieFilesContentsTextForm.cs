using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace jamoram62.tools.MSCompanion
{
    public partial class MovieFilesContentsTextForm : Form
    {

        private TextBox _mLog = null;
        private string _moviePath = "";

        public MovieFilesContentsTextForm(TextBox pMLog, string pMoviePath = "")
        {
            InitializeComponent();
            _mLog = pMLog;
            if (!string.IsNullOrEmpty(pMoviePath))
                _moviePath = pMoviePath;
        }

        private void MovieFilesContentsTextForm_Load(object sender, EventArgs e)
        {
            if (_moviePath == "")
                Close();

            lblMovieName.Text = Path.GetFileName(_moviePath);
            if (_mLog != null)
                _mLog.AppendText(string.Format("Loading contents of definition files for movie '{0}'...{1}", lblMovieName.Text, Environment.NewLine));

            FillTextView(rtbSummary, Path.Combine(_moviePath, Globals.MovieSummary));

            // string movieDetailFile = Path.Combine(_moviePath, Globals.MovieMScope);
            FillTextView(rtbDetail, Path.Combine(_moviePath, Globals.MovieMScope));
            if (_mLog != null)
                _mLog.AppendText(string.Format("Content of definition files for movie '{0}' loaded.{1}", lblMovieName.Text, Environment.NewLine));

        }

        private void FillTextView(RichTextBox pRichTextBox, string pFileName)
        {
            XmlTextReader reader = new XmlTextReader(pFileName);

            while (reader.Read())
            {

                switch (reader.NodeType)
                {

                    case XmlNodeType.Element: // The node is an element.
                        pRichTextBox.SelectionColor = Color.Blue;
                        pRichTextBox.AppendText("<");
                        pRichTextBox.SelectionColor = Color.Brown;
                        pRichTextBox.AppendText(reader.Name);
                        pRichTextBox.SelectionColor = Color.Blue;
                        pRichTextBox.AppendText(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        pRichTextBox.SelectionColor = Color.Black;
                        pRichTextBox.AppendText(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        pRichTextBox.SelectionColor = Color.Blue;
                        pRichTextBox.AppendText("</");
                        pRichTextBox.SelectionColor = Color.Brown;
                        pRichTextBox.AppendText(reader.Name);
                        pRichTextBox.SelectionColor = Color.Blue;
                        pRichTextBox.AppendText(">");
                        pRichTextBox.AppendText("\n");
                        break;
                }

            }            

        }
    }
}
