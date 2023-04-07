using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Games.BugRacing
{
    public class Bug
    {
        public Bug()
        {

        }

        public Bug(int number, string name, ConsoleColor bugColor, int odds)
        {
            Number = number;
            Name = name;
            BugColor = bugColor;
            Odds = odds;
        }

        public int Number { get; set; }
        public string Name { get; set; }
        public ConsoleColor BugColor { get; set; }
        public int Odds { get; set; }
        public int Place { get; set; }

        static Random rnd = new Random();

        /// <summary>
        /// Тасует список имен для жука
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Возвращает тасованный список имен</returns>
        public static List<string> ShuffleBugName(List<string> str)
        {
            var shuffledNames = str.OrderBy(x => rnd.Next()).ToList();
            return shuffledNames;
        }

        /// <summary>
        /// Тасует шансы жука на забег
        /// </summary>
        /// <param name="num"></param>
        /// <returns>Возваращает тасованный список шансов на забег жука</returns>
        public static List<int> ShuffleBugOdd(List<int> num)
        {
            var shuffledOdds = num.OrderBy(x => rnd.Next()).ToList();
            return shuffledOdds;
        }


        /// <summary>
        /// Выводит на консоль список жуков и их информацию
        /// </summary>
        /// <param name="bugs">Список жуков</param>
        public static void PrintBugScreen(List<Bug> bugs)
        {
            int ymax = Console.WindowHeight - 1;
            int xmax = Console.WindowWidth - 2;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(30, 1);
            Console.WriteLine("---------------------------------");
            Console.SetCursorPosition(36, 2);
            Console.WriteLine("ШАНСЫ НА ЗАБЕГИ ЖУКОВ");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("---------------------------------");

            for (int i = 0; i < bugs.Count; i++)
            {
                Console.SetCursorPosition(30, 4 + i);
                Console.ForegroundColor = bugs[i].BugColor;
                Console.Write($"{bugs[i].Number}. Имя:{bugs[i].Name}");
                Console.SetCursorPosition(52, 4 + i);
                Console.WriteLine($"Шансы: {bugs[i].Odds}:1");
            }
        }

        /// <summary>
        /// Список консольных цветов для жука
        /// </summary>
        public static List<ConsoleColor> Colors = new List<ConsoleColor>()
        {
            ConsoleColor.Blue,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Magenta,
            ConsoleColor.Red,
            ConsoleColor.DarkYellow,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkGray,
            ConsoleColor.Cyan
        };

        /// <summary>
        /// Список имен для жука
        /// </summary>
        public static List<string> Names = new List<string>()
        {
            "Cheetah",
            "Tiger",
            "Dragon Slayer",
            "Pandaren",
            "Jinx",
            "Nevermore",
            "Beast",
            "Aniki",
            "Bugman"
        };

        /// <summary>
        /// Список шансов жука на забег
        /// </summary>
        public static List<int> BugOdds = new List<int>() { 2, 3, 5, 6, 7, 8, 9, 4, 5 };
















    }
}