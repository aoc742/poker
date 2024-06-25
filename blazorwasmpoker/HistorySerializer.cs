using System.IO.Enumeration;
using System.Text.Json;

namespace blazorwasmpoker
{
    /// <summary>
    /// List of game state data to be stored and reloaded for offline use
    /// </summary>
    public class AppHistory
    {
        /// <summary>
        /// Users current game score
        /// </summary>
        public int CurrentScore { get; set; } = 0;

        /// <summary>
        /// Users overall high score across all gameplay
        /// </summary>
        public int HighScore { get; set; } = 0;

        /// <summary>
        /// IDs of the current 5 cards in hand
        /// </summary>
        public List<int> Hand { get; set; } = new List<int>() { 1, 2, 3, 4, 5 };

        /// <summary>
        /// 0 = NewGame
        /// 1 = Draw
        /// 2 = Deal
        /// 3 = GameOver
        /// </summary>
        public GameState GameState { get; set; } = 0;

        /// <summary>
        /// Initial game seed
        /// </summary>
        public int Seed { get; set; } = 0;

        /// <summary>
        /// Number of hands played this game
        /// </summary>
        public int HandsPlayed { get; set; } = 0;

        /// <summary>
        /// High score for most hands played in a game
        /// </summary>
        public int HandsPlayedHighScore { get; set; } = 0;
    }

    public class HistorySerializer
    {
        private static string filename = "apphistory.json";

        private static JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        public static void Serialize(AppHistory saveState)
        {
            // serialize
            string jsonString = JsonSerializer.Serialize(saveState, options);

            // Write to file
            File.WriteAllText(filename, jsonString);
        }

        public static AppHistory Deserialize()
        {
            // Read from file
            if (File.Exists(filename))
            {
                string filedata = File.ReadAllText(filename);

                // Deserialize
                AppHistory? saveState = JsonSerializer.Deserialize<AppHistory>(filedata);
                if (saveState != null)
                {
                    return saveState;
                }
            }

            return new AppHistory();
        }
    }
}
