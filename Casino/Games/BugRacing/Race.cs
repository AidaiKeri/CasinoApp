using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Channels;

namespace Games.BugRacing
{
    public class Race
    {

        public static int bugNumbers = 5;  //Количество участников забега
        static Player player1 = new Player();
        public static List<Bug> AllBugs = new List<Bug>();
        public static MainMenu mainMenu = new MainMenu();

        /// <summary>
        /// Генерирует рандомный список Жуков на забег
        /// </summary>
        /// <param name="names">Список имен</param>
        /// <param name="colors">Список цветов</param>
        /// <param name="odds">Список Шансов</param>
        /// <returns>Возвращает рандомный Список Жуков</returns>
        public static List<Bug> GenerateBugList(List<string> names, List<ConsoleColor> colors, List<int> odds)
        {
            List<Bug> bugList = new List<Bug>();
            for (int i = 0; i < bugNumbers; i++)
            {
                Bug newBug = new Bug(i + 1, names[i], colors[i], odds[i]);
                bugList.Add(newBug);
            }
            return bugList;
        }

        /// <summary>
        /// Выводит на консоль информацию об игроке и всех жуков участвующих на забеге
        /// </summary>
        public static void MainPrintInfo()
        {
            player1.PrintInfo(true);
            Bug.PrintBugScreen(AllBugs);
        }


        /// <summary>
        /// Запускает раунд забега
        /// </summary>
        public static void Start()
        {
            Console.Clear();
            AllBugs = GenerateBugList(Bug.ShuffleBugName(Bug.Names), Bug.Colors, Bug.ShuffleBugOdd(Bug.BugOdds));
            player1.PrintInfo(false);
            Bug.PrintBugScreen(AllBugs);
            if (!BugChoosen())
            {
                Console.WriteLine("ВЫ ВВЕЛИ НЕВЕРНОЕ ЧИСЛО!");
                return;
            }
            if (!TakeBet())
            {
                Console.WriteLine("ВЫ ВВЕЛИ НЕВЕРНОЕ ЧИСЛО!");
                return;
            }
            Console.Clear();
            player1.PrintInfo(true);
            Bug.PrintBugScreen(AllBugs);
            RunRace(AllBugs);
            EndRound();
        }

        public static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        /// <summary>
        /// Выбор жука и ставки игрока
        /// </summary>
        public static bool BugChoosen()
        {
            int ymax = Console.WindowHeight - 1;
            int xmax = Console.WindowWidth - 2;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xmax / 2 + 1, 1);
            Console.WriteLine("-------------------------");
            Console.SetCursorPosition(xmax / 2 + 4, 2);
            Console.WriteLine("ДЕЛАЙТЕ ВАШИ СТАВКИ");
            Console.SetCursorPosition(xmax / 2 + 1, 3);
            Console.WriteLine("-------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(xmax / 2 + 1, 4);
            Console.Write("ВЫБЕРИТЕ НОМЕР ЖУКА: ");
            Console.ForegroundColor = ConsoleColor.Yellow;

            int playerChoice = Convert.ToInt16(Console.ReadLine());
            if (playerChoice <= AllBugs.Count && playerChoice > 0)
            {
                player1.BugFavourite = AllBugs[playerChoice - 1];
                player1.ColorOfBug = AllBugs[playerChoice - 1].BugColor;
                return true;
            }
            else if (playerChoice > AllBugs.Count)
            {
                player1.BugFavourite = AllBugs[AllBugs.Count - 1];
                return true;
            }
            return false;
        }

        public static bool TakeBet()
        {
            int ymax = Console.WindowHeight - 1;
            int xmax = Console.WindowWidth - 2;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(xmax / 2 + 1, 5);

            Console.Write("ВАША СТАВКА: ");
            Console.ForegroundColor = ConsoleColor.Yellow;

            int playerBet = Convert.ToInt32(Console.ReadLine());
            if (playerBet > 0 && playerBet <= PersonChek.money)
            {
                player1.CurrentBet = playerBet;
                PersonChek.money -= player1.CurrentBet;
                player1.CurrentCashPrize = player1.CurrentBet * player1.BugFavourite.Odds;
                return true;
            }
            else if (playerBet >= PersonChek.money)
            {
                Console.WriteLine("У тебя недостаточно средств для такой ставки!");
                Console.WriteLine("Вся оставшаяся сумма пойдет на ставку!");
                player1.CurrentBet = (int)PersonChek.money;
                PersonChek.money -= player1.CurrentBet;
                Console.ReadKey();
                return true;
            }
            return false;
        }


        /// <summary>
        /// Запускает забег жуков
        /// </summary>
        /// <param name="bugs">Список жуков</param>
        public static void RunRace(List<Bug> bugs)
        {
            Random rnd = new Random();
            int ymax = Console.WindowHeight - 1;
            int xmax = Console.WindowWidth - 2;
            Console.SetCursorPosition(xmax / 2 + 1, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ВАШИ СТАВКИ ПРИНЯТЫ!");
            Console.SetCursorPosition(xmax / 2 + 1, 3);
            Console.Write("НАЖМИТЕ НА ЛЮБУЮ КЛАВИЩУ ДЛЯ НАЧАЛА СКАЧЕК");
            Console.ReadKey();
            Console.Clear();
            MainPrintInfo();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xmax / 2 + 1, 1);
            Console.WriteLine("-------------------------");
            Console.SetCursorPosition(xmax / 2 + 4, 2);
            Console.WriteLine("ИТОГИ СКАЧЕК");
            Console.SetCursorPosition(xmax / 2 + 1, 3);
            Console.WriteLine("-------------------------");
            var shuffledBugs = bugs.OrderBy(x => rnd.Next()).ToList();
            for (int i = 0; i < shuffledBugs.Count; i++)
            {
                ResetColor();
                shuffledBugs[i].Place = i + 1;
                Thread.Sleep(1200);
                Console.SetCursorPosition(xmax / 2 + 1, 4 + i);
                Console.ForegroundColor = shuffledBugs[i].BugColor;
                Console.Write($"{shuffledBugs[i].Place}. ЖУК:{shuffledBugs[i].Name}");

            }

            for (int i = 0; i < shuffledBugs.Count; i++)
            {
                if (shuffledBugs[i].Place == 1)
                {
                    if (player1.BugFavourite.Place == shuffledBugs[i].Place)
                    {
                        player1.IsWinner = true;
                    }
                    else
                    {
                        player1.IsWinner = false;
                    }
                    Console.SetCursorPosition(0, 4 + bugNumbers + 1);
                    ResetColor();
                    Console.WriteLine($"ПОБЕДИТЕЛЕМ ДАННОГО ЗАБЕГА СТАЛ ЖУК ПО ИМЕНИ {shuffledBugs[i].Name} С ШАНСАМИ НА ПОБЕДУ: {shuffledBugs[i].Odds}/1");
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Выводит результат забега
        /// </summary>
        public static void EndRound()
        {
            if (player1.IsWinner)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ПОЗДРАВЛЯЕМ!!! ВАШ ФАВОРИТ ПРИХОДИТ ПЕРВЫМ.");
                Console.WriteLine($"{player1.Name}, ВЫ ВЫИГРАЛИ {player1.CurrentCashPrize}$");
                PersonChek.money += player1.CurrentCashPrize;
                player1.CurrentBet = 0;
                player1.CurrentCashPrize = 0;
                mainMenu.Exit();
            }
            else if (!player1.IsWinner)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{player1.Name}, ВЫ ПРОИГРАЛИ. ВАМ ТОЧНО ПОВЕЗЕТ В СЛЕДУЮЩИЙ РАЗ!");
                player1.CurrentBet = 0;
                player1.CurrentCashPrize = 0;
                mainMenu.Exit();
            }

            if (PersonChek.money >= 0)
            {
                mainMenu.ZeroBalance();
            }
            Start();
        }
    }
}
