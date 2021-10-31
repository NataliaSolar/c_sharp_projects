using System;


namespace Natalia.Solar.Assignment04
{
    public enum Suit { Hearts, Diamonds, Clubs, Spades };
    public enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };

    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }



        public Card(Rank rank, Suit suit)
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
        private Card[] cardDeck;
        private static Random rng = new Random();

        public Deck()
        {
            int deckSize = Enum.GetValues(typeof(Rank)).Length* Enum.GetValues(typeof(Suit)).Length;
            cardDeck = new Card[deckSize];
            int count = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {

                    cardDeck[count] = new Card(rank, suit);
                    count++;
                }
            }
        }


        public int DeckSize()
        {
            return cardDeck.Length;
        }


        // Fisher-Yates algorithm source: https://exceptionnotfound.net/understanding-the-fisher-yates-card-shuffling-algorithm/
        public void Shuffle()
        {
            if (DeckSize() == 0) throw new NullReferenceException("You are trying to shuffle an empty deck.");

            for (int i = DeckSize() - 1; i > 0; --i)
            {
                int j = rng.Next(i + 1);
                Card temp = cardDeck[i];
                cardDeck[i] = cardDeck[j];
                cardDeck[j] = temp;
            }
        }


        public void Cut()
        {
            if (DeckSize() == 0) throw new NullReferenceException("You are trying to cut an empty deck.");
            int cutPoint = rng.Next(3, DeckSize() - 3);
            Card[] cutDeck = new Card[DeckSize()];
            int count = 0;

            for (int i = cutPoint; i < DeckSize(); i++)
            {
                cutDeck[count] = cardDeck[i];
                count++;
            }
            for (int i = 0; i < cutPoint; i++)
            {
                cutDeck[count] = cardDeck[i];
                count++;
            }
            cardDeck = cutDeck;
        }


        public Card DealCard()
        {
            if (DeckSize() == 0) throw new NullReferenceException("You are trying to deal from an empty deck."); 

            Card dealtCard = cardDeck[DeckSize() - 1];             
            Array.Resize(ref cardDeck, DeckSize() - 1);

            return dealtCard;
        }


        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i < DeckSize(); i++)
            {
                s += cardDeck[i].ToString();
                if (i != DeckSize() - 1) s += ", ";
            }
            s += $"]\n{DeckSize()} cards in deck.";
            return s;
        }
    }


    public class Hand
    {
        private Card[] cardsInHand;

        public Hand()
        {
            cardsInHand = new Card[0];
        }

        public int HandSize()
        {
            return cardsInHand.Length;
        }

        public Card[] GetCardsInHand()
        {
            Card[] cardsInHandCopy = new Card[cardsInHand.Length];
            Array.Copy(cardsInHand, cardsInHandCopy, cardsInHand.Length);
            return cardsInHandCopy;
        }

        public void AddCard(Card cardToAdd)
        {
            Array.Resize(ref cardsInHand, HandSize() + 1);
            cardsInHand[HandSize() - 1] = cardToAdd;
        }

        public Card RemoveCard(Card cardToRemove)
        {
            if (cardToRemove == null) throw new NullReferenceException("The null is passed as an argument.");
            bool found = false;
            Card[] newCards = new Card[cardsInHand.Length - 1];

            int i = 0;
            foreach (Card c in cardsInHand)
            {
                if (c.Rank == cardToRemove.Rank && c.Suit == cardToRemove.Suit)
                    found = true;
                else
                    newCards[i++] = c;
            }

            if (found)
            {
                cardsInHand = newCards;
                return cardToRemove;
            }
            return null;
        }




        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i < cardsInHand.Length; i++)
            {
                s += cardsInHand[i].ToString();
                if (i != cardsInHand.Length - 1) s += ", ";
            }
            s += "]\n";
            return s;
        }
    }
}
