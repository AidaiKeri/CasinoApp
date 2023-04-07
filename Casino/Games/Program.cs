using Games;
using Games.BugRacing;
using System;
using System.Text;

namespace Games
{
    public class Program
    {       
        public static void Main()
        {
            MainMenu mainMenu = new MainMenu();
            Casino casino = new Casino();
            Spinner spinner = new Spinner();
            BlackJack blackJack = new BlackJack();
            Bug bugRacing = new Bug();
            Bakkara bakkara = new Bakkara();

            Menu();

            bool Menu()
            {              
                mainMenu.ZeroBalance();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВаш баланс = " + PersonChek.money + " Сом");
                Console.WriteLine("Выберите игру:");
                Console.WriteLine("1) Рулетка");
                Console.WriteLine("2) Спинер");
                Console.WriteLine("3) Блэкджек");
                Console.WriteLine("4) Гонки жуков");
                Console.WriteLine("5) Баккара");

                Console.Write("\r\n Выберите вариант: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Добро пожаловать в игру Рулетка!");
                        casino.Start();                        
                        return true;
                    case "2":
                        Console.WriteLine("Добро пожаловать в игру Спинер!");
                        spinner.Start();
                        return true;
                    case "3":                        
                        Console.WriteLine("Добро пожаловать в игру Блэкджек!");
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\t\t♠♥♣♦");
                        blackJack.Start();
                        return true;
                    case "4":
                        Console.WriteLine("Добро пожаловать в игру Гонки жуков!");
                        mainMenu.Exit();
                        Race.Start();
                        return true;
                    case "5":
                        Console.WriteLine("Добро пожаловать в игру Баккара!");
                        bakkara.Start();
                        return true;
                    default:
                        Console.WriteLine("Неверная команда\n");
                        Console.Clear();
                        return Menu();
                }
            }
        }
    }
}
