using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace jamoram62.tools.MSCompanion
{



    public partial class MovieFilesContentsTreeViewForm : Form
    {
        private TextBox _mLog = null;
        private string _moviePath = "";

        public MovieFilesContentsTreeViewForm(TextBox pMLog, string pMoviePath = "")
        {
            InitializeComponent();
            _mLog = pMLog;
            if (!string.IsNullOrEmpty(pMoviePath))
                _moviePath = pMoviePath;

        }



        private void MovieFilesContents_Load(object sender, EventArgs e)
        {
            if(_moviePath == "")
                Close();

            lblMovieName.Text = Path.GetFileName(_moviePath);
            
            PopulateTreeView(tvMovieSummary, Path.Combine(_moviePath, Globals.MovieSummary));

            PopulateTreeView(tvMovieDetail, Path.Combine(_moviePath, Globals.MovieMScope));

        }



        private void PopulateTreeView(TreeView pTreeView, string pFileName)
        {
            if (!File.Exists(pFileName))
                return;

            try
            {
                // SECTION 1. Create a DOM Document and load the XML data into it.
                XmlDocument dom = new XmlDocument();
                dom.Load(pFileName);

                // SECTION 2. Initialize the TreeView control.
                pTreeView.Nodes.Clear();
                pTreeView.Nodes.Add(new TreeNode(dom.DocumentElement.Name));
                TreeNode tNode = new TreeNode();
                tNode = pTreeView.Nodes[0];

                // SECTION 3. Populate the TreeView with the DOM nodes.
                AddNode(dom.DocumentElement, tNode);
                pTreeView.ExpandAll();
            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;

            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                // Here you need to pull the data from the XmlNode based on the
                // type of node, whether attribute values are required, and so forth.
                inTreeNode.Text = (inXmlNode.OuterXml).Trim();
            }
        }

         



    }

    public class PopulateTreeViewParams
    {
        public bool IsSummary;
        public string FileName;
    }
}
