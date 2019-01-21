using System.Collections.Generic;

namespace YamlEditorConsole
{
    public class MyYamlSequenceNode : MyYamlNode
    {
        public List<MyYamlNode> nodes { get; set; }
        public int indentAmount { get; private set; }

        public MyYamlSequenceNode(string name, int indentAmount) : base(name)
        {
            this.nodes = new List<MyYamlNode>();
            this.indentAmount = indentAmount;
        }

        public void AddChildren(MyYamlNode child)
        {
            this.nodes.Add(child);
        }

        public override string ToString()
        {
            var indent = new string(' ', indentAmount);
            string text = indent + this.name + ":\n";
            foreach (MyYamlNode node in nodes)
            {
                text += indent + "- " + node.ToString().Substring((indentAmount + 2), node.ToString().Length - (indentAmount + 2));
            }

            return text;
        }
    }
}

