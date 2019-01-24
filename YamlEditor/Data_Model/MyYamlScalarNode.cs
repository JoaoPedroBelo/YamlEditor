using System.Collections.Generic;
using YamlDotNet.Core;

namespace Data_Model
{
    public class MyYamlScalarNode : MyYamlNode
    {
        public string value_type { get; set; }//PLACEHOLDER SOLUTION
        private string mValue { get; set; }
        public string value
        {
            get { return mValue; }
            set { mValue = value; Notify(); }
        }
        private string mTag { get; set; }
        public string tag
        {
            get { return mTag; }
            set { mTag = value; Notify(); }
        }
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

        public override void AddChildren(MyYamlNode child)
        {
        }

        public override bool Contains(string name)
        {
            return false;
        }

        public override MyYamlNode GetFirst(string name)
        {
            return null;
        }

        public override string ToString()
        {
            var indent = new string(' ', indentAmount);

            string print_tag = tag;
            if (!(string.IsNullOrEmpty(tag))) print_tag += " ";

            string print_value = value;
            if (style == ScalarStyle.SingleQuoted) print_value = "'" + value + "'";
            else if (style == ScalarStyle.DoubleQuoted) print_value = "\"" + value + "\"";
            else if (style == ScalarStyle.Folded)
            {
                print_value = ">\n";
                var value_lines = value.Split('\n');
                foreach (string line in value_lines)
                {
                    if (line == value_lines[0] || line == value_lines[value_lines.Length - 1])
                        print_value += indent + "  " + line;
                    else
                        print_value += indent + "    " + line;
                }
            }

            string text = indent + name + ": " + print_tag + print_value + '\n';
            if (name == "") text = print_tag + print_value + '\n';
            return text;
        }
    }
}