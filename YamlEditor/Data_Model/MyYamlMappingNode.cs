using System.Collections.Generic;

namespace Data_Model
{
    public class MyYamlMappingNode : MyYamlNode
    {
        public override List<MyYamlNode> nodes { get; set; }

        public MyYamlMappingNode(string name, int indentAmount) : base(name, indentAmount)
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