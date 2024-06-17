namespace blazorwasmpoker
{
    public class ResultsObtainedEventArgs : EventArgs
    {
        /// <summary>
        /// Win = True, Loss = False
        /// </summary>
        public bool WinLoss { get; set; }
        public WinCondition WinCondition { get; set; }
        public IEnumerable<PlayingCardModel> WinningCards { get; set; } = new List<PlayingCardModel>();
    }

    public class CardsUpdatedEventArgs : EventArgs
    {
        public IEnumerable<PlayingCardModel> NewCards { get; set; } = new List<PlayingCardModel>();
    }

    public class ScoreUpdatedEventArgs : EventArgs
    {
        public int Score { get; set; }
        public int ScoreChange { get; set; }
    }

    public class  HighScoreUpdatedEventArgs : EventArgs
    {
        public int HighScore { get; set; }
    }

    public class GameStateChangedEventArgs : EventArgs
    {
        public GameState GameState { get; set; }
    }

    public delegate void CardsUpdatedEventHandler(Object sender, CardsUpdatedEventArgs e);
    public delegate void ScoreUpdatedEventHandler(Object sender, ScoreUpdatedEventArgs e);
    public delegate void ResultsObtainedEventHandler(Object sender, ResultsObtainedEventArgs e);
    public delegate void GameStateChangedEventHandler(Object sender, GameStateChangedEventArgs e);
    public delegate void HighScoreUpdatedEventHandler(Object sender, HighScoreUpdatedEventArgs e);

    public interface IGameplay
    {
        public event CardsUpdatedEventHandler CardsUpdated;
        public event ScoreUpdatedEventHandler ScoreUpdated;
        public event ResultsObtainedEventHandler ResultsObtained;
        public event GameStateChangedEventHandler GameStateChanged;
        public event HighScoreUpdatedEventHandler HighScoreUpdated;
        public event EventHandler GameOverTriggered;

        void Start();

        void Reset();

        // Deal a new set of 5 cards
        void Deal();

        // Discard selected cards, draw new cards
        void Draw(IEnumerable<int> indicesOfCardsToDiscard);
    }
}