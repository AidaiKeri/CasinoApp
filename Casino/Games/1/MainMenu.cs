using Games;
using System;

namespace Games
{
    public class MainMenu
    {
        ConsoleKeyInfo key;
        public void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n Для выхода в главное меню нажмите Q \n Для продолжения нажмите Enter");
            key = Console.ReadKey();
            if (key.Key == ConsoleKey.Q)
            {
                Program.Main();
            }
        }
        public void ZeroBalance()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (PersonChek.money <= 0)
            {
                Console.WriteLine("Вам не хватает денег! \n Для закрытия игры нажмите Enter");
                Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Environment.Exit(0);
                }
            }           
        }
    }
}
