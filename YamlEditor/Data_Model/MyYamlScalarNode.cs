using System;
using System.Collections.Generic;
using YamlDotNet.Core;

namespace Data_Model
{
    public class MyYamlScalarNode : MyYamlNode
    {
        public string value_type { get; private set; }//PLACEHOLDER SOLUTION
        public string value { get; private set; }
        public string tag { get; private set; }
        public ScalarStyle style;
        public override List<MyYamlNode> nodes { get; set; }

        public MyYamlScalarNode(string name, string tag, string value, ScalarStyle style, int indentAmount) : base(name, indentAmount)
        {
            this.value = value;
            this.tag = tag;
            this.value_type = "string";
            this.style = style;

            int value_int = 0;
            bool successfullyParsedInt = int.TryParse(this.value, out value_int);
            if (successfullyParsedInt) this.value_type = "int";

            bool value_bool = true;
            bool successfullyParsedBool = bool.TryParse(this.value, out value_bool);
            if (successfullyParsedBool) this.value_type = "bool";

            this.nodes = null;
        }

        public override void AddChildren(MyYamlNode child) { }

        public override string ToString()
        {
            string print_tag = tag;
            if (!(string.IsNullOrEmpty(tag))) print_tag += " ";

            string print_value = value;
            if (style == ScalarStyle.SingleQuoted) print_value = "'" + value + "'";
            else if (style == ScalarStyle.DoubleQuoted) print_value = "\"" + value + "\"";

            var indent = new string(' ', indentAmount);
            string text = indent + name + ": " + print_tag + print_value + '\n';
            if (name == "") text = print_tag + print_value + '\n';
            return text;
        }
    }
}
