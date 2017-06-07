﻿using System;
using System.Collections.Generic;

namespace Blackjack
{   
    class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();
            Player player = new Player("Bob", dealer);
            player.Cash = 1000;

            for (;;)
            {
                dealer.Hand.Clear();
                player.Hand.Clear();
                dealer.PrepareDeckToTheNewGame();

                Console.WriteLine("=================================================");
                dealer.Deal(dealer.Hand);
                dealer.Deal(player.Hand);

                PrintDealerCards(dealer);

                int initialCash = player.Cash;

                Console.WriteLine();
                Console.WriteLine($"#{player.Name}'s cash: {player.Cash}");
                Console.WriteLine($"#{player.Name}, type a bet you wish: ");

                int betValue = int.Parse(Console.ReadLine());
                player.BetValue = betValue;

                Console.WriteLine();
                PrintPlayerCards(player);

                Console.WriteLine();

                string action;
                do
                {
                    Console.WriteLine("What do you want to do? (h/s)");
                    action = Console.ReadLine();

                    switch (action)
                    {
                        case "h":
                            player.Hit();
                            break;
                        case "s":
                            break;
                        default:
                            Console.WriteLine("Invalid action, try again. ");
                            break;
                    }
                    Console.WriteLine();

                    PrintPlayerCards(player);
                } while (action != "s" && player.Hand.TotalValue <= 21);

                Console.WriteLine();
                Console.WriteLine("--------------------------");

                while (dealer.Hand.TotalValue <= 17)
                {
                    dealer.GiveCard(dealer.Hand);
                }

                dealer.Hand.Show();
                PrintDealerCards(dealer);
                Console.WriteLine();

                var totalPlayerValue = player.Hand.TotalValue;
                var totalDealerValue = dealer.Hand.TotalValue;

                if (totalPlayerValue == totalDealerValue)
                {
                    Console.WriteLine("***DRAW***");
                    dealer.ReturnMoney(player);
                }
                else if ((totalPlayerValue <= 21 && totalPlayerValue > totalDealerValue) || (totalDealerValue > 21 && totalPlayerValue <= 21))
                {
                    dealer.GivePrize(player);
                    Console.WriteLine($"#{player.Name} - WON");
                }
                else
                {
                    Console.WriteLine($"#{player.Name} - LOST");
                }
            }
        }   

        static void PrintPlayerCards(Player player)
        {
            Console.WriteLine($"#{player.Name}'s cards: <{player.Hand.TotalValue}>");
            foreach(var card in player.Hand.Cards)
            {
                Console.WriteLine(card.ToString());
            }
        }

        static void PrintDealerCards(Dealer dealer)
        {
            Console.WriteLine($"Dealer's cards: <{dealer.Hand.FaceValue}>");
            foreach (var card in dealer.Hand.Cards)
            {
                Console.WriteLine(card.IsFaceUp ? card.ToString() : "XXX");
            }
        }
    }
}