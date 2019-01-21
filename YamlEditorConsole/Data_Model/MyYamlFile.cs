using System;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.RepresentationModel;
using Logging;

namespace YamlEditorConsole
{
    class YamlFile
    {
        public string fileName { get; private set; } // File name
        public string directory { get; private set; } // File directory
        public YamlStream yaml { get; private set; } // File contents as a YamlStream
        public List<MyYamlNode> nodes = new List<MyYamlNode>();
        public static List<YamlFile> all_files = new List<YamlFile>();
        public int indentAmount = 0;

        public YamlFile(string file_directory)
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
            LoadChildren((YamlMappingNode)yaml.Documents[0].RootNode);

        }

        /// <summary>
        /// Loads other yaml files included in configuration.yaml
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
                    nodes.Add(new MyYamlScalarNode(key.Value, scalar.Tag, scalar.Value, indentAmount));

                    if (scalar.Tag == "!include")
                    {
                        //LoadFile(node, scalar.Value); temos que fazer load aos outros ficheiros
                    }
                }
                else if (child.Value is YamlSequenceNode)
                {
                    nodes.Add(new MyYamlSequenceNode(key.Value, indentAmount));

                    indentAmount += 2;
                    LoadChildren(child.Value as YamlSequenceNode);
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
                    parent.AddChildren(new MyYamlScalarNode(key.Value, scalar.Tag, scalar.Value, indentAmount));

                    if (scalar.Tag == "!include")
                    {
                        //LoadFile(node, scalar.Value); temos que fazer load aos outros ficheiros
                    }
                }
                else if (child.Value is YamlSequenceNode)
                {
                    parent.AddChildren(new MyYamlSequenceNode(key.Value, indentAmount));

                    indentAmount += 2;
                    LoadChildren(child.Value as YamlSequenceNode);
                    indentAmount -= 2;
                }
                else if (child.Value is YamlMappingNode)
                {
                    parent.AddChildren(new MyYamlMappingNode(key.Value, indentAmount));

                    indentAmount += 2;
                    LoadChildren(child.Value as YamlMappingNode);
                    indentAmount -= 2;
                }

            }
        }

        private void LoadChildren(YamlSequenceNode sequence)
        {
            foreach (var child in sequence.Children)
            {
                if (child is YamlSequenceNode)
                {
                    Console.WriteLine("SEQUENCE RECUR: " + child);

                    LoadChildren(child as YamlSequenceNode);
                }
                else if (child is YamlMappingNode)
                {
                    LoadChildren(child as YamlMappingNode);
                }
                else if (child is YamlScalarNode)
                {
                    var scalar = child as YamlScalarNode;
                    Console.WriteLine(child + ":  " + scalar.Tag + " " + scalar.Value);
                }
            }
        }

        /// <summary>
        /// Saves the file
        /// </summary>
        public void SaveFile()
        {

        }

        /// <summary>
        /// Write the yamlStream to the console
        /// </summary>
        public void WriteToConsole()
        {
            foreach (var item in nodes)
            {
                Console.Write(item.ToString());
            }
        }
    }
}
