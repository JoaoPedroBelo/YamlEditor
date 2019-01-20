using System;
using System.Collections.Generic;
using System.Linq;
using Logging;

namespace YamlEditorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Instance.Recorder = new Logging.DateRecorderDecorator(new CounterDecorator(new ConsoleRecorder()));

            //Data_Model testing:
            /* List<MyYamlNode> lista = new List<MyYamlNode>();

            MyYamlMappingNode weblink = new MyYamlMappingNode("Weblink", 0);

            MyYamlSequenceNode entities = new MyYamlSequenceNode("Entities", 2);

            weblink.AddChildren(entities);

            MyYamlMappingNode weblink2 = new MyYamlMappingNode("", 0);

            entities.AddChildren(weblink2);

            entities.AddChildren(weblink2);

            MyYamlScalarNode name = new MyYamlScalarNode("name", "", "Home Assistant Webpage", 4);
            MyYamlScalarNode url = new MyYamlScalarNode("url", "", "url1", 4);
            MyYamlScalarNode icon = new MyYamlScalarNode("icon", "", "icon1", 4);

            weblink2.AddChildren(name);
            weblink2.AddChildren(url);
            weblink2.AddChildren(icon);

            MyYamlMappingNode group = new MyYamlMappingNode("group", 0);

            lista.Add(weblink);
            lista.Add(group);

            foreach (var item in lista)
            {
                Console.Write(item.ToString());
            } */

            var configuration = new YamlFile("C:/Users/Dfmar/source/repos/YamlEditor/Examples/Apocrathia/homeassistant/configuration.yaml");

            configuration.WriteToConsole();

        }
    }
}