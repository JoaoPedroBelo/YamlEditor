using YamlDotNet.Core;

namespace Data_Model
{
    public static class MyNodeFactory
    {
        public static MyYamlMappingNode CreateMyYamlMappingNode(string name, int indentAmount, MyYamlNode parent)
        {
            return new MyYamlMappingNode(name, indentAmount, parent);
        }

        public static MyYamlSequenceNode CreateMyYamlSequenceNode(string name, int indentAmount, MyYamlNode parent)
        {
            return new MyYamlSequenceNode(name, indentAmount, parent);
        }

        public static MyYamlScalarNode CreateMyYamlScalarNode(string name, string tag, string value, ScalarStyle style, int indentAmount, MyYamlNode parent, int aLine, int aCol)
        {
            return new MyYamlScalarNode(name, tag, value, style, indentAmount, parent, aLine, aCol);
        }
    }
}