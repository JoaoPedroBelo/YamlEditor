namespace YamlEditorConsole
{
    public class MyYamlScalarNode : MyYamlNode
    {
        public string value_type { get; private set; }//PLACEHOLDER SOLUTION
        public string value { get; private set; }
        public string tag { get; private set; }
        public int indentAmount { get; private set; }

        public MyYamlScalarNode(string name, string tag, string value, int indentAmount) : base(name)
        {
            this.value = value;
            this.tag = tag;
            this.indentAmount = indentAmount;
            this.value_type = "string";

            int value_int = 0;
            bool successfullyParsedInt = int.TryParse(this.value, out value_int);
            if (successfullyParsedInt) this.value_type = "int";

            bool value_bool = true;
            bool successfullyParsedBool = bool.TryParse(this.value, out value_bool);
            if (successfullyParsedBool) this.value_type = "bool";
            
        }

        public override string ToString()
        {
            var indent = new string(' ', indentAmount);
            return indent + name + ": " + tag + " " + value + '\n';
        }
    }
}
