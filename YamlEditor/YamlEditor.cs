using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using YamlDotNet.RepresentationModel;
using Logging;
using Data_Model;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace YamlEditor
{
    public partial class YamlEditor : MaterialForm
    {
        public YamlEditor()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );

            //makes the toolstripo buttons bigger
            toolStrip_TopMenu.ImageScalingSize = new Size(36, 36);
            //remove toolstrip border
            toolStrip_TopMenu.Renderer = new ToolStripNoBoder();

            Logger.Instance.Recorder = new Logging.DateRecorderDecorator(new CounterDecorator(new TextBoxRecorder(textBox_Log)));

        }

        //Corre form no segundo ecra 
        private void OnFormLoad(object sender, EventArgs e)
        {
            //this.Location = Screen.AllScreens[1].WorkingArea.Location;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnOpen(object sender, EventArgs e)
        {
            DeleteAllFilesAndNodesFromMemory();
            var dialog = new OpenFileDialog()
            { Filter = @"Yaml files (*.yaml)|*.yaml|All files (*.*)|*.*", DefaultExt = "yaml" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                System.Diagnostics.Trace.WriteLine($"Filename: {dialog.FileName}");
                Directory.SetCurrentDirectory(Path.GetDirectoryName(dialog.FileName) ?? "");

                MyYamlFileFactory.CreateMyYamlFile(dialog.FileName);//Loads the file

                //Switches the configuration file to first place
                MyYamlFile configuration = MyYamlFile.all_files[MyYamlFile.all_files.Count - 1];
                int counter = MyYamlFile.all_files.Count - 1;
                while (counter > -1)
                {
                    if (counter == 0) MyYamlFile.all_files[counter] = configuration;
                    else MyYamlFile.all_files[counter] = MyYamlFile.all_files[counter - 1];

                    counter--;
                }

                PopulateTreeView(mainTreeView, Path.GetDirectoryName(dialog.FileName));
            }

            Cursor.Current = Cursors.Default;
        }

        private void PopulateTreeView(TreeView treeView, string base_directory)
        {

            treeView.Nodes.Clear();

            foreach (MyYamlFile file in MyYamlFile.all_files)
            {
                TreeNode new_node = new TreeNode();

                if (file.directory != (base_directory + "\\"))
                {
                    //If file directory is different from the base directory loads the new directory files inside the directory node.
                    string include_dir_name = new DirectoryInfo(file.directory).Name; // gets the folder name
                    TreeNode directory_node = new TreeNode();
                    directory_node = treeView.Nodes.Find(include_dir_name, true)[0];

                    new_node = CreateTreeNode(file.fileName, file);
                    directory_node.Nodes.Add(new_node);
                }
                else
                {
                    new_node = CreateTreeNode(file.fileName, file);
                    treeView.Nodes.Add(new_node);
                }

                foreach (MyYamlNode yamlnode in file.nodes)
                {
                    if (yamlnode.nodes != null && yamlnode.nodes[0] is MyYamlSequenceNode)
                    {
                        PopulateNodes(new_node, yamlnode.nodes[0].nodes[0]);
                    }
                    else
                    {
                        PopulateNodes(new_node, yamlnode);
                    }
                }
            }
            //treeView.ExpandAll();
        }

        private void PopulateNodes(TreeNode parent, MyYamlNode yamlnode)
        {
            if (yamlnode is MyYamlScalarNode)
            {
                MyYamlScalarNode yamlnodeAsScalar = (MyYamlScalarNode)yamlnode;
                if (yamlnode.name == "") parent.Nodes.Add(CreateTreeNode(yamlnodeAsScalar.value, yamlnode));
                else parent.Nodes.Add(CreateTreeNode(yamlnode.name, yamlnode));
            }
            else if (yamlnode is MyYamlMappingNode)
            {
                TreeNode new_parent = new TreeNode();

                if (yamlnode.name == "")
                {
                    try
                    {
                        MyYamlMappingNode parentAsMyYamlNode = (MyYamlMappingNode)parent.Tag;
                        new_parent = CreateTreeNode(parent.Name, yamlnode);
                        parent.Nodes.Add(new_parent);
                    }
                    catch (Exception) { }

                    try
                    {
                        MyYamlSequenceNode parentAsMyYamlNode = (MyYamlSequenceNode)parent.Tag;
                        new_parent = CreateTreeNode(parent.Name, yamlnode);
                        parent.Nodes.Add(new_parent);
                    }
                    catch (Exception) { }

                    try
                    {
                        MyYamlFile parentAsMyYamlNode = (MyYamlFile)parent.Tag;
                        new_parent = CreateTreeNode(parent.Name, yamlnode);
                        parent.Nodes.Add(new_parent);
                    }
                    catch (Exception) { }
                }
                else if (yamlnode.name == "-")
                {
                    new_parent = CreateTreeNode(yamlnode.name, yamlnode);
                    parent.Nodes.Add(new_parent);
                }
                else
                {
                    new_parent = CreateTreeNode(yamlnode.name, yamlnode);
                    parent.Nodes.Add(new_parent);
                }

                foreach (MyYamlNode child in yamlnode.nodes)
                {
                    PopulateNodes(new_parent, child);
                }

            }
            else if (yamlnode is MyYamlSequenceNode)
            {
                TreeNode new_parent = CreateTreeNode(yamlnode.name, yamlnode);
                parent.Nodes.Add(new_parent);
                foreach (MyYamlNode child in yamlnode.nodes)
                {
                    PopulateNodes(new_parent, child);
                }
            }
        }

        public TreeNode CreateTreeNode(string name, Object yamlnode)
        {
            var new_node = new TreeNode();
            new_node.Name = name;
            new_node.Text = name;
            new_node.Tag = yamlnode;
            new_node.ImageIndex = new_node.SelectedImageIndex = GetImageIndex(yamlnode);
            return new_node;
        }

        private int GetImageIndex(Object node)
        {
            if (node is MyYamlMappingNode)
            {
                if (node is MyYamlMappingNode mapping && mapping.Contains("platform")) return 5;
                return 4;
            }
            else if (node is MyYamlSequenceNode)
            {
                return 3;
            }
            else if (node is MyYamlScalarNode)
            {
                var nodeAsScalar = (MyYamlScalarNode)node;
                if (nodeAsScalar.tag == "!secret") return 2;
                if (nodeAsScalar.tag == "!include") return 1;
                if (nodeAsScalar.tag == "!include_dir_named") return 6;
                return 0;
            }
            else if (node is MyYamlFile)
            {
                return 4;
            }
            return 0;
        }

        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            mainPropertyGrid.SelectedObject = e.Node.Tag;
            MessageBox.Show(mainTreeView.SelectedNode.Tag.ToString());
        }

        //Loads help page of clicked node
        private void OnDoubleClick(object sender, EventArgs e)
        {
            if (mainTreeView.SelectedNode == null) return;
            var selected = mainTreeView.SelectedNode;

            if (selected.Tag is MyYamlMappingNode node)
            {
                if (node.Contains("platform"))
                {
                    var childAsScalarNode = (MyYamlScalarNode)node.GetFirst("platform");
                    mainWebBrowser.Url = new Uri($@"https://www.home-assistant.io/components/{ selected.Text }.{ childAsScalarNode.value }");
                    mainTabControl.SelectTab(helpTabPage);
                    Logger.Instance.WriteLine($"Help page  \"https://www.home-assistant.io/components/{ selected.Text }.{ childAsScalarNode.value }\" opened.");
                }

            }
        }

        public void DeleteAllFilesAndNodesFromMemory()
        {
            MyYamlFile.all_files.Clear();
        }

        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            new MyYamlFile().SaveAllFiles();
        }
    }
}


//Removes toolstrip border
public partial class ToolStripNoBoder : ToolStripSystemRenderer
{
    public ToolStripNoBoder() { }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        //base.OnRenderToolStripBorder(e);
    }
}
