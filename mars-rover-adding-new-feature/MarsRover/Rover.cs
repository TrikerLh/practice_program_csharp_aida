using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MarsRover
{
    public class Rover
    {
        private const int Displacement = 1;
        private RoverVector _vector;
        private readonly CommandInterpreter _commandInterpreter;

        public Rover(int x, int y, string direction)
        {
            _vector = new(DirectionMapper.Create(direction), new Coordinates(x, y));
            _commandInterpreter = new CommandInterpreter();
        }

        //Extract command to a new abstraction 
        public void Receive(string commandsSequence)
        {
            var commands = ExtractCommands(commandsSequence);
            commands.ToList().ForEach(Execute);
        }

        private IList<string> ExtractCommands(string commandsSequence)
        {
            return commandsSequence.Select(Char.ToString).ToList();
        }

        private void Execute(string command)
        {
            if (command.Equals("l"))
            {
                _vector = _vector.RotateToLeft();
            }
            else if (command.Equals("r"))
            {
                _vector = _vector.RotateToRight();
            }
            else if (command.Equals("f"))
            {
                _vector = _vector.Move(Displacement);
            }
            else
            {
                _vector = _vector.Move(-Displacement);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rover)obj);
        }

        protected bool Equals(Rover other)
        {
            return Equals(_vector, other._vector);
        }

        public override int GetHashCode()
        {
            return (_vector != null ? _vector.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"{nameof(_vector)}: {_vector}";
        }
    }

    public class CommandInterpreter
    {
        public List<Command> Extract(string commandSequence)
        {
            return new List<Command>();
        }
    }

    public class Command
    {
    }
}