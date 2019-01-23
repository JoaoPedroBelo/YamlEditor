using System.Collections.Generic;
using System;

namespace Data_Model
{
    public abstract class MyYamlNode
    {
        public string name { get; set; }
        public int indentAmount { get; private set; }
        public abstract List<MyYamlNode> nodes { get; set; }

        public MyYamlNode(string name, int indentAmount)
        {
            this.name = name;
            this.indentAmount = indentAmount;
            this.nodes = new List<MyYamlNode>();
        }

        public abstract void AddChildren(MyYamlNode child);
        public abstract bool Contains(string name);
        public abstract MyYamlNode GetFirst(string name);
    }
}