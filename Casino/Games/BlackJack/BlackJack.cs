using MyBJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Games
{
    class BlackJack
    {
            private static int MinimumBet { get; set; } = 10;
            private static Deck deck1 = new Deck();
            private static Player player1 = new Player();
            private static Dealer dealer1 = new Dealer();

            MainMenu mainMenu = new MainMenu();


            public enum RoundResult
            {
                Push,
                PlayerWin,
                PlayerBust,
                PlayerBlackJack,
                DealerWin,
                Surrender,
                InvalidBet
            }


            /// <summary>
            /// Проверяет БлекДжек ли на руках
            /// </summary>
            /// <param name="hands">Проверяет БлекДжек ли на руках</param>
            /// <returns>Да - если на руках Блекджек</returns>
            public bool IsBlackJack(List<Card> hands)
            {
                if (hands.Count == 2)
                {
                    if (hands[0].Face == Card.CardFace.Ace && hands[1].Value == 10)
                    {
                        return true;
                    }
                    else if (hands[1].Face == Card.CardFace.Ace && hands[0].Value == 10)
                    {
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Раздача карт игроку и дилеру и вывод на консоль
            /// </summary>
            public void DistributeHands()
            {
                deck1.CreateDeck();
                player1.Hand = deck1.DealHand();
                dealer1.HiddenCard = deck1.DealHand();
                dealer1.RevealedCards = new List<Card>();

                // Если на руках два туза, далает очко одно Туза равной 1 (малый Туз)
                if (player1.Hand[0].Face == Card.CardFace.Ace && player1.Hand[1].Face == Card.CardFace.Ace)
                {
                    player1.Hand[1].Value = 1;
                }

                if (dealer1.HiddenCard[0].Face == Card.CardFace.Ace && dealer1.HiddenCard[1].Face == Card.CardFace.Ace)
                {
                    dealer1.HiddenCard[1].Value = 1;
                }
                dealer1.ReavelCard();
                player1.PrintInfo();
                dealer1.PrintInfo();
            }

            /// <summary>
            /// Запускает полный процесс одного раунда.
            /// </summary>
            public void StartRound()
            {
                Console.Clear();
                if (TakeBet() == false)
                {
                    EndRound(RoundResult.InvalidBet);
                    return;
                }
                Console.Clear();
                DistributeHands();
                TakeAction();

                dealer1.ReavelCard();

                Console.Clear();
                player1.PrintInfo();
                dealer1.PrintInfo();

                player1.RoundsPlayed++;
                if (player1.Hand.Count == 0)
                {
                    EndRound(RoundResult.Surrender);
                    return;
                }
                else if (player1.GetHandValue() > 21)
                {
                    EndRound(RoundResult.PlayerBust);
                    return;
                }

                while (dealer1.GetHandValue() <= 16)
                {
                    Thread.Sleep(1000);
                    dealer1.RevealedCards.Add(deck1.TakeExtraCard());

                    Console.Clear();
                    player1.PrintInfo();
                    dealer1.PrintInfo();
                }

                if (player1.GetHandValue() > dealer1.GetHandValue())
                {
                    player1.Wins++;
                    if (IsBlackJack(player1.Hand) == true)
                    {
                        EndRound(RoundResult.PlayerBlackJack);
                    }
                    else
                    {
                        EndRound(RoundResult.PlayerWin);
                    }
                }
                else if (dealer1.GetHandValue() > 21)
                {
                    player1.Wins++;
                    EndRound(RoundResult.PlayerWin);
                }
                else if (player1.GetHandValue() < dealer1.GetHandValue())
                {
                    EndRound(RoundResult.DealerWin);
                }
                else
                {
                    EndRound(RoundResult.Push);
                }
            }

            /// <summary>
            /// Выводит итоги раунда согласно результату и запускает новый раунд.
            /// При окончании фишек, дополнительно выдаются фишки путем создания нового объекта класса "Игрок"
            /// </summary>
            /// <param name="result">Результат раунда</param>
            public void EndRound(RoundResult result)
            {
                switch (result)
                {
                    case RoundResult.Push:
                        player1.ReturnBet();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("РОВНО! У Вас и у дилера одинаковые очки.");
                        break;
                    case RoundResult.Surrender:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"ВЫ ОТКАЗАЛИСЬ ОТ РАУНДА! Возвращена половина ставки. Возвращено {player1.Bet / 2} фишек.");
                        PersonChek.money += player1.Bet / 2;
                        player1.ClearBet();
                        break;
                    case RoundResult.PlayerWin:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"ВЫ ВЫИГРАЛИ. Выиграно {player1.WinBet(false)} фишек.");
                        break;
                    case RoundResult.PlayerBlackJack:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"У ВАС БЛЭК ДЖЕК. Выиграно {player1.WinBet(true)} фишек.");
                        break;
                    case RoundResult.PlayerBust:
                        player1.ClearBet();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"ВЫ ПРОИГРАЛИ.");
                        break;
                    case RoundResult.DealerWin:
                        player1.ClearBet();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" ДИЛЕР ВЫИГРАЛ. ");
                        break;
                    case RoundResult.InvalidBet:
                        player1.ClearBet();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" НЕВЕРНАЯ СТАВКА. ");
                        break;
                }

                
                if (PersonChek.money <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" У ВАС ЗАКОНЧИЛИСЬ ФИШКИ. ВЫ ПРОИГРАЛИ на {player1.RoundsPlayed - 1} раунде! ");
                    Console.WriteLine("Вам выдано 500 фишек и обновлены все статистики сессии");
                    player1 = new Player();
                }

                mainMenu.Exit();
                StartRound();
            }


            /// <summary>
            /// Игрок делает ставку
            /// </summary>
            public static bool TakeBet()
            {
                Console.Write("Количество Фишек: ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(PersonChek.money);
                ResetColor();

                Console.Write("Минимальная ставка: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(MinimumBet);
                ResetColor();

                Console.Write($"Делайте ставку для начала {player1.RoundsPlayed} раунда: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                int bet = Convert.ToInt32(Console.ReadLine());
                ResetColor();
                if (bet >= MinimumBet && PersonChek.money >= bet)
                {
                    player1.MakeBet(bet);
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Дальнейшие дейтвия игрока после получения карт на руки.
            /// </summary>
            public static void TakeAction()
            {
                List<string> action = new List<string>()
            {
                "Взять карту",
                "Оставить как есть (стэнд)",
                "Отказаться от раунда",
                "Удвоить ставку и получить карту"
            };
                int playerChoice = 0;
                do
                {
                    Console.Clear();
                    player1.PrintInfo();
                    dealer1.PrintInfo();
                    Console.WriteLine("Действия: ");
                    for (int i = 0; i < action.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {action[i]}");
                    }

                    Console.Write("Ваши Действия: ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    playerChoice = Convert.ToInt32(Console.ReadLine());
                    ResetColor();
                    switch (playerChoice)
                    {
                        case 1:
                            player1.Hand.Add(deck1.TakeExtraCard());
                            break;
                        case 2:
                            break;
                        case 3:
                            player1.Hand.Clear();
                            break;
                        case 4:
                            if (PersonChek.money <= player1.Bet)
                            {
                                player1.MakeBet(PersonChek.money);
                            }
                            else
                            {
                                player1.MakeBet(player1.Bet);
                            }
                            player1.Hand.Add(deck1.TakeExtraCard());
                            break;
                        default:
                            Console.WriteLine("Сделайте правильное действие!");
                            Console.WriteLine("Нажмите на любую клавишу для выбора действия...");
                            Console.ReadKey();
                            break;
                    }
                    if (player1.GetHandValue() > 21)
                    {
                        foreach (Card card in player1.Hand)
                        {
                            if (card.Value == 11)
                            {
                                card.Value = 1;
                                break;
                            }
                        }
                    }
                } while (!playerChoice.Equals(2)
                         && !playerChoice.Equals(4)
                         && !playerChoice.Equals(3)
                         && player1.GetHandValue() <= 21);
            }

            /// <summary>
            /// Точка входа в игру
            /// </summary>
            public void Start()
            {
                mainMenu.Exit();
                StartRound();
            }

            /// <summary>
            /// Сбрасывает на указанный цвет шрифта на консоле
            /// </summary>
            public static void ResetColor()
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.BackgroundColor = ConsoleColor.Black;
            }

        }
    }

