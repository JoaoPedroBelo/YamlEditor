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
        public static List<YamlFile> all_files = new List<YamlFile>(); 
        public YamlFile(string file_directory)
        {
            var file_directory_split = file_directory.Split("/");
            this.fileName = file_directory_split[file_directory_split.Length - 1];
            foreach (var text in file_directory_split)
            {
                if (text != file_directory_split[file_directory_split.Length - 1]) this.directory += text + "/";
            }
            LoadFile(file_directory); // sets the yaml value

            Logger.Instance.Recorder = new Logging.DateRecorderDecorator(new CounterDecorator(new ConsoleRecorder()));

            all_files.Add(this);
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

                    Console.WriteLine("-" + key.Value + ":  " + scalar.Tag + scalar.Value);

                    if (scalar.Tag == "!include")
                    {
                        //LoadFile(node, scalar.Value); temos que fazer load aos outros ficheiros
                    }
                }
                else if (child.Value is YamlSequenceNode)
                {
                    Console.WriteLine("::" + key.Value);

                    LoadChildren(child.Value as YamlSequenceNode);
                }
                else if (child.Value is YamlMappingNode)
                {
                    Console.WriteLine("_" + key.Value);

                    LoadChildren(child.Value as YamlMappingNode);
                }

            }
        }

        private void LoadChildren(YamlSequenceNode sequence)
        {
            foreach (var child in sequence.Children)
            {
                if (child is YamlSequenceNode)
                {
                    Console.WriteLine(child);

                    LoadChildren(child as YamlSequenceNode);
                }
                else if (child is YamlMappingNode)
                {
                    //Console.WriteLine(child);

                    LoadChildren(child as YamlMappingNode);
                }
                else if (child is YamlScalarNode)
                {
                    var scalar = child as YamlScalarNode;
                    Console.WriteLine(child + ":  " + scalar.Value);
                }
            }
        }

        /// <summary>
        /// Saves the yamlStream to the file
        /// </summary>
        public void ChangeProperty(string property, string value)
        {
            var mapping = yaml.Documents[0].RootNode as YamlMappingNode;
            var children = mapping?.Children;
            if (children == null)
            {
                Logger.Instance.WriteLine("Can´t change property value.");
                return;
            }

            foreach (var child in children)
            {
                var key = child.Key as YamlScalarNode;
                System.Diagnostics.Trace.Assert(key != null);

                /* foreach (KeyValuePair<YamlNode, YamlNode> kvp in child)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                } */
            }

        }

        /// <summary>
        /// Saves the yamlStream to the file
        /// </summary>
        public void SaveFile()
        {
            using (TextWriter writer = File.CreateText(this.directory + this.fileName))
            {
                try
                {
                    yaml.Save(writer, false);
                    Logger.Instance.WriteLine("File written to \"" + this.directory + this.fileName + "\".");
                }
                catch (Exception exception)
                {
                    Logger.Instance.WriteLine(exception.Message);
                }
            }
        }

        /// <summary>
        /// Write the yamlStream to the console
        /// </summary>
        public void WriteToConsole()
        {
            //Console.WriteLine(yamlStream.Documents[0].RootNode);


            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
            foreach (var entry in mapping.Children)
            {
                Console.WriteLine(((YamlScalarNode)entry.Key).Value);

                try
                {
                    var items = (YamlMappingNode)mapping.Children[new YamlScalarNode(((YamlScalarNode)entry.Key).Value)];
                    foreach (var item in items.Children)
                    {
                        Console.WriteLine(((YamlScalarNode)item.Key).Value);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Empty node");
                }                 

                Console.WriteLine();
                Console.WriteLine();
            }

            /* var items2 = (YamlSequenceNode)items.Children[new YamlScalarNode("entities")];
            foreach (YamlMappingNode item in items2)
            {
                Console.WriteLine(
                    "{0}\t{1}",
                    item.Children[new YamlScalarNode("name")],
                    item.Children[new YamlScalarNode("url")]
                );
            } */

        }
    }
}