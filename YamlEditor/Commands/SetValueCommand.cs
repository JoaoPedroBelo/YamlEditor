using Data_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamlEditor.Commands
{
    class SetValueCommand : ICommand
    {
        private MyYamlScalarNode ScalarNode { get; }
        private string Value { get; set; }

        public SetValueCommand(MyYamlScalarNode aScalarNode, string aValue)
        {
            ScalarNode = aScalarNode;
            Value = aValue;
        }

        public void Execute()
        {
            string value = ScalarNode.value;
            ScalarNode.value = Value;
            Value = value; 
        }

        public void Undo()
        {
            Execute();
        }

        public void Redo()
        {
            Execute();
        }
    }
}
