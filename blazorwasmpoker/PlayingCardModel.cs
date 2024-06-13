namespace blazorwasmpoker
{
    public enum CardSuit
    {
        Clubs,
        Spades,
        Hearts,
        Diamonds
    }

    public enum CardNumber
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public class PlayingCardModel
    {
        // Maps Card IDs to Card Names
        private readonly Dictionary<int, string> CardNames = new Dictionary<int, string>()
        {
            // Clubs
            {1, "A ♣"},
            {2, "2 ♣"},
            {3, "3 ♣"},
            {4, "4 ♣"},
            {5, "5 ♣"},
            {6, "6 ♣"},
            {7, "7 ♣"},
            {8, "8 ♣"},
            {9, "9 ♣"},
            {10, "10 ♣"},
            {11, "J ♣"},
            {12, "Q ♣"},
            {13, "K ♣"},

            // Spades
            {14, "A ♠"},
            {15, "2 ♠"},
            {16, "3 ♠"},
            {17, "4 ♠"},
            {18, "5 ♠"},
            {19, "6 ♠"},
            {20, "7 ♠"},
            {21, "8 ♠"},
            {22, "9 ♠"},
            {23, "10 ♠"},
            {24, "J ♠"},
            {25, "Q ♠"},
            {26, "K ♠"},

            // Diamonds
            {27, "A ♦"},
            {28, "2 ♦"},
            {29, "3 ♦"},
            {30, "4 ♦"},
            {31, "5 ♦"},
            {32, "6 ♦"},
            {33, "7 ♦"},
            {34, "8 ♦"},
            {35, "9 ♦"},
            {36, "10 ♦"},
            {37, "J ♦"},
            {38, "Q ♦"},
            {39, "K ♦"},

            // Hearts
            {40, "A ♥"},
            {41, "2 ♥"},
            {42, "3 ♥"},
            {43, "4 ♥"},
            {44, "5 ♥"},
            {45, "6 ♥"},
            {46, "7 ♥"},
            {47, "8 ♥"},
            {48, "9 ♥"},
            {49, "10 ♥"},
            {50, "J ♥"},
            {51, "Q ♥"},
            {52, "K ♥"}
        };

        private int _id;
        private string _name;
        private CardSuit _suit;
        private CardNumber _number;

        public PlayingCardModel(int cardId)
        {
            this._id = cardId;
            this._name = CardNames[cardId];

            // Determine card suit
            if (_id < 13)
            {
                this._suit = CardSuit.Clubs;
            }
            else if (_id >= 14 && _id <= 26)
            {
                this._suit = CardSuit.Spades;
            }
            else if (_id >= 27 && _id <= 39)
            {
                this._suit = CardSuit.Diamonds;
            }
            else
            {
                this._suit = CardSuit.Hearts;
            }

            // Determine card number
            switch (_id)
            {
                case 1:
                case 14:
                case 27:
                case 40:
                    this._number = CardNumber.Ace;
                    break;
                case 2:
                case 15:
                case 28:
                case 41:
                    this._number = CardNumber.Two;
                    break;
                case 3:
                case 16:
                case 29:
                case 42:
                    this._number = CardNumber.Three;
                    break;
                case 4:
                case 17:
                case 30:
                case 43:
                    this._number = CardNumber.Four;
                    break;
                case 5:
                case 18:
                case 31:
                case 44:
                    this._number = CardNumber.Five;
                    break;
                case 6:
                case 19:
                case 32:
                case 45:
                    this._number = CardNumber.Six;
                    break;
                case 7:
                case 20:
                case 33:
                case 46:
                    this._number = CardNumber.Seven;
                    break;
                case 8:
                case 21:
                case 34:
                case 47:
                    this._number = CardNumber.Eight;
                    break;
                case 9:
                case 22:
                case 35:
                case 48:
                    this._number = CardNumber.Nine;
                    break;
                case 10:
                case 23:
                case 36:
                case 49:
                    this._number = CardNumber.Ten;
                    break;
                case 11:
                case 24:
                case 37:
                case 50:
                    this._number = CardNumber.Jack;
                    break;
                case 12:
                case 25:
                case 38:
                case 51:
                    this._number = CardNumber.Queen;
                    break;
                case 13:
                case 26:
                case 39:
                case 52:
                    this._number = CardNumber.King;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_id));
            }
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public CardSuit GetSuit()
        {
            return _suit;
        }

        public CardNumber GetCardNumber()
        {
            return _number;
        }
    }
}