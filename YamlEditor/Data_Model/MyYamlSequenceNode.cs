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

            foreach (MyYamlNode node in nodes)
            {
                if (node.nodes != null)
                MessageBox.Show("" + node.nodes.Count + " ___ " + node.nodes[0].name);

                if (node.nodes != null && node.nodes.Count <= 1 && node.name == "" && node is MyYamlScalarNode)
                {
                    text += indent + "- " + node.ToString();
                }
                else if (node.name == "" && node is MyYamlScalarNode)
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