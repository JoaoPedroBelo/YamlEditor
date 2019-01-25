using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;
using System.Windows.Forms;
using System.Linq;

namespace Data_Model
{
    public class MyYamlFile
    {
        public string fileName { get; private set; } // File name
        public string directory { get; private set; } // File directory
        public YamlStream yaml { get; private set; } // File contents as a YamlStream
        public List<MyYamlNode> nodes = new List<MyYamlNode>();
        public static List<MyYamlFile> all_files = new List<MyYamlFile>();
        public int indentAmount = 0;

        public MyYamlFile()
        {
        }

        public MyYamlFile(string file_directory)
        {
            this.directory = (Path.GetDirectoryName(file_directory) ?? "") + "\\";
            this.fileName = Path.GetFileName(file_directory);

            LoadFile(file_directory);

            all_files.Add(this);
        }

        /// <summary>
        /// Loads a yaml file and sets the yaml property value
        /// </summary>
        private void LoadFile(string filename)
        {
            yaml = new YamlStream();
            try
            {
                using (var stream = new StreamReader(filename))
                {
                    yaml.Load(stream);
                }
                Logger.Instance.WriteLine($"File  \"{directory + fileName}\" opened.");
            }
            catch (Exception exception)
            {
                Logger.Instance.WriteLine(exception.Message + " In file '" + filename + "'");
            }

            if (yaml.Documents.Count == 0) return;

            try
            {
                LoadChildren((YamlMappingNode)yaml.Documents[0].RootNode);
            }
            catch (Exception) { }

            try
            {
                LoadChildren((YamlSequenceNode)yaml.Documents[0].RootNode);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Loads all the nodes and files included in configuration
        /// </summary>
        private void LoadChildren(YamlMappingNode mapping)
        {
            var children = mapping?.Children;
            if (children == null) return;

            foreach (var child in children)
            {
                var key = child.Key as YamlScalarNode;
                //System.Diagnostics.Trace.Assert(key != null);

                //alias/anchor error on yamlStream fix:
                if (key.Value[0] == '*' || key.Value[key.Value.Length - 1] == '*')
                    key.Value = "\"" + key.Value + "\"";

                if (child.Value is YamlScalarNode)
                {
                    var scalar = child.Value as YamlScalarNode;
                    //Fix yamlStream syntax error
                    //removes unnecessary '/' after folder name in includes 


                    if (!(string.IsNullOrEmpty(scalar.Value)) && (scalar.Value[scalar.Value.Length - 1] == '\\' || scalar.Value[scalar.Value.Length - 1] == '/'))
                        scalar.Value = scalar.Value.Remove(scalar.Value.Length - 1);

                    //if the value is single quoted and contains a "'" we need to replace it with "''"
                    if (scalar.Value.Contains("'") && scalar.Style == YamlDotNet.Core.ScalarStyle.SingleQuoted)
                        scalar.Value = scalar.Value.Replace("'", "''");

                    //if the value is double quoted and contains a '"' we need to replace it with '"'
                    if (scalar.Value.Contains("\"") && scalar.Style == YamlDotNet.Core.ScalarStyle.DoubleQuoted)
                        scalar.Value = scalar.Value.Replace("\"", "\\\"");

                    nodes.Add(MyNodeFactory.CreateMyYamlScalarNode(key.Value, scalar.Tag, scalar.Value, scalar.Style, indentAmount, null, key.Start.Line, key.Start.Column));

                    if (scalar.Tag == "!include")
                    {
                        if (File.Exists(directory + scalar.Value)) MyYamlFileFactory.CreateMyYamlFile(directory + scalar.Value);
                        else Logger.Instance.WriteLine("Could not find file '" + directory + scalar.Value + "'.");
                    }
                    else if (scalar.Tag == "!include_dir_named" || scalar.Tag == "!include_dir_merge_named" || scalar.Tag == "!include_dir_merge_list")
                    {
                        CreateDirectoryIfDoesntExist(directory, scalar.Value);

                        string[] files = System.IO.Directory.GetFiles(directory + scalar.Value + "\\", "*.yaml");
                        foreach (var value in files)
                        {
                            var file_to_import = Path.GetFileName(value);
                            if (File.Exists(directory + scalar.Value + "\\" + file_to_import)) MyYamlFileFactory.CreateMyYamlFile(directory + scalar.Value + "\\" + file_to_import);
                            else Logger.Instance.WriteLine("Could not find file '" + directory + scalar.Value + "\\" + file_to_import + "'.");
                        }
                    }
                }
                else if (child.Value is YamlSequenceNode)
                {
                    nodes.Add(MyNodeFactory.CreateMyYamlSequenceNode(key.Value, indentAmount, null));

                    indentAmount += 2;
                    MyYamlSequenceNode parent = (MyYamlSequenceNode)nodes[nodes.Count - 1];
                    LoadChildren(child.Value as YamlSequenceNode, parent);
                    indentAmount -= 2;
                }
                else if (child.Value is YamlMappingNode)
                {
                    nodes.Add(MyNodeFactory.CreateMyYamlMappingNode(key.Value, indentAmount, null));

                    indentAmount += 2;
                    MyYamlMappingNode parent = (MyYamlMappingNode)nodes[nodes.Count - 1];
                    LoadChildren(child.Value as YamlMappingNode, parent);
                    indentAmount -= 2;
                }
            }
        }

        //In case there is a file with a sequence node in the beginning
        private void LoadChildren(YamlSequenceNode mapping)
        {
            var children = mapping?.Children;
            if (children == null) return;

            foreach (var child in children)
            {
                var key = child as YamlScalarNode;

                if (child is YamlMappingNode)
                {
                    nodes.Add(MyNodeFactory.CreateMyYamlMappingNode("", indentAmount, null));
                    MyYamlMappingNode root = (MyYamlMappingNode)nodes[nodes.Count - 1];

                    root.AddChildren(MyNodeFactory.CreateMyYamlSequenceNode("", indentAmount, root));
                    MyYamlSequenceNode first_sequence = (MyYamlSequenceNode)root.nodes[root.nodes.Count - 1];

                    first_sequence.AddChildren(MyNodeFactory.CreateMyYamlMappingNode("", indentAmount, first_sequence));

                    indentAmount += 2;
                    MyYamlMappingNode parent = (MyYamlMappingNode)first_sequence.nodes[first_sequence.nodes.Count - 1];
                    LoadChildren(child as YamlMappingNode, parent);
                    indentAmount -= 2;
                }
            }
        }

        //OVERLOADING
        private void LoadChildren(YamlMappingNode mapping, MyYamlNode parent)
        {
            var children = mapping?.Children;
            if (children == null) return;

            foreach (var child in children)
            {
                var key = child.Key as YamlScalarNode;

                //alias/anchor error on yamlStream fix:
                if (key.Value[0] == '*' || key.Value[key.Value.Length - 1] == '*')
                    key.Value = "\"" + key.Value + "\"";

                if (child.Value is YamlScalarNode)
                {
                    var scalar = child.Value as YamlScalarNode;
                    //Fix yamlStream syntax error
                    //removes unnecessary '/' after folder name in includes 
                    if (!(string.IsNullOrEmpty(scalar.Value)) && (scalar.Value[scalar.Value.Length - 1] == '\\' || scalar.Value[scalar.Value.Length - 1] == '/'))
                        scalar.Value = scalar.Value.Remove(scalar.Value.Length - 1);

                    //if the value is single quoted and contains a "'" we need to replace it with "''"
                    if (scalar.Value.Contains("'") && scalar.Style == YamlDotNet.Core.ScalarStyle.SingleQuoted)
                        scalar.Value = scalar.Value.Replace("'", "''");

                    //if the value is double quoted and contains a '"' we need to replace it with '"'
                    if (scalar.Value.Contains("\"") && scalar.Style == YamlDotNet.Core.ScalarStyle.DoubleQuoted)
                        scalar.Value = scalar.Value.Replace("\"", "\\\"");

                    parent.AddChildren(MyNodeFactory.CreateMyYamlScalarNode(key.Value, scalar.Tag, scalar.Value, scalar.Style, indentAmount, parent, key.Start.Line, key.Start.Column));

                    if (scalar.Tag == "!include")
                    {
                        if (File.Exists(directory + scalar.Value)) MyYamlFileFactory.CreateMyYamlFile(directory + scalar.Value);
                        else Logger.Instance.WriteLine("Could not find file '" + directory + scalar.Value + "'.");
                    }
                    if (scalar.Tag == "!include_dir_named" || scalar.Tag == "!include_dir_merge_named" || scalar.Tag == "!include_dir_merge_list")
                    {
                        CreateDirectoryIfDoesntExist(directory, scalar.Value);

                        string[] files = System.IO.Directory.GetFiles(directory + scalar.Value + "\\", "*.yaml");
                        foreach (var value in files)
                        {
                            var file_to_import = Path.GetFileName(value);
                            if (File.Exists(directory + scalar.Value + "\\" + file_to_import)) MyYamlFileFactory.CreateMyYamlFile(directory + scalar.Value + "\\" + file_to_import);
                            else Logger.Instance.WriteLine("Could not find file '" + directory + scalar.Value + "\\" + file_to_import + "'.");
                        }
                    }
                }
                else if (child.Value is YamlSequenceNode)
                {
                    parent.AddChildren(MyNodeFactory.CreateMyYamlSequenceNode(key.Value, indentAmount, parent));

                    indentAmount += 2;
                    MyYamlSequenceNode new_parent = (MyYamlSequenceNode)parent.nodes[parent.nodes.Count - 1];
                    LoadChildren(child.Value as YamlSequenceNode, new_parent);
                    indentAmount -= 2;
                }
                else if (child.Value is YamlMappingNode)
                {
                    parent.AddChildren(MyNodeFactory.CreateMyYamlMappingNode(key.Value, indentAmount, parent));

                    indentAmount += 2;
                    MyYamlMappingNode new_parent = (MyYamlMappingNode)parent.nodes[parent.nodes.Count - 1];
                    LoadChildren(child.Value as YamlMappingNode, new_parent);
                    indentAmount -= 2;
                }
            }
        }

        //OVERLOADING
        private void LoadChildren(YamlSequenceNode sequence, MyYamlNode parent)
        {
            foreach (var child in sequence.Children)
            {
                if (child is YamlScalarNode)
                {
                    var scalar = child as YamlScalarNode;
                    parent.AddChildren(MyNodeFactory.CreateMyYamlScalarNode("", scalar.Tag, scalar.Value, scalar.Style, indentAmount, parent, scalar.Start.Line, scalar.Start.Column));
                }
                else if (child is YamlSequenceNode)
                {
                    parent.AddChildren(MyNodeFactory.CreateMyYamlSequenceNode("", indentAmount, parent));

                    MyYamlSequenceNode new_parent = (MyYamlSequenceNode)parent.nodes[parent.nodes.Count - 1];
                    LoadChildren(child as YamlSequenceNode, new_parent);
                }
                else if (child is YamlMappingNode)
                {
                    parent.AddChildren(MyNodeFactory.CreateMyYamlMappingNode("", indentAmount, parent));

                    MyYamlMappingNode new_parent = (MyYamlMappingNode)parent.nodes[parent.nodes.Count - 1];
                    LoadChildren(child as YamlMappingNode, new_parent);
                }
            }
        }

        public void CreateDirectoryIfDoesntExist(string directory, string value)
        {
            //Create directory if doesnt exists
            if (value.Contains("/"))
            {
                string[] folders = value.Split('/');
                string current_folder = folders[0];
                foreach (string folder in folders)
                {
                    if (folder != folders[0])
                        current_folder += '\\' + folder;

                    System.IO.Directory.CreateDirectory(directory + current_folder);
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(directory + value);
            }
        }

        public bool StringContainsCharacter(string text, char character)
        {
            foreach (char x in text)
            {
                if (x == character) return true;
            }
            return false;
        }

        /// <summary>
        /// Saves the file
        /// </summary>
        public void SaveFile()
        {
            File.WriteAllText(directory + fileName, ToString());
            Logger.Instance.WriteLine($"File saved: '{ directory + fileName }'");
        }

        public void SaveFileReplacingValues()
        {
            StreamReader mReadFile = new StreamReader(directory + fileName);
            List<string> mLines = new List<string>();
            string mLine;
            while((mLine = mReadFile.ReadLine()) != null)
            {
                mLines.Add(mLine);
            }
            mReadFile.Close();
            foreach (MyYamlNode node in nodes)
            {
                if (node is MyYamlScalarNode)
                {
                    Logger.Instance.WriteLine("SaveFileReplacingValues: Node: " + node.name);
                    MyYamlScalarNode nodeAsScalar = (MyYamlScalarNode)node;
                    string newLine = mLines[nodeAsScalar.line - 1].Substring(0, nodeAsScalar.col - 1);
                    newLine += nodeAsScalar.name + ":";
                    if (nodeAsScalar.tag != null && nodeAsScalar.tag.Length > 0) newLine += " " + nodeAsScalar.tag;
                    newLine += " " + nodeAsScalar.value;
                    mLines[nodeAsScalar.line - 1] = newLine;
                }
            }
            File.WriteAllLines(directory + fileName, mLines.ToArray());
        }

        /// <summary>
        /// Saves the files
        /// </summary>
        public void SaveAllFiles()
        {
            foreach (MyYamlFile file in all_files)
            {
                //file.SaveFile();
                file.SaveFileReplacingValues();
            }
            Logger.Instance.WriteLine("All files saved.");
        }

        /// <summary>
        /// Returns the all the file data as string
        /// </summary>
        public override string ToString()
        {
            string text = "";
            foreach (var item in nodes)
            {
                text += item.ToString();
            }
            return text;
        }

        /// <summary>
        /// Returns the all nodes data as string for testing purposes
        /// </summary>
        public string All_Nodes_To_String()
        {
            string text = "";
            foreach (MyYamlNode node in nodes)
            {
                text += node.name + " - " + node.GetType() + '\n';
            }
            return text;
        }

        /// <summary>
        /// Returns the all the files data as string for testing purposes
        /// </summary>
        public static string All_Files_Directory_ToString()
        {
            string text = "";
            foreach (MyYamlFile file in all_files)
            {
                text += file.directory + " - " + file.fileName + '\n';
            }
            return text;
        }

        /// <summary>
        /// Returns the all the files data as string for testing purposes
        /// </summary>
        public static string All_Files_ToString()
        {
            string text = "";
            foreach (MyYamlFile file in all_files)
            {
                text += file.ToString();
            }
            return text;
        }
    }
}