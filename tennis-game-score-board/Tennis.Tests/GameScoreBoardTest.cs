using NSubstitute;
using NUnit.Framework;

namespace Tennis.Tests;

public class GameScoreBoardTest
{
    private InputScore _inputScore;
    private OutputMessage _outputMessage;
    private GameScoreBoard _gameScoreBoard;
    
    [SetUp]
    public void SetUp()
    {
        _inputScore = Substitute.For<InputScore>();
        _outputMessage = Substitute.For<OutputMessage>();
        _gameScoreBoard = new GameScoreBoard(_inputScore, _outputMessage);
    }

    [Test]
    public void player_one_wins_the_game()
    {
        _inputScore.ReadScore().Returns("score 1", "score 1", "score 1", "score 1");

        _gameScoreBoard.StartGame();

        _outputMessage.Received(1).Send("Fifteen Love");
        _outputMessage.Received(1).Send("Thirty Love");
        _outputMessage.Received(1).Send("Forty Love");
        _outputMessage.Received(1).Send("Player 1 has won!!");
        _outputMessage.Received(1).Send("It was a nice game.");
        _outputMessage.Received(1).Send("Bye now!");
    }

    [Test]
    public void player_two_wins_the_game()
    {
        _inputScore.ReadScore().Returns("score 2", "score 2", "score 2", "score 2");

        _gameScoreBoard.StartGame();

        _outputMessage.Received(1).Send("Love Fifteen");
        _outputMessage.Received(1).Send("Love Thirty");
        _outputMessage.Received(1).Send("Love Forty");
        _outputMessage.Received(1).Send("Player 2 has won!!");
        _outputMessage.Received(1).Send("It was a nice game.");
        _outputMessage.Received(1).Send("Bye now!");
    }
}