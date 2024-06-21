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
<<<<<<< HEAD
        FifteenThirty();
=======
        FiftennTherty();
>>>>>>> b6fb6ddf27aec1928b44f1e50d605f0ba2897262
        ThirtyThirty();
        ThirtyForty();
        Deuce(1);
        Advantage("2", 1);
        Won("2");
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

        FifteenLove();
        FifteenFifteen();
        ThirtyFifteen();
        ThirtyThirty();
        FortyThirty();
        Deuce(3);
        Advantage("1", 3);
        Won("1");
        _outputMessage.Received(14).Send(Arg.Any<string>());
    }

    private void ThirtyForty()
    {
        _outputMessage.Received(1).Send("Thirty Forty");
    }


    private void FifteenThirty()
    {
        _outputMessage.Received(1).Send("Fifteen Thirty");
    }

    private void LoveForty()
    {
        _outputMessage.Received(1).Send("Love Forty");
    }

    private void LoveThirty()
    {
        _outputMessage.Received(1).Send("Love Thirty");
    }

    private void LoveFifteen()
    {
        _outputMessage.Received(1).Send("Love Fifteen");
    }

    private void Won(string player)
    {
        _outputMessage.Received(1).Send($"Player {player} has won!!");
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