using System.Collections.Generic;

namespace YamlEditorConsole
{
    public class AddChildrenScalarNode : IAddChildrenStrategy
    {
        public void AddChildren(MyYamlNode child, List<MyYamlNode> nodes)
        {
            nodes.Add(child);
        }
    }
}