using System.Collections.Generic;

namespace YamlEditorConsole
{
    class MyYamlMappingNode : MyYamlNode
    {
        public List<MyYamlNode> nodes { get; set; }
        public int indentAmount { get; private set; }

        public MyYamlMappingNode(string name, int indentAmount) : base (name)
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
            if (name == "") text = ""; //If is empty overwrites previous value
            foreach (MyYamlNode node in nodes)
            {
                text += node.ToString();
            }

            return text;
        }
    }
}