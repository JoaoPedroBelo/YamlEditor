using System.Collections.Generic;

namespace YamlEditorConsole
{
    public class AddChildrenToList : IAddChildrenStrategy
    {
        public void AddChildren(MyYamlNode child, List<MyYamlNode> nodes)
        {
            nodes.Add(child);
        }
    }
}