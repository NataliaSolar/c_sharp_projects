using System;


namespace LWTech.NataliaSolar.Assignment03
{
    public enum Suits { Hearts, Diamonds, Clubs, Spades };
    public enum Ranks { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };

    public class Card
    {
        public Suits Suit { get; private set; }
        public Ranks Rank { get; private set; }



        public Card(Ranks rank = Ranks.Ace, Suits suit = Suits.Hearts)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        public override string ToString()
        {
            return $"[{Rank} of {Suit}]";
        }

    }

    public class Deck
    {
        public Card[] CardDeck { get; private set; }
        public int Count { get; private set; }
        public int DeckSize { get; private set; }

        public Deck()
        {
            DeckSize = 52;
            CardDeck = new Card[DeckSize];
            Count = 0;
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            {
                foreach (Ranks rank in Enum.GetValues(typeof(Ranks)))
                {

                    CardDeck[Count] = new Card(rank, suit);
                    Count++;
                }
            }
        }

        public Deck (Deck deck)
        {
            this.DeckSize = deck.DeckSize;
            this.Count = deck.Count;
            CardDeck = new Card[DeckSize];            
            for (int i = 0; i < deck.CardDeck.Length; i++ )
            {
                this.CardDeck[i] = deck.CardDeck[i];
            }
            this.Count = deck.Count;            
        }

        public Deck (Card[] deckArray)
        {
            DeckSize = deckArray.Length;
            CardDeck = new Card[DeckSize];
            Count = 0;
            for (int i = 0; i < DeckSize; i++)
            {
                CardDeck[i] = deckArray[i];
                Count++;
            }
 
        }


        // Fisher-Yates algorithm source: https://exceptionnotfound.net/understanding-the-fisher-yates-card-shuffling-algorithm/
        public void Shuffle()
        {
            Count = 1;
            Random rng = new Random();
            for (int i = DeckSize - 1; i > 0; --i)
            {                
                int j = rng.Next(i + 1);
                Card temp = CardDeck[i];
                CardDeck[i] = CardDeck[j];
                CardDeck[j] = temp;
                Count++;
            }
        }


        public void Cut()
        {
            Random rng = new Random();
            int cutDepth = rng.Next(46)+ 3;
            Card[] cutDeck = new Card[DeckSize];
            Count = 0;

            for (int i = cutDepth; i< DeckSize; i++)
            {
                cutDeck[Count] = CardDeck[i];
                Count++;
            }            
            for (int i = 0; i < cutDepth; i++)
            {
                cutDeck[Count] = CardDeck[i];
                Count++;
            }
            CardDeck = cutDeck;
        }

        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i< DeckSize; i++ )
            {
                s += CardDeck[i].ToString();
                if (i != DeckSize - 1) s += ", ";
            }
            s += $"]\n{Count} cards in deck.";
            return s;
        }


    }

    public class Player
    {
        public Card[] Hand { get; private set; }
        public int HandSize { get; private set; }//5 cards

        public Player(int handSize = 3)
        {
            this.HandSize = handSize;
            Hand = new Card[HandSize];
            for (int i = 0; i< HandSize; i++)
            {
                Hand[i] = new Card();
            }
        }

        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i < HandSize; i++)
            {
                s += Hand[i].ToString();
                if (i != HandSize - 1) s += ", ";
            }
            s += "]\n";
            return s;
        }
    }


    public class Game
    {
        public Deck GameDeck { get; private set; }
        public int NumberOfPlayers { get; private set; } //4 players
        public Player[] GamePlayers { get; private set; }


        public Game(Deck cardDeck, int numberOfPlayers = 2, int handSize = 3)
        {
            this.GameDeck = new Deck(cardDeck);
            this.NumberOfPlayers = numberOfPlayers;
            GamePlayers = new Player[NumberOfPlayers];
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                GamePlayers[i] = new Player(handSize);
            }
            
        }
        

        public Deck DealCards()
        {
            int index = GameDeck.DeckSize - 1;
            int cardLimit = GamePlayers[0].HandSize;
            for (int card = 0; card < cardLimit; card++)
            {
                for (int player = 0; player < NumberOfPlayers; player++)
                {
                    GamePlayers[player].Hand[card] = GameDeck.CardDeck[index];
                    index--;
                }
            }

            Card[] newDeckArray = new Card[index+1];
            for (int i = 0; i <=index; i++)
            {
                newDeckArray[i] = GameDeck.CardDeck[i];
            }
            Deck newDeck = new Deck(newDeckArray);
            return newDeck;
        }


        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                s += GamePlayers[i].ToString();
            }           
            return s;
        }


    }




    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("New deck:");
            Deck deck = new Deck();
            Console.WriteLine(deck);
            Console.ReadLine();

            Console.WriteLine("Shuffled deck:");
            deck.Shuffle();
            Console.WriteLine(deck);
            Console.ReadLine();

            Console.WriteLine("Cut deck:");
            deck.Cut();
            Console.WriteLine(deck);
            Console.ReadLine();

            Console.WriteLine("Dealt hands:");
            Game game = new Game(deck,4,5);
            deck = game.DealCards();
            Console.WriteLine(game);
            Console.ReadLine();

            Console.WriteLine("Remaining cards in deck:");
            Console.WriteLine(deck);
            Console.ReadLine();
        }
    }
}
