using System.Collections.Generic;
using System;
namespace YamlEditorConsole
{
    public abstract class MyYamlNode
    {
        public string name { get; private set; }
        public int indentAmount { get; private set; }
        public abstract List<MyYamlNode> nodes { get; set; }

        public MyYamlNode(string name, int indentAmount)
        {
            this.name = name;
            this.indentAmount = indentAmount;
            this.nodes = new List<MyYamlNode>();
        }

        public abstract void AddChildren(MyYamlNode child);
    }
}