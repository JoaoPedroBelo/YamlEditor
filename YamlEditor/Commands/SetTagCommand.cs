using Data_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamlEditor.Commands
{
    class SetTagCommand : ICommand
    {
        private MyYamlScalarNode ScalarNode { get; }
        private string Tag { get; set; }

        public SetTagCommand(MyYamlScalarNode aScalarNode, string aTag)
        {
            ScalarNode = aScalarNode;
            Tag = aTag;
        }

        public void Execute()
        {
            string tag = ScalarNode.tag;
            ScalarNode.tag = Tag;
            Tag = tag;
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
