using System.Collections.Generic;
using System;

namespace Data_Model
{
    public class MyYamlSequenceNode : MyYamlNode
    {
        public override List<MyYamlNode> nodes { get; set; }

        public MyYamlSequenceNode(string name, int indentAmount) : base(name, indentAmount) { }

        public override void AddChildren(MyYamlNode child)
        {
            this.nodes.Add(child);
        }

        public override bool Contains(string name)
        {
            foreach (var child in nodes)
            {
                if (child is MyYamlScalarNode)
                {
                    if (child.name == name) return true;
                }
                else child.Contains(name);
            }
            return false;
        }

        public override MyYamlNode GetFirst(string name)
        {
            foreach (var child in nodes)
            {
                if (child is MyYamlScalarNode)
                {
                    if (child.name == name) return child;
                }
                else child.Contains(name);
            }
            return null;
        }

        public override string ToString()
        {
            var indent = new string(' ', indentAmount);
            string text = indent + this.name + ":\n";
            if (string.IsNullOrEmpty(name)) text = "";
            if (nodes.Count == 0) text = indent + this.name + ": []\n";

            indent = new string(' ', indentAmount);

            foreach (MyYamlNode node in nodes)
            {
                if (node.name == "" && node is MyYamlScalarNode)
                {
                    indent = new string(' ', indentAmount + 2);
                    text += indent + "- " + node.ToString();
                    indent = new string(' ', indentAmount);
                }
                else
                {
                    text += indent + "- " + node.ToString().Substring((indentAmount + 2), node.ToString().Length - (indentAmount + 2));
                }
            }

            return text;
        }
    }
}

