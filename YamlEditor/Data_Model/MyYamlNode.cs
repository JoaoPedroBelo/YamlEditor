using System.Collections.Generic;
using YamlEditor.Patterns;

namespace Data_Model
{
    public abstract class MyYamlNode : ISubject
    {
        private string mName;
        public string name
        {
            get { return mName; }
            set { mName = value; Notify(); }
        }
        public int indentAmount { get; private set; }
        public abstract List<MyYamlNode> nodes { get; set; }
        public MyYamlNode parent { get; set; }

        public event UpdateEventHandler OnUpdate;

        public MyYamlNode(string name, int indentAmount, MyYamlNode parent)
        {
            this.name = name;
            this.indentAmount = indentAmount;
            this.nodes = new List<MyYamlNode>();
            this.parent = parent;
        }

        public abstract void AddChildren(MyYamlNode child);

        public abstract bool Contains(string name);

        public abstract MyYamlNode GetFirst(string name);

        public void Notify(object aData = null)
        {
            OnUpdate?.Invoke(this, aData);
        }
    }
}