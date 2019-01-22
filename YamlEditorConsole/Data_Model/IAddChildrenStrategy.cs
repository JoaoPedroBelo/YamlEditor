using System.Collections.Generic;

namespace YamlEditorConsole
{
    public interface IAddChildrenStrategy
    {
        void AddChildren(MyYamlNode child, List<MyYamlNode> nodes);
    }
}