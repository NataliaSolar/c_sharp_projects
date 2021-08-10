using System;
using System.Collections.Generic;

namespace Natalia.Solar.Assignment04
{
    class Program
    {
        static void Main(string[] args)
        {

            bool gameOver = false;
            Player currentPlayer;
            Player playerWhoWasAsked = null;
            Rank askedRank;
            bool turnIsOver = false;
            bool goFish = false;
            Card drawnCard;
            Card cardWithAskedRank;
            int turn = 0;


            Console.WriteLine("Go Fish Simulation(Multiplayer)\n================================");

            Deck deck = new Deck();
            deck.Shuffle();
            deck.Cut();

            List<Player> players = new List<Player>();

            players.Add(new RandomPlayer("Lisa"));
            players.Add(new LastCardFirstLeftPlayer("Mike"));
            players.Add(new FirstCardFirstRightPlayer("Tom"));
            players.Add(new LastCardRandomPlayer("Bill"));


            for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        players[j].Hand.AddCard(deck.DealCard());
                    }
                }


            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine(players[i]);
            }

            DisplayScoreBoard(players, deck);


            Console.ReadLine();

            do
            {
                if (players[0].Hand.HandSize() == 0 && players[1].Hand.HandSize() == 0 && players[2].Hand.HandSize() == 0 && players[3].Hand.HandSize() == 0 && deck.DeckSize() == 0)
                {
                    gameOver = true;
                }

                foreach (Player player in players)
                {
                    turn++;
                    turnIsOver = false;
                    currentPlayer = player;
                    if (currentPlayer.Hand.HandSize() == 0)
                    {
                        continue;
                    }

                    Console.WriteLine($"\nIt is now {currentPlayer.Name}'s turn.");

                    turnIsOver = false;
                    while (!turnIsOver && !gameOver)
                    {
                        goFish = false;
                        while (!goFish && !gameOver)
                        {
                            askedRank = currentPlayer.ChooseRankToAskFor();
                            playerWhoWasAsked = currentPlayer.ChoosePlayerToAsk(players);
                            Console.WriteLine($"{currentPlayer.Name} says: {playerWhoWasAsked.Name}! Give me all of your {askedRank}s!");
                            cardWithAskedRank = playerWhoWasAsked.FindRankInHand(askedRank);


                            if (cardWithAskedRank == null)
                            {
                                Console.WriteLine($"{playerWhoWasAsked.Name} says: GO FISH!");
                                goFish = true;
                            }

                            while (cardWithAskedRank != null)
                            {
                                playerWhoWasAsked.Hand.RemoveCard(cardWithAskedRank);
                                currentPlayer.Hand.AddCard(cardWithAskedRank);
                                Console.WriteLine($"{currentPlayer.Name} gets the {cardWithAskedRank} from {playerWhoWasAsked.Name}.");
                                cardWithAskedRank = playerWhoWasAsked.FindRankInHand(askedRank);
                            }

                            GetNewCardsInHandIfEmty(deck, playerWhoWasAsked);

                            if (currentPlayer.HasBookInHand())
                            {
                                PlayingABook(currentPlayer);

                                DisplayScoreBoard(players, deck);
                                goFish = false;

                                GetNewCardsInHandIfEmty(deck, currentPlayer);

                                if (currentPlayer.Hand.HandSize() == 0 && deck.DeckSize() == 0)
                                {
                                    break;
                                }
                            }

                            if (currentPlayer.Hand.HandSize() != 0 && !goFish)
                            {
                                Console.WriteLine($"It is still {currentPlayer.Name}'s turn.  {currentPlayer.Hand}");
                            }
                        }

                        if (deck.DeckSize() != 0)
                        {
                            drawnCard = deck.DealCard();
                            currentPlayer.Hand.AddCard(drawnCard);
                            Console.WriteLine($"{currentPlayer.Name} draws a {drawnCard} from the deck.The deck now has {deck.DeckSize()} cards remaining.");

                            if (currentPlayer.HasBookInHand())
                            {
                                PlayingABook(currentPlayer);

                                DisplayScoreBoard(players, deck);
                                Console.WriteLine($"It is still {currentPlayer.Name}'s turn.  {currentPlayer.Hand}");
                                    goFish = false;
                            }
                            else
                            {
                                turnIsOver = true;
                            }

                        }
                        else
                        {
                            Console.WriteLine($"Deck is empty. {currentPlayer.Name} cannot draw a card.");
                            turnIsOver = true;
                        }
                    }


                Console.WriteLine($"{currentPlayer.Name}'s turn is over. {currentPlayer.Hand}");
                DisplayScoreBoard(players, deck);
            }
                    //Console.ReadLine();
            } while (!gameOver);


            Console.WriteLine("\n\n============== Game Over! =================\n\n");


            DisplayScoreBoard(players, deck);

            bool tieGame = false;
            Player winner = players[0];
            for (int i = 1; i < players.Count; i++)
            {
                if (players[i].Score > winner.Score)
                {
                    tieGame = false;
                    winner = players[i];
                }
                else if (players[i].Score == winner.Score)
                {
                    tieGame = true;
                }

            }

            Console.WriteLine("\nAfter " + turn + " turns,");
            if (tieGame)
                Console.WriteLine("It's a tie!");
            else
                Console.WriteLine("The winner is " + winner.Name + " with " + winner.Score + " points!");

            Console.ReadLine();
        }




        private static void DisplayScoreBoard(List<Player> players, Deck deck)
        {
            if (players == null) throw new ArgumentNullException("Null is passed as an argument.");
            Console.WriteLine($"SCORE:  | {players[0].Name}: {players[0].Score} | {players[1].Name}: {players[1].Score} |" +
                            $" {players[2].Name}: {players[2].Score} | {players[3].Name}: {players[3].Score} |  [Deck: {deck.DeckSize()}]");
        }


        private static void GetNewCardsInHandIfEmty(Deck deck, Player player)
        {
            if (deck.DeckSize() == 0) return;
            if (player.Hand.HandSize() != 0) return;
            int newHand = 0;
            int newHandSize = 0;

            if (deck.DeckSize() < 5)
            {

                for (newHand = 0; newHand < deck.DeckSize(); newHand++)
                {
                    player.Hand.AddCard(deck.DealCard());
                    newHandSize++;
                }
            }
            else
            {
                for (newHand = 0; newHand < 5; newHand++)
                {
                    player.Hand.AddCard(deck.DealCard());
                    newHandSize++;
                }
            }

            Console.WriteLine($"{player.Name}'s hand is empty. {player.Name} draws a {newHandSize} cards from the deck." +
                    $"The deck now has {deck.DeckSize()} cards remaining. {player.Hand}");
        }


        private static void PlayingABook(Player player)
        {
            if (player == null) throw new ArgumentNullException("Null is passed as an argument.");
            Card bookCardToRemove;
            Console.WriteLine($">>> {player.Name} HAS A BOOK! PLAYING A BOOK OF {player.BookRank}S!".ToUpper());
            bookCardToRemove = player.FindRankInHand(player.BookRank);
            while (bookCardToRemove != null)
            {
                player.Hand.RemoveCard(bookCardToRemove);
                bookCardToRemove = player.FindRankInHand(player.BookRank);
            }
        }
    }
}
