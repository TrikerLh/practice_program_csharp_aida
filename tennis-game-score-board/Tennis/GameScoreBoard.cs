using System.Numerics;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace Tennis;

public class GameScoreBoard {
    private readonly InputScore _inputScore;
    private readonly OutputMessage _outputMessage;
    private readonly Player _playerOne = new Player();
    private readonly Player _playerTwo = new Player();
    private bool _wasDeuce = false;

    public GameScoreBoard(InputScore inputScore, OutputMessage outputMessage) {
        _inputScore = inputScore;
        _outputMessage = outputMessage;
    }

    public void StartGame() {
        do {
            var readScore = _inputScore.ReadScore();
            AddPointToPlayer(readScore);
            if (!IsGameOver()) {
                PrintCurrentScore();
            }
        } while (!IsGameOver());

        PrintGameOverMessage();
    }

    private void PrintCurrentScore() {
        var message = CreateScoreMessage();
        _outputMessage.Send(message);
    }

    private string CreateScoreMessage() {
        if (IsDeuce()) {
            _wasDeuce = true;
            return "Deuce";
        }

        if (_wasDeuce) {
            if (_playerOne.HasAdvantage(_playerTwo)) {
                _wasDeuce = false;
                return "Advantage player 1";
            }
            return "Advantage player 2";

        }

        return $"{_playerOne.GetMessagePoint()} {_playerTwo.GetMessagePoint()}";
    }

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


    public void PrintGameOverMessage() {
        int playerNumber;
        playerNumber = _playerOne.Points() > _playerTwo.Points() ? 1 : 2;

        _outputMessage.Send($"Player {playerNumber} has won!!");
        _outputMessage.Send("It was a nice game.");
        _outputMessage.Send("Bye now!");
    }

    public bool IsGameOver() {
        if (_playerOne.Points() > _playerTwo.Points()) {
            if (_playerOne.Points() > 3 && (_playerOne.Points() - _playerTwo.Points() > 1)) {
                return true;
            }
        }

        if (_playerTwo.Points() > _playerOne.Points()) {
            if (_playerTwo.Points() > 3 && (_playerTwo.Points() - _playerOne.Points() > 1)) {
                return true;
            }
        }
        return false;
    }

    public bool IsDeuce() {
        return _playerOne.IsDeuce(_playerTwo);
    }
}