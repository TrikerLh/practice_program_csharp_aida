using NSubstitute;
using NUnit.Framework;

namespace Tennis.Tests;

public class GameScoreBoardTest
{
    [Test]
    public void player_one_score()
    {
        var inputScore = Substitute.For<InputScore>();
        inputScore.ReadScore().Returns("score 1");
        var outputMessage = Substitute.For<OutputMessage>();
        var gameScoreBoard = new GameScoreBoard(inputScore, outputMessage);

        gameScoreBoard.StartGame();

        outputMessage.Received(1).Send("Fifteen Love");
    }
}