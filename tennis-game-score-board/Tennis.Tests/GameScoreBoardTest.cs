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

        FifteenLove();
        ThirtyLove();
        FortyLove();
        Won("1");
    }

    [Test]
    public void player_two_wins_the_game()
    {
        _inputScore.ReadScore().Returns("score 2", "score 2", "score 2", "score 2");

        _gameScoreBoard.StartGame();

        LoveFifteen();
        LoveThirty();
        LoveForty();
        Won("2");
    }

    [Test]
    public void match_with_deuce() {
        _inputScore.ReadScore().Returns("score 2", "score 1", "score 2", "score 1", "score 2", "score 1");

        _gameScoreBoard.StartGame();

        _outputMessage.Received(1).Send("Love Fifteen");
        _outputMessage.Received(1).Send("Fifteen Fifteen");
        _outputMessage.Received(1).Send("Fifteen Thirty");
        _outputMessage.Received(1).Send("Thirty Forty");
        _outputMessage.Received(1).Send("Deuce");
    }


    [Test]
    public void player_two_wins_the_game_with_advantage() {
        _inputScore.ReadScore().Returns("score 2", "score 1", "score 2", "score 1", "score 2", "score 1", "score 2", "score 2");

        _gameScoreBoard.StartGame();

        LoveFifteen();
        FifteenFifteen();
        FifteenThirty();
        ThirtyThirty();
        ThirtyForty();
        Deuce(1);
        Advantage("2", 1);
        Won("2");
        _outputMessage.Received(1).Send("It was a nice game.");
        _outputMessage.Received(1).Send("Bye now!");
    }


    [Test]
    public void player_one_wins_the_game_with_advantage() {
        _inputScore.ReadScore().Returns("score 1", "score 2", "score 1", "score 2", "score 1", "score 2", "score 1", "score 1");

        _gameScoreBoard.StartGame();

        FifteenLove();
        FifteenFifteen();
        ThirtyFifteen();
        ThirtyThirty();
        FortyThirty();
        Deuce(1);
        Advantage("1", 1);
        Won("1");
        _outputMessage.Received(10).Send(Arg.Any<string>());
    }

    [Test]
    public void player_one_wins_the_game_with_advantageXXX() {
        _inputScore.ReadScore().Returns("score 1", "score 2", "score 1", "score 2", "score 1", "score 2", "score 1", "score 2", "score 1", "score 2", "score 1", "score 1");

        _gameScoreBoard.StartGame();

        _outputMessage.Received(1).Send("Fifteen Love");
        _outputMessage.Received(1).Send("Fifteen Fifteen");
        _outputMessage.Received(1).Send("Thirty Fifteen");
        _outputMessage.Received(1).Send("Thirty Thirty");
        _outputMessage.Received(1).Send("Forty Thirty");
        _outputMessage.Received(3).Send("Deuce");
        _outputMessage.Received(3).Send("Advantage player 1");
        _outputMessage.Received(1).Send("Player 1 has won!!");
        _outputMessage.Received(1).Send("It was a nice game.");
        _outputMessage.Received(1).Send("Bye now!");
    }

    private void Advantage(string player, int requiredNumberOfCalls)
    {
        _outputMessage.Received(requiredNumberOfCalls).Send($"Advantage player {player}");
    }

    private void Deuce(int requiredNumberOfCalls)
    {
        _outputMessage.Received(requiredNumberOfCalls).Send("Deuce");
    }

    private void FortyLove()
    {
        _outputMessage.Received(1).Send("Forty Love");
    }

    private void ThirtyLove()
    {
        _outputMessage.Received(1).Send("Thirty Love");
    }

    private void FortyThirty()
    {
        _outputMessage.Received(1).Send("Forty Thirty");
    }

    private void ThirtyThirty()
    {
        _outputMessage.Received(1).Send("Thirty Thirty");
    }

    private void ThirtyFifteen()
    {
        _outputMessage.Received(1).Send("Thirty Fifteen");
    }

    private void FifteenFifteen()
    {
        _outputMessage.Received(1).Send("Fifteen Fifteen");
    }

    private void FifteenLove()
    {
        _outputMessage.Received(1).Send("Fifteen Love");
    }
}