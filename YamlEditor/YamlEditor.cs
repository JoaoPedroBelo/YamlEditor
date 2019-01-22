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
            this.Location = Screen.AllScreens[1].WorkingArea.Location;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnOpen(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            { Filter = @"Yaml files (*.yaml)|*.yaml|All files (*.*)|*.*", DefaultExt = "yaml" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
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
        }

        private void PopulateTreeView(TreeView treeView, string base_directory)
        {
            treeView.Nodes.Clear();

            TreeNode root = CreateTreeNode("homeassistant", new MyYamlMappingNode("",0));
            treeView.Nodes.Add(root);

            foreach (MyYamlFile file in MyYamlFile.all_files)
            {
                if (file.directory != (base_directory + "\\"))
                {
                    string include_dir_name = new DirectoryInfo(file.directory).Name;
                    TreeNode new_node = new TreeNode();
                    if (treeView.Nodes.Find(include_dir_name, true).Length == 0)
                    {
                        new_node = CreateTreeNode(include_dir_name, new MyYamlMappingNode("", 0));
                        root.Nodes.Add(new_node);
                    }
                    else
                    {
                        new_node = treeView.Nodes.Find(include_dir_name, true)[0];
                    }

                }
                else
                {
                    TreeNode new_node = CreateTreeNode(file.fileName, new MyYamlMappingNode("", 0));
                    root.Nodes.Add(new_node);

                    if (file.nodes != null)
                    {
                        foreach (MyYamlNode yamlnode in file.nodes)
                        {
                            PopulateNodes(new_node, yamlnode);
                        }
                    }
                }
            }

            treeView.ExpandAll();
        }

        private void PopulateNodes(TreeNode parent, MyYamlNode yamlnode)
        {
            if (yamlnode is MyYamlScalarNode)
            {
                parent.Nodes.Add(CreateTreeNode(yamlnode.name,yamlnode));
            }else if (yamlnode is MyYamlMappingNode)
            {
                TreeNode new_parent = CreateTreeNode(yamlnode.name, yamlnode);
                parent.Nodes.Add(new_parent);
                foreach (MyYamlNode child in yamlnode.nodes)
                {
                    PopulateNodes(new_parent, child);
                }
            }else if (yamlnode is MyYamlSequenceNode)
            {
                TreeNode new_parent = CreateTreeNode(yamlnode.name, yamlnode);
                parent.Nodes.Add(new_parent);
                foreach (MyYamlNode child in yamlnode.nodes)
                {
                    PopulateNodes(new_parent, child);
                }
            }
        }

        public TreeNode CreateTreeNode(string name, MyYamlNode yamlnode)
        {
            var new_node = new TreeNode();
            new_node.Name = name;
            new_node.Text = name;
            new_node.Tag = yamlnode;
            return new_node;
        }

        private int GetImageIndex(YamlNode node)
        {
            switch (node.NodeType)
            {
                case YamlNodeType.Scalar:
                    if (node.Tag == "!secret") return 2;
                    if (node.Tag == "!include") return 1;
                    return 0;
                case YamlNodeType.Sequence: return 3;
                case YamlNodeType.Mapping:
                    if (node is YamlMappingNode mapping && mapping.Children.Any(pair => ((YamlScalarNode)pair.Key).Value == "platform")) return 5;
                    return 4;
            }
            return 0;
        }

        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            mainPropertyGrid.SelectedObject = e.Node.Tag;
        }

        //Loads help page of clicked node
        private void OnDoubleClick(object sender, EventArgs e)
        {
            if (mainTreeView.SelectedNode == null) return;
            var selected = mainTreeView.SelectedNode;

            if (selected.Tag is YamlMappingNode node)
            {
                if (node.Children.Any(p => ((YamlScalarNode)p.Key).Value == "platform"))
                {
                    var platform = node.Children.FirstOrDefault(p => ((YamlScalarNode)p.Key).Value == "platform");
                    mainWebBrowser.Url = new Uri($@"https://www.home-assistant.io/components/{ selected.Text }.{ platform.Value }");
                    mainTabControl.SelectTab(helpTabPage);
                    Logger.Instance.WriteLine($"Help page  \"https://www.home-assistant.io/components/{ selected.Text }.{ platform.Value }\" opened.");
                }
            }
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
