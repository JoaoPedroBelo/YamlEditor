using System.Collections.Generic;
using System.Windows.Forms;

namespace Data_Model
{
    public class MyYamlSequenceNode : MyYamlNode
    {
        public override List<MyYamlNode> nodes { get; set; }

        public MyYamlSequenceNode(string name, int indentAmount, MyYamlNode parent) : base(name, indentAmount, parent)
        {
        }

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

            bool all_childs_scalar = true;
            foreach (MyYamlNode node in nodes)
            {
                if (node is MyYamlMappingNode || node is MyYamlSequenceNode)
                    all_childs_scalar = false;
            }

            foreach (MyYamlNode node in nodes)
            {
                if (node.name == "" && node is MyYamlScalarNode)
                {
                    if (all_childs_scalar)
                    {
                        string new_indent = new string(' ', indentAmount + 2);
                        text += new_indent + "- " + node.ToString();
                    }
                    else
                    {
                        text += indent + "- " + node.ToString();
                    }
                }
                else
                {
                    text += indent + "- " + node.ToString().Substring((indentAmount + 2), node.ToString().Length - (indentAmount + 2));
                    string[] all_lines = node.ToString().Substring((indentAmount + 2), node.ToString().Length - (indentAmount + 2)).Split('\n');
                }
            }
            return text;
        }
    }
}