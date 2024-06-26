namespace Tennis
{
    public abstract class Game
    {
        protected readonly Player _playerOne;
        protected readonly Player _playerTwo;
        protected readonly InputScore _inputScore;
        protected readonly OutputMessage _outputMessage;

        public static void Play(InputScore inputScore, OutputMessage outputMessage)
        {
            var game = Start(inputScore, outputMessage);

            do
            {
                game = game.PlayPoint();
            } while (!game.IsOver());

            game.Display();
        }

        protected Game(Player playerOne, Player playerTwo, InputScore inputScore, OutputMessage outputMessage)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _inputScore = inputScore;
            _outputMessage = outputMessage;
        }

        protected virtual Game NextState()
        {
            return this;
        }

        protected abstract void Display();

        protected virtual bool IsOver()
        {
            return false;
        }

        private static Game Start(InputScore inputScore, OutputMessage outputMessage)
        {
            return new InitialGame(new Player(), new Player(), outputMessage, inputScore);
        }

        private Game PlayPoint()
        {
            var gameState = ScorePlayer();

            if (!gameState.IsOver())
            {
                gameState.Display();
            }

            return gameState;
        }

        private Game ScorePlayer()
        {
            if (_inputScore.ReadScore() == "score 1")
            {
                return ScorePlayerOne();
            }

            return ScorePlayerTwo();
        }

        private Game ScorePlayerOne()
        {
            _playerOne.AddPoint();
            return NextState();
        }

        private Game ScorePlayerTwo()
        {
            _playerTwo.AddPoint();
            return NextState();
        }
    }
    internal class InitialGame : Game
    {
        public InitialGame(Player playerOne, Player playerTwo, OutputMessage outputMessage, InputScore inputScore) : base(playerOne, playerTwo, inputScore, outputMessage)
        {

        }

        protected override Game NextState()
        {
            if (IsDeuce())
            {
                return new DeuceGame(_playerOne, _playerTwo, _outputMessage, _inputScore);
            }

            if (HasWonPlayerOne() || HasWonPlayerTwo())
            {
                return new OverGame(_playerOne, _playerTwo, _outputMessage, _inputScore);
            }

            return this;
        }

        protected override void Display()
        {
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

    public class DeuceGame : Game
    {
        public DeuceGame(Player playerOne, Player playerTwo, OutputMessage outputMessage, InputScore inputScore) : base(playerOne, playerTwo, inputScore, outputMessage)
        {
        }

        protected override Game NextState()
        {
            return new AdvantageGame(_playerOne, _playerTwo, _outputMessage, _inputScore);
        }

        protected override void Display()
        {
            _outputMessage.Send("Deuce");
        }
    }

    public class AdvantageGame : Game
    {
        public AdvantageGame(Player playerOne, Player playerTwo, OutputMessage outputMessage, InputScore inputScore) : base(playerOne, playerTwo, inputScore, outputMessage)
        {
        }

        protected override Game NextState()
        {
            if (_playerOne.IsTiedWith(_playerTwo))
            {
                return new DeuceGame(_playerOne, _playerTwo, _outputMessage, _inputScore);
            }

            return new OverGame(_playerOne, _playerTwo, _outputMessage, _inputScore);
        }

        private string CreateScoreMessage()
        {
            if (_playerOne.HasMorePointsThan(_playerTwo))
            {
                return "Advantage player 1";
            }

            return "Advantage player 2";
        }

        protected override void Display()
        {
            _outputMessage.Send(CreateScoreMessage());
        }

    }

    internal class OverGame : Game
    {
        public OverGame(Player playerOne, Player playerTwo, OutputMessage outputMessage, InputScore inputScore) : base(playerOne, playerTwo, inputScore, outputMessage)
        {

        }

        protected override Game NextState()
        {
            return this;
        }

        protected override void Display()
        {
            var winnerNumber = (_playerOne.HasMorePointsThan(_playerTwo) ? 1 : 2);
            _outputMessage.Send($"Player {winnerNumber} has won!!");
            _outputMessage.Send("It was a nice game.");
            _outputMessage.Send("Bye now!");
        }

        protected override bool IsOver()
        {
            return true;
        }
    }
}
