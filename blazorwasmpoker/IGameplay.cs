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

    public class SeedUpdatedEventArgs : EventArgs
    {
        public int Seed { get; set; }
    }

    public class GameStateChangedEventArgs : EventArgs
    {
        // True when invoked the very first time the app is refreshed, otherwise false.
        public bool FirstLoad { get; set; } = false;
        public GameState GameState { get; set; }
    }

    public class HandsPlayedUpdatedEventArgs : EventArgs
    {
        public int HandsPlayed {get; set;}
    }

        public class HandsPlayedHighScoreUpdatedEventArgs : EventArgs
    {
        public int HandsPlayedHighScore {get; set;}
    }

    public delegate void CardsUpdatedEventHandler(Object sender, CardsUpdatedEventArgs e);
    public delegate void ScoreUpdatedEventHandler(Object sender, ScoreUpdatedEventArgs e);
    public delegate void ResultsObtainedEventHandler(Object sender, ResultsObtainedEventArgs e);
    public delegate void GameStateChangedEventHandler(Object sender, GameStateChangedEventArgs e);
    public delegate void HighScoreUpdatedEventHandler(Object sender, HighScoreUpdatedEventArgs e);
    public delegate void SeedUpdatedEventHandler(Object sender, SeedUpdatedEventArgs e);
    public delegate void HandsPlayedUpdatedEventHandler(object sender, HandsPlayedUpdatedEventArgs e);
    public delegate void HandsPlayedHighScoreUpdatedEventHandler(object sender, HandsPlayedHighScoreUpdatedEventArgs e);

    public interface IGameplay
    {
        public event CardsUpdatedEventHandler CardsUpdated;
        public event ScoreUpdatedEventHandler ScoreUpdated;
        public event ResultsObtainedEventHandler ResultsObtained;
        public event GameStateChangedEventHandler GameStateChanged;
        public event HighScoreUpdatedEventHandler HighScoreUpdated;
        public event SeedUpdatedEventHandler SeedUpdated;
        public event HandsPlayedUpdatedEventHandler HandsPlayedUpdated;
        public event HandsPlayedHighScoreUpdatedEventHandler HandsPlayedHighScoreUpdated;
        public event EventHandler GameOverTriggered;

        void Start();

        void Reset();

        // Deal a new set of 5 cards
        void Deal();

        // Discard selected cards, draw new cards
        void Draw(IEnumerable<int> indicesOfCardsToDiscard);
    }
}