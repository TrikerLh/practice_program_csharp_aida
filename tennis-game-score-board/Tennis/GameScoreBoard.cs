namespace Tennis;

public class GameScoreBoard
{
    private readonly InputScore _inputScore;
    private readonly OutputMessage _outputMessage;
    private Player _playerOne = new Player();
    private Player _playerTwo = new Player();

    public GameScoreBoard(InputScore inputScore, OutputMessage outputMessage)
    {
        _inputScore = inputScore;
        _outputMessage = outputMessage;
    }

    public void StartGame()
    {
        do
        {
            AddPointToPlayer();
            PrintCurrentScore();
        } while (!IsGameOver());

        PrintGameOverMessage();
    }

    private void PrintCurrentScore()
    {
        var message = CreateScoreMessage();
        _outputMessage.Send(message);
        if (message == "Deuce")
        {
            var playerNumber = "2";
            _outputMessage.Send($"Advantage player {playerNumber}");
        }
    }

    private string CreateScoreMessage()
    {
        if (_playerOne.IsDeuce(_playerTwo))
        {
            return "Deuce";
        }
        return $"{_playerOne.GetMessagePoint()} {_playerTwo.GetMessagePoint()}";
    }

    private void AddPointToPlayer()
    {
        var score = _inputScore.ReadScore();

        if (PlayerOneScores(score))
        {
            _playerOne.AddPoint();
        }
        else if(PlayerTwoScores(score))
        {
            _playerTwo.AddPoint();
        }
    }

    private void PrintGameOverMessage()
    {
        int playerNumber;
        if (_playerOne.HasWon())
        {
            playerNumber = 1;
        }
        else
        {
            playerNumber = 2;
        }

        _outputMessage.Send($"Player {playerNumber} has won!!");
        _outputMessage.Send("It was a nice game.");
        _outputMessage.Send("Bye now!");
    }

    private bool IsGameOver()
    {
        return _playerOne.HasWon() || _playerTwo.HasWon();
    }

    private bool PlayerTwoScores(string input)
    {
        return input == "score 2";
    }

    private bool PlayerOneScores(string input)
    {
        return input == "score 1";
    }
}