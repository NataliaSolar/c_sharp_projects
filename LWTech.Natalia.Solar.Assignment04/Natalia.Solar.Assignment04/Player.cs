using System;


namespace Natalia.Solar.Assignment04
{
    public abstract class Player
    {
        public string Name { get; private set; }
        public Hand Hand { get; private set; }
        // Other Properties/member variables go here
        public Rank BookRank { get; private set; }
        public int Score { get; private set; }

        public Player(string name)
        {
            this.Name = name;
            this.Hand = new Hand();
            this.BookRank = 0;
            this.Score = 0;
        }

        public abstract Player ChoosePlayerToAsk(Player[] players);
        public abstract Rank ChooseRankToAskFor();


        public bool HasBookInHand()
        {            
            int count;
            for (int currentCardIndex  = 0; currentCardIndex< Hand.GetCardsInHand().Length; currentCardIndex++)
            {
                count = 1;
                for (int i = 0; i< Hand.GetCardsInHand().Length; i++)
                {
                    if (currentCardIndex == i)
                    {
                        continue;
                    }
                    if (Hand.GetCardsInHand()[currentCardIndex].Rank== Hand.GetCardsInHand()[i].Rank)
                    {
                        count++;
                    }
                    if(count == 4)
                    {
                        this.BookRank = Hand.GetCardsInHand()[currentCardIndex].Rank;
                        this.Score += 1;
                        return true;
                    }
                }
            }
            return false;
        }




        public string WinnerToString(Player[] players)
        {
            int highestScore = players[0].Score;
            int winnersCount = 0;
            string s = "";
            foreach (Player player in players)
            {
                if (player.Score >= highestScore)
                {
                    highestScore = player.Score;
                    winnersCount++;
                } 
            }

            s = "The winner is ";
            foreach (Player player in players)
            {
                if (player.Score == highestScore)
                {
                    s += $"{player.Name} with {player.Score} points ";  
                }
            }
            s += "!";

            return s;
        }



        public Card FindRankInHand(Rank rank)
        {
            foreach (Card card in Hand.GetCardsInHand())
            {
                if (card.Rank==rank)
                {
                    return new Card(card.Rank, card.Suit);
                }
            }
            return null;
        }



        public int GetNewCardsInHand(Deck deck)
        {
            if(deck.DeckSize() == 0) throw new NullReferenceException("You are trying to get cards from an empty deck.");
            int newHand = 0;
            int count = 0;
            if (deck.DeckSize() < 5)
            {

                for (newHand = 0; newHand < deck.DeckSize(); newHand++)
                {
                    Hand.AddCard(deck.DealCard());
                    count++;
                }
            }
            else
            {
                for (newHand = 0; newHand < 5; newHand++)
                {
                    Hand.AddCard(deck.DealCard());
                    count++;
                }
            }
            return count;
        }

        

        // Other Player methods go here

        public override string ToString()
        {
            string s = Name + "'s Hand: ";
            s += Hand.ToString();
            return s;
        }
    }



    public class RandomPlayer : Player
    {
        private static Random rng = new Random();
        public RandomPlayer(string name) : base(name)
        {  }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            int playerIndex =-1;
            do
            {
                playerIndex = rng.Next(players.Length);
            } while (this.Name == players[playerIndex].Name);
            return players[playerIndex];
        }

        public override Rank ChooseRankToAskFor()
        {
            int cardIndex = rng.Next(Hand.HandSize());
            
            return Hand.GetCardsInHand()[cardIndex].Rank;

        }

    }


    //A player that always chooses the first card in their hand and the first player on their right.
    public class FirstCardFirstRightPlayer : Player
    {
        public FirstCardFirstRightPlayer(string name) : base(name)
        { }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            int playerIndex = -1;
            for (int i = 0; i<players.Length; i++)
            {
                if(this.Name == players[i].Name)
                {
                    playerIndex = i;
                }
            }
            if (playerIndex == 0)
                return players[players.Length - 1];
            else
                return players[playerIndex - 1];
        }


        public override Rank ChooseRankToAskFor()
        {
            int cardIndex = 0;

            return Hand.GetCardsInHand()[cardIndex].Rank;

        }
    }

    //A player that always chooses the last card in their hand and the first player on their left.
    public class LastCardFirstLeftPlayer : Player
    {
        public LastCardFirstLeftPlayer(string name) : base(name)
        { }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            int playerIndex = -1;
            for (int i = 0; i < players.Length; i++)
            {
                if (this.Name == players[i].Name)
                {
                    playerIndex = i;
                }
            }
            if (playerIndex == players.Length - 1)
                return players[0];
            else
                return players[playerIndex + 1];
        }


        public override Rank ChooseRankToAskFor()
        {
            int cardIndex = Hand.HandSize()-1;

            return Hand.GetCardsInHand()[cardIndex].Rank;

        }
    }


    //A player that always chooses the last card in their hand but asks a random player.
    public class LastCardRandomPlayer : Player
    {
        private static Random rng = new Random();

        public LastCardRandomPlayer(string name) : base(name)
        { }

        public override Player ChoosePlayerToAsk(Player[] players)
        {
            int playerIndex = -1;
            do
            {
                playerIndex = rng.Next(players.Length);
            } while (this.Name == players[playerIndex].Name);
            return players[playerIndex];
        }


        public override Rank ChooseRankToAskFor()
        {
            int cardIndex = Hand.HandSize() - 1;

            return Hand.GetCardsInHand()[cardIndex].Rank;

        }
    }
}
