namespace blazorwasmpoker
{
    public enum WinCondition
    {
        Loss = 0,
        JacksOrBetter = 5,
        TwoPair = 10,
        ThreeOfAKind = 15,
        Straight = 20,
        Flush = 25,
        FullHouse = 40,
        FourOfAKind = 125,
        StraightFlush = 250,
        RoyalFlush = 5000
    }

    public class GameModel : IGameplay
    {
        private List<PlayingCardModel> _deck = new List<PlayingCardModel>(); // 52 card deck
        public List<PlayingCardModel> _hand = new List<PlayingCardModel>(new PlayingCardModel[5]); // 5 card hand
        private int _score = 0;

        private int seed = new Random().Next();
        private Random rng;

        public event CardsUpdatedEventHandler? CardsUpdated;
        public event ScoreUpdatedEventHandler? ScoreUpdated;
        public event ResultsObtainedEventHandler? ResultsObtained;
        public event EventHandler? GameOverTriggered;

        public GameModel()
        {
            // Create card deck
            for (int i = 1; i <= 52; i++)
            {
                this._deck.Add(new PlayingCardModel(i));
            }

            rng = new Random(seed);
        }

        // Id numbers of the 5 cards in hand
        private List<int> GetHandIds()
        {
            return new List<int>(new int[5]);
        }

        private bool isRoyalFlush()
        {
            var handIds = GetHandIds();

            CardSuit currentSuit = _hand[0].GetSuit();
            if (_hand.All(card => card.GetSuit() == currentSuit) &&
                _hand.Any(card => card.GetCardNumber() == CardNumber.Ten) &&
                _hand.Any(card => card.GetCardNumber() == CardNumber.Jack) &&
                _hand.Any(card => card.GetCardNumber() == CardNumber.Queen) &&
                _hand.Any(card => card.GetCardNumber() == CardNumber.King) &&
                _hand.Any(card => card.GetCardNumber() == CardNumber.Ace)
                )
            {
                return true;
            }
            return false;
        }

        private bool isStraightFlush()
        {
            return isStraight() && isFlush();
        }

        private bool is4OfAKind()
        {
            var groups = _hand.GroupBy(x => x.GetCardNumber());

            if (groups.Count(groups => groups.Count() == 4) > 0)
            {
                return true;
            }
            return false;
        }

        private bool isFullHouse()
        {
            var groups = _hand.GroupBy(x => x.GetCardNumber());

            if (groups.Count(groups => groups.Count() == 2) == 1 &&
                groups.Count(groups => groups.Count() == 3) == 1)
            {
                return true;
            }
            return false;
        }

        private bool isFlush()
        {
            CardSuit firstSuit = _hand[0].GetSuit();
            if (_hand.All(card => card.GetSuit() == firstSuit))
            {
                return true;
            }
            return false;
        }

        private bool isStraight()
        {
            // Must be 5 unique card numbers, otherwise not a straight
            if (_hand.DistinctBy(card => card.GetCardNumber()).Count() < 5) return false;

            var sortedHand = _hand.OrderBy(card => card.GetCardNumber()).ToList();
            int minNumber = (int)sortedHand[0].GetCardNumber();
            int maxNumber = (int)sortedHand[4].GetCardNumber();

            // Ace is set to low value here
            if (maxNumber - minNumber == 4) return true;

            // Now set Ace to high value then check again
            if (minNumber == 0)
            {
                maxNumber = 13;
                minNumber = (int)sortedHand[1].GetCardNumber();
            }

            if (maxNumber - minNumber == 4) return true;

            return false;
        }

        private bool is3OfAKind()
        {
            var groups = _hand.GroupBy(x => x.GetCardNumber());

            if (groups.Count(groups => groups.Count() == 3) > 0)
            {
                return true;
            }
            return false;
        }

        private bool is2Pair()
        {
            var groups = _hand.GroupBy(x => x.GetCardNumber());

            if (groups.Count(groups => groups.Count() == 2) == 2)
            {
                return true;
            }

            return false;
        }

        private bool isJacksOrBetter()
        {
            if (_hand.Count(card => card.GetCardNumber().Equals(CardNumber.Jack)) >= 2 ||
                _hand.Count(card => card.GetCardNumber().Equals(CardNumber.Queen)) >= 2 ||
                _hand.Count(card => card.GetCardNumber().Equals(CardNumber.King)) >= 2 ||
                _hand.Count(card => card.GetCardNumber().Equals(CardNumber.Ace)) >= 2
                )
            {
                return true;
            }
            return false;
        }

        public WinCondition calculateScore()
        {
            if (isRoyalFlush()) return WinCondition.RoyalFlush;
            if (isStraightFlush()) return WinCondition.StraightFlush;
            if (is4OfAKind()) return WinCondition.FourOfAKind;
            if (isFullHouse()) return WinCondition.FullHouse;
            if (isFlush()) return WinCondition.Flush;
            if (isStraight()) return WinCondition.Straight;
            if (is3OfAKind()) return WinCondition.ThreeOfAKind;
            if (is2Pair()) return WinCondition.TwoPair;
            if (isJacksOrBetter()) return WinCondition.JacksOrBetter;
            return WinCondition.Loss;
        }

        private void Shuffle<T>(IList<T> values)
        {
            int inputArrayLength = values.Count();

            for (int i = inputArrayLength - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                var temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }
        }

        public void Deal()
        {
            // Upon dealing new hand, automatically bet 5 points for the user
            this._score -= 5;
            ScoreUpdated?.Invoke(this, new ScoreUpdatedEventArgs()
            {
                Score = this._score,
                ScoreChange = 0
            });

            this.Shuffle(this._deck);
            _hand[0] = _deck[0];
            _hand[1] = _deck[1];
            _hand[2] = _deck[2];
            _hand[3] = _deck[3];
            _hand[4] = _deck[4];

            CardsUpdated?.Invoke(this, new CardsUpdatedEventArgs()
            {
                NewCards = _hand
            });
        }

        public void Draw(IEnumerable<int> indicesOfCardsToDiscard)
        {
            int nextDeckIndex = 5; // draw the next card in the shuffled deck
            foreach (int index in indicesOfCardsToDiscard)
            {
                _hand[index] = _deck[nextDeckIndex];
                ++nextDeckIndex;
            }

            CardsUpdated?.Invoke(this, new CardsUpdatedEventArgs()
            {
                NewCards = _hand
            });

            EndOfTurn();
        }

        public void EndOfTurn()
        {
            WinCondition winCondition = calculateScore();
            int scoreChange = (int)winCondition;
            this._score += scoreChange;

            ScoreUpdated?.Invoke(this, new ScoreUpdatedEventArgs() { Score = _score, ScoreChange = scoreChange });

            ResultsObtained?.Invoke(this, new ResultsObtainedEventArgs()
            {
                WinLoss = scoreChange > 0,
                WinCondition = winCondition
            });

            if (this._score <= 0)
            {
                Reset();
            }
        }

        public void Reset()
        {
            GameOverTriggered?.Invoke(this, new EventArgs());
            Start();
        }

        public void Start()
        {
            // TODO check for saved state. If exists, load it

            // Otherwise start a new game
            this._score = 100; // Upon game over, add 100 points to the total to start over
            ScoreUpdated?.Invoke(this, new ScoreUpdatedEventArgs() { Score = _score, ScoreChange = 0 });

        }

        public int GetSeed()
        {
            return seed;
        }
    }
}