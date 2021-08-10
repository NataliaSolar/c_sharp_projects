using System;


namespace Natalia.Solar.Assignment04
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                bool gameOver = false;
                Player currentPlayer;
                Player playerWhoWasAsked = null;
                Rank askedRank;
                bool turnIsOver = false;
                bool goFish = false;
                Card drawnCard;
                Card cardWithAskedRank;
                Card bookCardToRemove;
                int turn = 0;
                string winner;


                Console.WriteLine("Go Fish Simulation(Multiplayer)\n================================");

                Deck deck = new Deck();
                deck.Shuffle();
                deck.Cut();

                Player[] players = new Player[4];

                players[0] = new RandomPlayer("Lisa (Rnd)");
                players[1] = new LastCardFirstLeftPlayer("Mike (lastL)");
                players[2] = new FirstCardFirstRightPlayer("Tom (1stR)");
                players[3] = new LastCardRandomPlayer("Bill (lastRnd)");

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        players[j].Hand.AddCard(deck.DealCard());
                    }
                }


                for (int i = 0; i < players.Length; i++)
                {
                    Console.WriteLine(players[i]);
                }

                Console.WriteLine($"SCORE:  | {players[0].Name}: {players[0].Score} | {players[1].Name}: {players[1].Score} |" +
                    $" {players[2].Name}: {players[2].Score} | {players[3].Name}: {players[3].Score} |  [Deck: {deck.DeckSize()}]");



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
                                    cardWithAskedRank = playerWhoWasAsked.Hand.RemoveCard(cardWithAskedRank);
                                    currentPlayer.Hand.AddCard(cardWithAskedRank);
                                    Console.WriteLine($"{currentPlayer.Name} gets the {cardWithAskedRank} from {playerWhoWasAsked.Name}.");
                                    cardWithAskedRank = playerWhoWasAsked.FindRankInHand(askedRank);
                                }

                                if (playerWhoWasAsked.Hand.HandSize() == 0 && deck.DeckSize() != 0)
                                {
                                    int newHandSize;
                                    newHandSize = playerWhoWasAsked.GetNewCardsInHand(deck);
                                    Console.WriteLine($"{playerWhoWasAsked.Name}'s hand is empty. {playerWhoWasAsked.Name} draws a {newHandSize} cards from the deck." +
                                        $"The deck now has {deck.DeckSize()} cards remaining. {playerWhoWasAsked.Hand}");
                                }

                                if (currentPlayer.HasBookInHand())
                                {
                                    Console.WriteLine($">>> {currentPlayer.Name} HAS A BOOK! PLAYING A BOOK OF {currentPlayer.BookRank}S!".ToUpper());
                                    bookCardToRemove = currentPlayer.FindRankInHand(currentPlayer.BookRank);
                                    while (bookCardToRemove != null)
                                    {
                                        currentPlayer.Hand.RemoveCard(bookCardToRemove);
                                        bookCardToRemove = currentPlayer.FindRankInHand(currentPlayer.BookRank);
                                    }

                                    Console.WriteLine($"SCORE:  | {players[0].Name}: {players[0].Score} | {players[1].Name}: {players[1].Score} |" +
                                                                    $" {players[2].Name}: {players[2].Score} | {players[3].Name}: {players[3].Score} |  [Deck: {deck.DeckSize()}]");
                                    goFish = false;

                                    if (currentPlayer.Hand.HandSize() == 0 && deck.DeckSize() != 0)
                                    {
                                        int newHandSize;
                                        newHandSize = currentPlayer.GetNewCardsInHand(deck);
                                        Console.WriteLine($"{currentPlayer.Name}'s hand is empty. {currentPlayer.Name} draws a {newHandSize} cards from the deck." +
                                            $"The deck now has {deck.DeckSize()} cards remaining. {currentPlayer.Hand}");
                                    }

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

                            if (gameOver)
                            {
                                break;
                            }

                            if (deck.DeckSize() != 0)
                            {
                                drawnCard = deck.DealCard();
                                currentPlayer.Hand.AddCard(drawnCard);
                                Console.WriteLine($"{currentPlayer.Name} draws a {drawnCard} from the deck.The deck now has {deck.DeckSize()} cards remaining.");

                                if (currentPlayer.HasBookInHand())
                                {
                                    Console.WriteLine($">>> {currentPlayer.Name} HAS A BOOK! PLAYING A BOOK OF {currentPlayer.BookRank}S!".ToUpper());
                                    bookCardToRemove = currentPlayer.FindRankInHand(currentPlayer.BookRank);
                                    while (bookCardToRemove != null)
                                    {
                                        currentPlayer.Hand.RemoveCard(bookCardToRemove);
                                        bookCardToRemove = currentPlayer.FindRankInHand(currentPlayer.BookRank);
                                    }

                                    Console.WriteLine($"SCORE:  | {players[0].Name}: {players[0].Score} | {players[1].Name}: {players[1].Score} |" +
                                                                    $" {players[2].Name}: {players[2].Score} | {players[3].Name}: {players[3].Score} |  [Deck: {deck.DeckSize()}]");
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
                        Console.WriteLine($"SCORE:  | {players[0].Name}: {players[0].Score} | {players[1].Name}: {players[1].Score} |" +
                            $" {players[2].Name}: {players[2].Score} | {players[3].Name}: {players[3].Score} |  [Deck: {deck.DeckSize()}]");
                    }
                    // Console.ReadLine();
                } while (!gameOver);


                Console.WriteLine("\n\n============== Game Over! =================\n\n");


                Console.WriteLine($"SCORE:  | {players[0].Name}: {players[0].Score} | {players[1].Name}: {players[1].Score} |" +
                            $" {players[2].Name}: {players[2].Score} | {players[3].Name}: {players[3].Score} |  [Deck: {deck.DeckSize()}]");

                winner = players[0].WinnerToString(players);

                Console.WriteLine($"After {turn} turns, \n{winner}");

                Console.ReadLine();
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine($"Exception caught! {e.Message}");
            }

        }
    }
}
