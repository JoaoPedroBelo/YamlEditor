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

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace YamlEditorFinal
{
    public partial class YamlEditorFinal : MaterialForm
    {
        public YamlEditorFinal()
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
                Logger.Instance.WriteLine($"File  \"{dialog.FileName}\" opened.");
                Directory.SetCurrentDirectory(Path.GetDirectoryName(dialog.FileName) ?? "");

                mainTreeView.Nodes.Clear();
                var root = mainTreeView.Nodes.Add(Path.GetFileName(dialog.FileName));
                root.ImageIndex = root.SelectedImageIndex = 3;
                LoadFile(root, dialog.FileName);
                root.Expand();
            }  
        }

        private void LoadFile(TreeNode node, string filename)
        {
            var yaml = new YamlStream();
            try
            {
                using (var stream = new StreamReader(filename))
                {
                    yaml.Load(stream);
                }
            }
            catch (Exception exception)
            {
                Logger.Instance.WriteLine(exception.Message);
            }

            if (yaml.Documents.Count == 0) return;
            LoadChildren(node, yaml.Documents[0].RootNode as YamlMappingNode);
        }

        private void LoadChildren(TreeNode root, YamlMappingNode mapping)
        {
            var children = mapping?.Children;
            if (children == null) return;

            foreach (var child in children)
            {
                var key = child.Key as YamlScalarNode;
                System.Diagnostics.Trace.Assert(key != null);

                if (child.Value is YamlScalarNode)
                {
                    var scalar = child.Value as YamlScalarNode;

                    var node = root.Nodes.Add($"{key.Value}: {scalar.Value}");
                    node.Tag = child;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(scalar);

                    if (scalar.Tag == "!include")
                    {
                        LoadFile(node, scalar.Value);
                    }
                }
                else if (child.Value is YamlSequenceNode)
                {
                    var node = root.Nodes.Add(key.Value);
                    node.Tag = child.Value;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child.Value);

                    LoadChildren(node, child.Value as YamlSequenceNode);
                }
                else if (child.Value is YamlMappingNode)
                {
                    var node = root.Nodes.Add(key.Value);
                    node.Tag = child.Value;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child.Value);

                    LoadChildren(node, child.Value as YamlMappingNode);
                }
            }
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

        private void LoadChildren(TreeNode root, YamlSequenceNode sequence)
        {
            foreach (var child in sequence.Children)
            {
                if (child is YamlSequenceNode)
                {
                    var node = root.Nodes.Add(root.Text);
                    node.Tag = child;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);

                    LoadChildren(node, child as YamlSequenceNode);
                }
                else if (child is YamlMappingNode)
                {
                    var node = root.Nodes.Add(root.Text);
                    node.Tag = child;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);

                    LoadChildren(node, child as YamlMappingNode);
                }
                else if (child is YamlScalarNode)
                {
                    var scalar = child as YamlScalarNode;
                    var node = root.Nodes.Add(scalar.Value);
                    node.Tag = child;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);
                }
            }
        }

        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            mainPropertyGrid.SelectedObject = e.Node.Tag;
        }

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

public partial class ToolStripNoBoder : ToolStripSystemRenderer
{
    public ToolStripNoBoder() { }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        //base.OnRenderToolStripBorder(e);
    }
}
