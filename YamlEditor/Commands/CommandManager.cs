using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamlEditor.Commands
{
    public class CommandManager : ICommandManager
    {
        protected List<ICommand> Commands { get; } = new List<ICommand>();
        protected int Position { get; set; } = -1;

        public bool HasUndo()
        {
            return (Position > -1);
        }

        public bool HasRedo()
        {
            return (Position < Commands.Count - 1);
        }

        public void Undo()
        {
            if (!HasUndo()) return;
            Logging.Logger.Instance.WriteLine("CommandManager: Undo");
            Commands[Position].Undo();
            Position--;
        }

        public void Redo()
        {
            if (!HasRedo()) return;
            Logging.Logger.Instance.WriteLine("CommandManager: Redo");
            Position++;
            Commands[Position].Redo();
        }

        public void Execute(ICommand aCommand)
        {
            if (HasRedo())
            {
                Commands.RemoveRange(Position + 1, Commands.Count - Position - 1);
            }
            Logging.Logger.Instance.WriteLine("CommandManager: Execute");
            aCommand.Execute();
            Commands.Add(aCommand);
            Position = Commands.Count - 1;
        }
    }
}
