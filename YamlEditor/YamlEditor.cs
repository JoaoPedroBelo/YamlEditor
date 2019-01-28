using Data_Model;
using YamlEditor.Commands;
using Logging;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Timers;
using YamlDotNet.RepresentationModel;
using System.Collections.Generic;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace YamlEditor
{
    public partial class YamlEditor : MaterialForm
    {

        private MyYamlScalarNode selectedScalarNode { get; set; }
        private CommandManager Manager = new CommandManager();

        // Auto Save Timer
        private System.Timers.Timer autoSaveTimer = new System.Timers.Timer();

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

            toolStripMenuItem_ClearLog.Click += new EventHandler(ClearLogClicked);

            toolStripButton_Undo.Enabled = false;
            toolStripButton_Redo.Enabled = false;
            nameTextBox.Enabled = false;
            tagTextBox.Enabled = false;
            valueTextBox.Enabled = false;
            updateButton.Enabled = false;

            Manager.OnUpdate += (subject, data) =>
            {
                toolStripButton_Undo.Enabled = Manager.HasUndo();
                toolStripButton_Redo.Enabled = Manager.HasRedo();
                if (selectedScalarNode != null)
                {
                    nameTextBox.Text = selectedScalarNode.name;
                    tagTextBox.Text = selectedScalarNode.tag;
                    valueTextBox.Text = selectedScalarNode.value;
                }
            };

            // Auto Save Timer
            autoSaveTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnAutoSaveTimedEvent);
            autoSaveTimer.Interval = 30000; // 30 segundos
            autoSaveTimer.Enabled = false;
            autoSaveCheckBox.Checked = false;

        }

        public void ClearLogClicked(object sender, EventArgs e)
        {
            textBox_Log.Clear();
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
                    directory_node = FindNodeInTreeViewByValue(include_dir_name);

                    if (directory_node != null)
                    {
                        new_node = CreateTreeNode(file.fileName, file);
                        directory_node.Nodes.Add(new_node);
                    }
                }
                else
                {
                    new_node = CreateTreeNode(file.fileName, file);
                    treeView.Nodes.Add(new_node);
                }

                foreach (MyYamlNode yamlnode in file.nodes)
                {
                    if (yamlnode.nodes != null && yamlnode.nodes.Count > 0 && yamlnode.nodes[0] is MyYamlSequenceNode && yamlnode.name == "")
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
            if (yamlnode is MyYamlScalarNode)
            {
                ((MyYamlScalarNode)yamlnode).OnUpdate += (subject, data) =>
                {
                    if (((MyYamlScalarNode)subject).name == "")
                    {
                        new_node.Name = ((MyYamlScalarNode)subject).value;
                        new_node.Text = ((MyYamlScalarNode)subject).value;

                    }
                    else
                    {
                        new_node.Name = ((MyYamlScalarNode)subject).name;
                        new_node.Text = ((MyYamlScalarNode)subject).name;
                    }
                    new_node.Tag = (MyYamlScalarNode)subject;
                    new_node.ImageIndex = new_node.SelectedImageIndex = GetImageIndex((MyYamlScalarNode)subject);
                };
            }
            return new_node;
        }

        public TreeNode FindNodeInTreeViewByValue(string name)
        {
            foreach (TreeNode node in mainTreeView.Nodes)
            {
                if (node.Tag is MyYamlScalarNode)
                {
                    MyYamlScalarNode nodeAsScalar = (MyYamlScalarNode)node.Tag;
                    string folder_name = nodeAsScalar.value;

                    if (folder_name.Contains("\\"))
                        folder_name = folder_name.Split('\\')[folder_name.Split('\\').Length - 1];
                    else if (folder_name.Contains("/"))
                        folder_name = folder_name.Split('/')[folder_name.Split('/').Length - 1];

                    if (folder_name == name) return node;

                }
                else
                {
                    if (node.Nodes.Count > 0)
                    {
                        TreeNode node_found = FindNodeInParentNodeByValue(name, node);
                        if (node_found != null) return node_found;
                    }
                }
            }

            return null;
        }

        public TreeNode FindNodeInParentNodeByValue(string name, TreeNode parent)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                if (node.Tag is MyYamlScalarNode)
                {
                    MyYamlScalarNode nodeAsScalar = (MyYamlScalarNode)node.Tag;
                    string folder_name = nodeAsScalar.value;

                    if (folder_name.Contains("\\"))
                        folder_name = folder_name.Split('\\')[folder_name.Split('\\').Length - 1];
                    else if (folder_name.Contains("/"))
                        folder_name = folder_name.Split('/')[folder_name.Split('/').Length - 1];

                    if (folder_name == name) return node;
                }
                else
                {
                    TreeNode node_found = FindNodeInParentNodeByValue(name, node);
                    if (node_found != null) return node_found;
                }
            }

            return null;
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
                if (nodeAsScalar.tag == "!include_dir_named" || nodeAsScalar.tag == "!include_dir_merge_list" || nodeAsScalar.tag == "!include_dir_merge_named") return 6;
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
            Logger.Instance.WriteLine("onAfterSelect");
            if (e.Node.Tag is MyYamlScalarNode)
            {
                Logger.Instance.WriteLine("onAfterSelect: is MyYamlScalarNode @ line " + ((MyYamlScalarNode)e.Node.Tag).line.ToString() + " col " + ((MyYamlScalarNode)e.Node.Tag).col.ToString());
                selectedScalarNode = (MyYamlScalarNode)e.Node.Tag;
                nameTextBox.Text = selectedScalarNode.name;
                tagTextBox.Text = selectedScalarNode.tag;
                valueTextBox.Text = selectedScalarNode.value;
                if (selectedScalarNode.name != "") nameTextBox.Enabled = true;
                else nameTextBox.Enabled = false;
                tagTextBox.Enabled = true;
                valueTextBox.Enabled = true;
                updateButton.Enabled = true;
            }
            else
            {
                selectedScalarNode = null;
                nameTextBox.Text = "";
                nameTextBox.Enabled = false;
                tagTextBox.Text = "";
                tagTextBox.Enabled = false;
                valueTextBox.Enabled = false;
                valueTextBox.Text = "";
                updateButton.Enabled = false;
            }
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

        private void onPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Logger.Instance.WriteLine("onPropertyValueChanged");
        }

        private void onUndoClick(object sender, EventArgs e)
        {
            Logger.Instance.WriteLine("onUndoClick");
            Manager.Undo();
        }

        private void onRedoClick(object sender, EventArgs e)
        {
            Logger.Instance.WriteLine("onRedoClick");
            Manager.Redo();
        }

        private void materialFlatButton_LogMenu_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(-100, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            materialContextMenuStrip_LogMenu.Show(ptLowerLeft);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Logger.Instance.WriteLine("updateButton_Click");
            if (selectedScalarNode == null) return;
            MacroCommand macroCommand = new MacroCommand();
            if (selectedScalarNode.name != "") macroCommand.Add(new SetNameCommand(selectedScalarNode, nameTextBox.Text));
            macroCommand.Add(new SetTagCommand(selectedScalarNode, tagTextBox.Text));
            macroCommand.Add(new SetValueCommand(selectedScalarNode, valueTextBox.Text));
            Manager.Execute(macroCommand);
        }

        private void OnAutoSaveTimedEvent(object source, ElapsedEventArgs e)
        {
            Logger.Instance.WriteLine("OnAutoSaveTimedEvent");
            new MyYamlFile().SaveAllFiles();
        }

        private void autoSaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Instance.WriteLine("autoSaveCheckBox_CheckedChanged " + ((CheckBox)sender).CheckState.ToString());
            if (((CheckBox)sender).CheckState == CheckState.Checked) autoSaveTimer.Enabled = true;
            else autoSaveTimer.Enabled = false;
        }

        /// <summary>
        /// Saves the files in a temporary folder and loads them into a YamlStream to validate the changes made
        /// </summary>
        private void toolStripButton_Validate_Click(object sender, EventArgs e)
        {
            if (mainTreeView.Nodes.Count == 0)
            {
                Logger.Instance.WriteLine("There are no files open, cannot validate changes.");
            }
            else
            {
                bool errorfound = false;

                string temporarydirectory = GetTemporaryDirectory("YamlEditor");
                Directory.CreateDirectory(temporarydirectory); //creates a YamlEditor folder in the operating systems temporary foder
                string basedirectory = MyYamlFile.all_files[0].directory; //the first file is always the configuration.yaml
                basedirectory = basedirectory.Remove(basedirectory.Length - 1); //removes the '/' at the end of the directory

                MyYamlFile.SaveAllFilesInNewDirectory(basedirectory, temporarydirectory);

                List<String> folders = new List<string>(); //creates a list of subfolders and an empty folder to represent the base folder
                folders.Add("");
                foreach (var subfolder in Directory.GetDirectories(temporarydirectory))
                    folders.Add(subfolder);

                foreach (string subfolder in folders)
                {
                    string path = subfolder;
                    if (subfolder == "")
                        path = temporarydirectory + "\\" + subfolder;

                    DirectoryInfo d = new DirectoryInfo(Path.GetFullPath(path));//Assuming Test is your Folder
                    FileInfo[] Files = d.GetFiles("*.yaml"); //Getting Text files
                    foreach (FileInfo file in Files)
                    {
                        YamlStream yaml = new YamlStream();
                        try
                        {
                            using (var stream = new StreamReader(file.FullName))
                            {
                                yaml.Load(stream);
                            }
                            Logger.Instance.WriteLine($"Validating:  '\"{file.FullName}\"'.");
                        }
                        catch (Exception exception)
                        {
                            Logger.Instance.WriteLine(exception.Message + " In file '" + file.FullName + "'");
                            errorfound = true;
                        }
                    }
                }

                //Directory.Delete(temporarydirectory, true); //deletes the YamlEditor folder created in the operating systems temporary foder

                if (errorfound)
                    Logger.Instance.WriteLine("VALIDATION: Errors were found while validating the files.");
                else
                    Logger.Instance.WriteLine("VALIDATION: No errors were found while validating the files.");
            }
        }

        public string GetTemporaryDirectory(string foldername)
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), foldername);
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}

//Removes toolstrip border
public partial class ToolStripNoBoder : ToolStripSystemRenderer
{
    public ToolStripNoBoder()
    {
    }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        //base.OnRenderToolStripBorder(e);
    }
}