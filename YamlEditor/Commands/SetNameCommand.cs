using Data_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamlEditor.Commands
{
    class SetNameCommand : ICommand
    {
        private MyYamlScalarNode ScalarNode { get; }
        private string Name { get; set; }

        public SetNameCommand(MyYamlScalarNode aScalarNode, string aName)
        {
            ScalarNode = aScalarNode;
            Name = aName;
        }

        public void Execute()
        {
            string name = ScalarNode.name;
            ScalarNode.name = Name;
            Name = name; 
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
