namespace Tennis;

public class GameScoreBoard {
    private readonly InputScore _inputScore;
    private readonly OutputMessage _outputMessage;

    public GameScoreBoard(InputScore inputScore, OutputMessage outputMessage)
    {
        _outputMessage = outputMessage;
        _inputScore = inputScore;
    }

    public void StartGame() {
        Game.Play(_inputScore, _outputMessage);
    }
}

