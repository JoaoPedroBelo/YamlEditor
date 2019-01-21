using System;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.RepresentationModel;
using Logging;

namespace YamlEditorConsole
{
    class MyYamlFile
    {
        public string fileName { get; private set; } // File name
        public string directory { get; private set; } // File directory
        public YamlStream yaml { get; private set; } // File contents as a YamlStream
        public List<MyYamlNode> nodes = new List<MyYamlNode>();
        public static List<MyYamlFile> all_files = new List<MyYamlFile>();
        public int indentAmount = 0;

        public MyYamlFile(string file_directory)
        {
            var file_directory_split = file_directory.Split("/");
            this.fileName = file_directory_split[file_directory_split.Length - 1];
            foreach (var text in file_directory_split)
            {
                if (text != file_directory_split[file_directory_split.Length - 1]) this.directory += text + "/";
            }
            LoadFile(file_directory); // sets the yaml value

            all_files.Add(this);

            Logger.Instance.Recorder = new Logging.DateRecorderDecorator(new CounterDecorator(new ConsoleRecorder()));
        }

        /// <summary>
        /// Loads configuration.yaml file and sets the yaml property value
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
            }
            catch (Exception exception)
            {
                Logger.Instance.WriteLine(exception.Message);
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
                System.Diagnostics.Trace.Assert(key != null);

                if (child.Value is YamlScalarNode)
                {
                    var scalar = child.Value as YamlScalarNode;
                    nodes.Add(new MyYamlScalarNode(key.Value, scalar.Tag, scalar.Value, scalar.Style, indentAmount));

                    if (scalar.Tag == "!include")
                    {
                        if (File.Exists(directory + scalar.Value)) new MyYamlFile(directory + scalar.Value);
                        else Logger.Instance.WriteLine("Could not find file '" + directory + scalar.Value + "'.");
                    }
                    if (scalar.Tag == "!include_dir_named")
                    {
                        string[] files = System.IO.Directory.GetFiles(directory + scalar.Value + "/", "*.yaml");
                        foreach (var value in files)
                        {
                            var value_split = value.Split("/");
                            var file_to_import = value_split[value_split.Length - 1];
                            if (File.Exists(directory + scalar.Value + "/" + file_to_import)) new MyYamlFile(directory + scalar.Value + "/" + file_to_import);
                            else Logger.Instance.WriteLine("Could not find file '" + directory + scalar.Value + "/" + file_to_import + "'.");
                        }

                    }
                }
                else if (child.Value is YamlSequenceNode)
                {
                    nodes.Add(new MyYamlSequenceNode(key.Value, indentAmount));

                    indentAmount += 2;
                    MyYamlSequenceNode parent = (MyYamlSequenceNode)nodes[nodes.Count - 1];
                    LoadChildren(child.Value as YamlSequenceNode, parent);
                    indentAmount -= 2;
                }
                else if (child.Value is YamlMappingNode)
                {
                    nodes.Add(new MyYamlMappingNode(key.Value, indentAmount));

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
                System.Diagnostics.Trace.Assert(key != null);

                if (child is YamlMappingNode)
                {
                    nodes.Add(new MyYamlMappingNode("", indentAmount));
                    MyYamlMappingNode root = (MyYamlMappingNode)nodes[nodes.Count - 1];

                    root.AddChildren(new MyYamlSequenceNode("", indentAmount));
                    MyYamlSequenceNode first_sequence = (MyYamlSequenceNode)root.nodes[root.nodes.Count - 1];

                    first_sequence.AddChildren(new MyYamlMappingNode("", indentAmount));

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
                System.Diagnostics.Trace.Assert(key != null);

                if (child.Value is YamlScalarNode)
                {
                    var scalar = child.Value as YamlScalarNode;
                    parent.AddChildren(new MyYamlScalarNode(key.Value, scalar.Tag, scalar.Value, scalar.Style, indentAmount));

                    if (scalar.Tag == "!include")
                    {
                        if (File.Exists(directory + scalar.Value)) new MyYamlFile(directory + scalar.Value);
                        else Logger.Instance.WriteLine("Could not find file '" + directory + scalar.Value + "'.");
                    }
                    if (scalar.Tag == "!include_dir_named")
                    {
                        string[] files = System.IO.Directory.GetFiles(directory + scalar.Value + "/", "*.yaml");
                        foreach (var value in files)
                        {
                            var value_split = value.Split("/");
                            var file_to_import = value_split[value_split.Length - 1];
                            if (File.Exists(directory + scalar.Value + "/" + file_to_import)) new MyYamlFile(directory + scalar.Value + "/" + file_to_import);
                            else Logger.Instance.WriteLine("Could not find file '" + directory + scalar.Value + "/" + file_to_import + "'.");
                        }

                    }
                }
                else if (child.Value is YamlSequenceNode)
                {
                    parent.AddChildren(new MyYamlSequenceNode(key.Value, indentAmount));

                    indentAmount += 2;
                    MyYamlSequenceNode new_parent = (MyYamlSequenceNode)parent.nodes[parent.nodes.Count - 1];
                    LoadChildren(child.Value as YamlSequenceNode, new_parent);
                    indentAmount -= 2;
                }
                else if (child.Value is YamlMappingNode)
                {
                    parent.AddChildren(new MyYamlMappingNode(key.Value, indentAmount));

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
                    parent.AddChildren(new MyYamlScalarNode("", scalar.Tag, scalar.Value, scalar.Style, indentAmount));

                    //CANT HAVE INCLUDE IN SEQUENCE CHILDREN???
                    /* if (scalar.Tag == "!include")
                    {
                        LoadFile(node, scalar.Value);
                    } */
                }
                else if (child is YamlSequenceNode)
                {
                    parent.AddChildren(new MyYamlSequenceNode("", indentAmount));

                    MyYamlSequenceNode new_parent = (MyYamlSequenceNode)parent.nodes[parent.nodes.Count - 1];
                    LoadChildren(child as YamlSequenceNode, new_parent);
                }
                else if (child is YamlMappingNode)
                {
                    parent.AddChildren(new MyYamlMappingNode("", indentAmount));

                    MyYamlMappingNode new_parent = (MyYamlMappingNode)parent.nodes[parent.nodes.Count - 1];
                    LoadChildren(child as YamlMappingNode, new_parent);
                }
            }
        }

        /// <summary>
        /// Saves the file
        /// </summary>
        public void SaveFile()
        {
            File.WriteAllText(directory + fileName, ToString());
            //File.WriteAllText("C:/Users/DFmar/OneDrive/Desktop/Test.yaml", ToString());
        }

        public void SaveAllFiles()
        {
            foreach (MyYamlFile file in all_files)
            {

                file.SaveFile();
            }
        }

        /// <summary>
        /// Returns the yamlStream
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
    }
}
