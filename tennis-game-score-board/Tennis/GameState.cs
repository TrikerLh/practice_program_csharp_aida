using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis {
    public abstract class GameState {
        protected readonly Player _playerTwo;
        protected readonly OutputMessage _outputMessage;
        protected readonly Player _playerOne;

        protected GameState(Player playerOne, Player playerTwo, OutputMessage outputMessage) {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _outputMessage = outputMessage;
        }

        public virtual GameState ScorePlayerOne() {
            _playerOne.AddPoint();
            return this;
        }

        public virtual GameState ScorePlayerTwo() {
            _playerTwo.AddPoint();
            return this;
        }

        public abstract void Display();

        public virtual bool IsOver() {
            return false;
        }
    }
    public class InitialGameState : GameState {
        public InitialGameState(Player playerOne, Player playerTwo, OutputMessage outputMessage) : base(playerOne, playerTwo, outputMessage) {

        }
        public override GameState ScorePlayerOne() {
            _playerOne.AddPoint();
            return NextState();
        }

        public override GameState ScorePlayerTwo() {
            _playerTwo.AddPoint();
            return NextState();
        }

        public override void Display() {
            _outputMessage.Send($"{_playerOne.GetMessagePoint()} {_playerTwo.GetMessagePoint()}");
        }

        private GameState NextState() {
            if (_playerOne.Points() == _playerTwo.Points() && _playerOne.Points() == 3) {
                return new DeuceGameState(_playerOne, _playerTwo, _outputMessage);
            }

            if (_playerOne.Points() > _playerTwo.Points() && _playerOne.Points() > 3)
            {
                return new GameOverGameState(_playerOne, _playerTwo, _outputMessage);
            }

            if (_playerTwo.Points() > _playerOne.Points() && _playerTwo.Points() > 3) {
                return new GameOverGameState(_playerOne, _playerTwo, _outputMessage);
            }

            return this;
        }
    }

    public class DeuceGameState : GameState {
        public DeuceGameState(Player playerOne, Player playerTwo, OutputMessage outputMessage) : base(playerOne, playerTwo, outputMessage) {
        }


        public override GameState ScorePlayerOne() {
            _playerOne.AddPoint();
            return new AdvantageGameState(_playerOne, _playerTwo, _outputMessage);
        }

        public override GameState ScorePlayerTwo() {
            _playerTwo.AddPoint();
            return new AdvantageGameState(_playerOne, _playerTwo, _outputMessage);
        }

        public override void Display() {
            _outputMessage.Send("Deuce");
        }
    }

    public class AdvantageGameState : GameState {
        public AdvantageGameState(Player playerOne, Player playerTwo, OutputMessage outputMessage) : base(playerOne, playerTwo, outputMessage) {
        }

        private string CreateScoreMessage() {
            if (_playerOne.Points() > _playerTwo.Points()) {
                return "Advantage player 1";
            }

            return "Advantage player 2";
        }

        public override GameState ScorePlayerOne() {
            _playerOne.AddPoint();
            return NextState();
        }

        public override GameState ScorePlayerTwo() {
            _playerTwo.AddPoint();
            return NextState();
        }

        private GameState NextState() {
            if (_playerOne.Points() == _playerTwo.Points()) {
                return new DeuceGameState(_playerOne, _playerTwo, _outputMessage);
            }

            return new GameOverGameState(_playerOne, _playerTwo, _outputMessage);
        }

        public override void Display() {
            _outputMessage.Send(CreateScoreMessage());
        }

    }

    internal class GameOverGameState : GameState {
        public GameOverGameState(Player playerOne, Player playerTwo, OutputMessage outputMessage) : base(playerOne, playerTwo, outputMessage) {

        }

        public override void Display()
        {
            _outputMessage.Send($"Player {GetWinnerNumber()} has won!!");
            _outputMessage.Send("It was a nice game.");
            _outputMessage.Send("Bye now!");
        }

        private int GetWinnerNumber()
        {
            return _playerOne.Points() > _playerTwo.Points() ? 1 : 2;
        }

        public override bool IsOver() {
            return true;
        }
    }
}
