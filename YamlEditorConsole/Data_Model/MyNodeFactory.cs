using YamlDotNet.Core;

namespace YamlEditorConsole
{
    public static class MyNodeFactory
    {
        public static MyYamlMappingNode CreateMyYamlMappingNode(string name, int indentAmount)
        {
            return new MyYamlMappingNode(name, indentAmount);
        }

        public static MyYamlSequenceNode CreateMyYamlSequenceNode(string name, int indentAmount)
        {
            return new MyYamlSequenceNode(name, indentAmount);
        }

        public static MyYamlScalarNode CreateMyYamlScalarNode(string name, string tag, string value, ScalarStyle style, int indentAmount)
        {
            return new MyYamlScalarNode(name, tag, value, style, indentAmount);
        }
    }
}