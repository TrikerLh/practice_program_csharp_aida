namespace Tennis;

public class GameScoreBoard
{
    private readonly InputScore _inputScore;
    private readonly OutputMessage _outputMessage;

    public GameScoreBoard(InputScore inputScore, OutputMessage outputMessage)
    {
        _inputScore = inputScore;
        _outputMessage = outputMessage;
    }

    public void StartGame()
    {
        var input = _inputScore.ReadScore();

        if (input == "score 1")
        {
            _outputMessage.Send("Fifteen Love");
        }
        else
        {
            _outputMessage.Send("Love Fifteen");
        }
    }
}