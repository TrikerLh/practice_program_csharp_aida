namespace Tennis;

public class GameScoreBoard {
    private readonly InputScore _inputScore;
    private GameState _gameState;

    public GameScoreBoard(InputScore inputScore, OutputMessage outputMessage) {
        _inputScore = inputScore;
        _gameState = new InitialGameState(new Player(), new Player(), outputMessage);
    }

    public void StartGame() {
        do {
            var readScore = _inputScore.ReadScore();
            AddPointToPlayer(readScore);
            if (!_gameState.IsOver()) {
                _gameState.Display();
            }
        } while (!_gameState.IsOver());

        _gameState.Display();
    }

    public void AddPointToPlayer(string readScore) {
        if (PlayerOneScores(readScore)) {
            _gameState = _gameState.ScorePlayerOne();
        }
        else if (PlayerTwoScores(readScore)) {
            _gameState = _gameState.ScorePlayerTwo();
        }
    }
    private bool PlayerTwoScores(string input) {
        return input == "score 2";
    }

    private bool PlayerOneScores(string input) {
        return input == "score 1";
    }
}

