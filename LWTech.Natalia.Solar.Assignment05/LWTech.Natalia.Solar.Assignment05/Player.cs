using System;
using System.Collections.Generic;

namespace Natalia.Solar.Assignment04
{
    public abstract class Player
    {
        public string Name { get; private set; }
        public Hand Hand { get; private set; }
        public Rank BookRank { get; private set; }
        public int Score { get; private set; }

        public Player(string name)
        {
            this.Name = name;
            this.Hand = new Hand();
            this.BookRank = 0;
            this.Score = 0;
        }

        public abstract Player ChoosePlayerToAsk(List<Player> players);
        public abstract Rank ChooseRankToAskFor();


        public bool HasBookInHand()
        {
            int count;
            for (int currentCardIndex = 0; currentCardIndex < Hand.GetCardsInHand().Count; currentCardIndex++)
            {
                count = 1;
                for (int i = 0; i < Hand.GetCardsInHand().Count; i++)
                {
                    if (currentCardIndex == i)
                    {
                        continue;
                    }
                    if (Hand.GetCardsInHand()[currentCardIndex].Rank == Hand.GetCardsInHand()[i].Rank)
                    {
                        count++;
                    }
                    if (count == 4)
                    {
                        this.BookRank = Hand.GetCardsInHand()[currentCardIndex].Rank;
                        this.Score += 1;
                        return true;
                    }
                }
            }
            return false;
        }







        public Card FindRankInHand(Rank rank)
        {
            foreach (Card card in Hand.GetCardsInHand())
            {
                if (card.Rank == rank)
                {
                    return new Card(card.Rank, card.Suit);
                }
            }
            return null;
        }



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
        public RandomPlayer(string name) : base(name + "(Rnd)")
        { }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            int playerIndex = -1;
            do
            {
                playerIndex = rng.Next(players.Count);
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
        public FirstCardFirstRightPlayer(string name) : base(name + "(RS)")
        { }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            int playerIndex = -1;
            for (int i = 0; i < players.Count; i++)
            {
                if (this.Name == players[i].Name)
                {
                    playerIndex = i;
                }
            }
            if (playerIndex == 0)
                return players[players.Count - 1];
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
        public LastCardFirstLeftPlayer(string name) : base(name + "(LS)")
        { }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            int playerIndex = -1;
            for (int i = 0; i < players.Count; i++)
            {
                if (this.Name == players[i].Name)
                {
                    playerIndex = i;
                }
            }
            if (playerIndex == players.Count - 1)
                return players[0];
            else
                return players[playerIndex + 1];
        }


        public override Rank ChooseRankToAskFor()
        {
            int cardIndex = Hand.HandSize() - 1;

            return Hand.GetCardsInHand()[cardIndex].Rank;

        }
    }


    //A player that always chooses the last card in their hand but asks a random player.
    public class LastCardRandomPlayer : Player
    {
        private static Random rng = new Random();

        public LastCardRandomPlayer(string name) : base(name + "(LRnd)")
        { }

        public override Player ChoosePlayerToAsk(List<Player> players)
        {
            int playerIndex = -1;
            do
            {
                playerIndex = rng.Next(players.Count);
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
