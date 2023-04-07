using System;

namespace Games.BugRacing
{

    public class Player : PersonChek
    {
        public string Name { get; set; }
        public int CurrentBet { get; set; }
        public int CurrentCashPrize { get; set; }
        public Bug BugFavourite { get; set; }
        public ConsoleColor ColorOfBug { get; set; }
        public bool IsWinner { get; set; }


        public Player()
        {
            Name = "Player";
        }

        public Player(string name)
        {
            Name = name;
        }


        /// <summary>
        /// Метод для получения имени игрока
        /// </summary>
        /// <returns>Возвращает имя игрока</returns>
        private string GetName()
        {

            string name;
            Console.SetCursorPosition(50, 5);
            Console.Write("Введите ваше имя: ");
            name = Console.ReadLine().ToUpper();
            Console.Clear();
            return name;
        }

        /// <summary>
        /// Вывод информация игрока на консоль
        /// </summary>
        public void PrintInfo(bool isBugChoosen)
        {
            int ymax = Console.WindowHeight - 1;
            int xmax = Console.WindowWidth - 2;
            if (isBugChoosen)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("-----------------------");
                Console.SetCursorPosition(1, 2);
                Console.WriteLine("ИНФОРМАЦИЯ ОБ ИГРОКЕ");
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("-----------------------");

                Race.ResetColor();
                Console.Write("Имя: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(Name);

                Race.ResetColor();
                Console.Write("Деньги: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(money + "$");

                Race.ResetColor();
                Console.Write("Ставка: ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(CurrentBet + "$");

                Race.ResetColor();
                Console.Write($"Возможный выигрыш: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(CurrentCashPrize + "$");

                Race.ResetColor();
                Console.Write("Жук-Фаворит: ");
                Console.ForegroundColor = ColorOfBug;
                Console.WriteLine(BugFavourite.Name);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("-----------------------");
                Console.SetCursorPosition(1, 2);
                Console.WriteLine("ИНФОРМАЦИЯ ОБ ИГРОКЕ");
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("-----------------------");

                Race.ResetColor();
                Console.Write("Имя: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(Name);

                Race.ResetColor();
                Console.Write("Деньги: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(money + "$");
            }
        }
    }
}