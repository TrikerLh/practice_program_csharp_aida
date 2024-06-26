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

        public static GameState Initial(OutputMessage outputMessage)
        {
            return new InitialGameState(new Player(), new Player(), outputMessage);
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
    internal class InitialGameState : GameState {
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
            _outputMessage.Send($"{FormatPoints(_playerOne.Points())} {FormatPoints(_playerTwo.Points())}");
        }

        private string FormatPoints(int points)
        {
            if (points == 0)
                return "Love";
            if (points == 1)
                return "Fifteen";
            if (points == 2)
                return "Thirty";

            return "Forty";
        }

        private GameState NextState() {
            if (IsDeuce()) {
                return new DeuceGameState(_playerOne, _playerTwo, _outputMessage);
            }

            if (HasWonPlayerOne() || HasWonPlayerTwo())
            {
                return new GameOverGameState(_playerOne, _playerTwo, _outputMessage);
            }

            return this;
        }

        private bool IsDeuce()
        {
            return _playerOne.IsTiedWith(_playerTwo) && _playerOne.HasFortyPoints();
        }

        private bool HasWonPlayerTwo()
        {
            return _playerTwo.HasMorePointsThan(_playerOne) && _playerTwo.HasMoreThanFortyPoints();
        }

        private bool HasWonPlayerOne()
        {
            return _playerOne.HasMorePointsThan(_playerTwo) && _playerOne.HasMoreThanFortyPoints();
        }
    }

    public class DeuceGameState : GameState {
        public DeuceGameState(Player playerOne, Player playerTwo, OutputMessage outputMessage) : base(playerOne, playerTwo, outputMessage) {
        }


        public override GameState ScorePlayerOne() {
            _playerOne.AddPoint();
            return NextState();
        }

        public override GameState ScorePlayerTwo()
        {
            _playerTwo.AddPoint();
            return NextState();
        }

        private GameState NextState()
        {
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
            if (_playerOne.HasMorePointsThan(_playerTwo)) {
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
            if (_playerOne.IsTiedWith(_playerTwo)) {
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
            var winnerNumber = (_playerOne.HasMorePointsThan(_playerTwo) ? 1 : 2);
            _outputMessage.Send($"Player {winnerNumber} has won!!");
            _outputMessage.Send("It was a nice game.");
            _outputMessage.Send("Bye now!");
        }

        public override bool IsOver() {
            return true;
        }
    }
}
