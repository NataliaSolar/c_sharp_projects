using System;
using System.Collections;
using System.Collections.Generic;

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
        private Stack<Card> cardDeck;
        private static Random rng = new Random();

        public Deck()
        {
            int deckSize = Enum.GetValues(typeof(Rank)).Length * Enum.GetValues(typeof(Suit)).Length;
            cardDeck = new Stack<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cardDeck.Push(new Card(rank, suit));
                }
            }
        }


        public int DeckSize()
        {
            return cardDeck.Count;
        }


        // Fisher-Yates algorithm source: https://exceptionnotfound.net/understanding-the-fisher-yates-card-shuffling-algorithm/
        public void Shuffle()
        {
            if (DeckSize() == 0) throw new NullReferenceException("You are trying to shuffle an empty deck.");

            Card[] cardDeckArray = cardDeck.ToArray();
            for (int i = DeckSize() - 1; i > 0; --i)
            {
                int j = rng.Next(i + 1);
                Card temp = cardDeckArray[i];
                cardDeckArray[i] = cardDeckArray[j];
                cardDeckArray[j] = temp;
            }
            cardDeck = new Stack<Card>(cardDeckArray);
        }


        public void Cut()
        {
            if (DeckSize() == 0) throw new NullReferenceException("You are trying to cut an empty deck.");
            int cutPoint = rng.Next(3, DeckSize() - 3);
            Card[] cutDeck = new Card[DeckSize()];
            int count = 0;
            Card[] cardDeckArray = cardDeck.ToArray();

            for (int i = cutPoint; i < DeckSize(); i++)
            {
                cutDeck[count] = cardDeckArray[i];
                count++;
            }
            for (int i = 0; i < cutPoint; i++)
            {
                cutDeck[count] = cardDeckArray[i];
                count++;
            }
            cardDeck = new Stack<Card>(cutDeck);
        }


        public Card DealCard()
        {
            if (DeckSize() == 0) throw new NullReferenceException("You are trying to deal from an empty deck.");
            return cardDeck.Pop();
        }


        public override string ToString()
        {
            int i =0;
            string s = "[";
            foreach (Card card in cardDeck)
            {
                s += card.ToString();
                if (i != DeckSize() - 1)
                    s += ", ";
                i++;
            }
            s += $"]\n{DeckSize()} cards in deck.";
            return s;
        }
    }


    public class Hand
    {
        private List<Card> cardsInHand;

        public Hand()
        {
            cardsInHand = new List<Card>();
        }

        public int HandSize()
        {
            return cardsInHand.Count;
        }



        public List<Card> GetCardsInHand()
        {
            List<Card> cardsInHandCopy = new List<Card>(cardsInHand);
            return cardsInHandCopy;
        }



        public void AddCard(Card cardToAdd)
        {
            cardsInHand.Add(cardToAdd);
        }



        public void RemoveCard(Card cardToRemove)
        {
            if (cardToRemove == null) throw new NullReferenceException("The null is passed as an argument.");            
            int i = 0;
            foreach (Card card in cardsInHand)
            {
                if (card.Rank == cardToRemove.Rank && card.Suit == cardToRemove.Suit)
                {
                    cardsInHand.RemoveAt(i);
                    break;
                }                    
                i++;
            }            
        }




        public override string ToString()
        {
            int i = 0;
            string s = "[";
            foreach (Card card in cardsInHand)
            {
                s += card.ToString();
                if (i != cardsInHand.Count - 1) s += ", ";
                i++;
            }
            s += "]\n";
            return s;
        }
    }
}



