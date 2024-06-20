using System.Numerics;

namespace Tennis;

public class GameScoreBoard {
    private readonly InputScore _inputScore;
    private readonly OutputMessage _outputMessage;
    private bool _gameDeuce = false;
    private Game _game = new Game();

    public GameScoreBoard(InputScore inputScore, OutputMessage outputMessage) {
        _inputScore = inputScore;
        _outputMessage = outputMessage;
    }

    public void StartGame() {
        do {
            var readScore = _inputScore.ReadScore();    
            _game.AddPointToPlayer(readScore);
            PrintCurrentScore();
        } while (!IsGameOver());

        PrintGameOverMessage();
    }

    private void PrintCurrentScore() {
        var message = CreateScoreMessage();
        _outputMessage.Send(message);
    }

    private string CreateScoreMessage() {
        if (_game._playerOne.IsDeuce(_game._playerTwo)) {
            _gameDeuce = true;
            return "Deuce";
        }

        if (_gameDeuce) {
            _gameDeuce = false;
            if (_game._playerOne.HasAdvantage(_game._playerTwo)) {

                return "Advantage player 1";
            }
            return "Advantage player 2";

        }
        return $"{_game._playerOne.GetMessagePoint()} {_game._playerTwo.GetMessagePoint()}";
    }


    public void PrintGameOverMessage() {
        int playerNumber;
        if (_game._playerOne.HasWon()) {
            playerNumber = 1;
        }
        else {
            playerNumber = 2;
        }

        _outputMessage.Send($"Player {playerNumber} has won!!");
        _outputMessage.Send("It was a nice game.");
        _outputMessage.Send("Bye now!");
    }

    private bool IsGameOver() {
        return _game._playerOne.HasWon() || _game._playerTwo.HasWon();
    }
}

public class Game
{
    public Player _playerOne = new Player();
    public Player _playerTwo = new Player();

    public void AddPointToPlayer(string readScore) {
        if (PlayerOneScores(readScore)) {
            _playerOne.AddPoint();
        }
        else if (PlayerTwoScores(readScore)) {
            _playerTwo.AddPoint();
        }
    }
    private bool PlayerTwoScores(string input) {
        return input == "score 2";
    }

    private bool PlayerOneScores(string input) {
        return input == "score 1";
    }
}