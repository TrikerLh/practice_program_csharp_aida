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
    public void player_one_score()
    {
        _inputScore.ReadScore().Returns("score 1");

        _gameScoreBoard.StartGame();

        _outputMessage.Received(1).Send("Fifteen Love");
    }

    [Test]
    public void player_two_score()
    {
        _inputScore.ReadScore().Returns("score 2");

        _gameScoreBoard.StartGame();

        _outputMessage.Received(1).Send("Love Fifteen");
    }

}