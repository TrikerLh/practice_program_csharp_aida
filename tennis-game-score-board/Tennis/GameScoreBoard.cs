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
        _outputMessage.Send("Fifteen Love");
    }
}