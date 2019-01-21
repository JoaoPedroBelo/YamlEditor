using System.Collections.Generic;
using System;
namespace YamlEditorConsole
{
    public class MyYamlMappingNode : MyYamlNode
    {


        public MyYamlMappingNode(string name, int indentAmount) : base(name, indentAmount) { }

        public override string ToString()
        {
            var indent = new string(' ', indentAmount);
            string text = indent + this.name + ":\n";
            if (name == "") text = ""; //If is empty overwrites previous value
            if (name == "-") text = "-"; //In case there is a sequence node in the beggining of the file it is read as a mapping node
            foreach (MyYamlNode node in nodes)
            {
                text += node.ToString();
            }
            return text;
        }
    }
}