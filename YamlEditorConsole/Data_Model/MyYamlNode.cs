using System.Collections.Generic;
using System;
namespace YamlEditorConsole
{
    public class MyYamlNode
    {
        public string name { get; private set; }
        public int indentAmount { get; private set; }
        public virtual List<MyYamlNode> nodes { get; set; }

        public MyYamlNode(string name, int indentAmount)
        {
            this.name = name;
            this.nodes = new List<MyYamlNode>();
            this.indentAmount = indentAmount;
        }

        public virtual void AddChildren(MyYamlNode child)
        {
            this.nodes.Add(child);
        }
    }
}
//USAR STRATEGY DEFINIR ADDCHILDREN IGUAL PARA MAPPING NODE E SEQUENCE NODE