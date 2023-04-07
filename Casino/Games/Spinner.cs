using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games;

namespace Games
{
    class Spinner
    {
        public int K { get; set; }
        private ConsoleKeyInfo key;
        MainMenu mainMenu = new MainMenu();
        public void Start()
        {
            mainMenu.ZeroBalance();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nВаш баланс = " + PersonChek.money + " Сом");
            Console.WriteLine("Сделать ставку: ");
            K = Int32.Parse(Console.ReadLine());
            Play();
        }
        public void Play()
        {
            var num = new int[8];
            var rnd = new Random();
            while (true)
            {
                mainMenu.Exit();
                ConsoleKeyInfo Key = Console.ReadKey();
                if (Key.Key == ConsoleKey.Enter)
                {                    
                    PersonChek.money -= K;
                    Console.WriteLine($"Ваш текуший баланс {PersonChek.money}с"); Console.WriteLine($"Ваша ставка {K}c");
                    var a = rnd.Next(1, num.Length);
                    var b = rnd.Next(1, num.Length);
                    var c = rnd.Next(1, num.Length);
                    Console.WriteLine($"Результат: {a} {b} {c}");

                    if (a == b && b == c && c == a)
                    {
                        Wins1();
                    }
                    else if (a == b || c == b)
                    {
                        Wins2();
                    }
                    else if (a == c)
                    {
                        FreeSpin();
                    }
                    else
                    {
                        Console.WriteLine("Попробуйте еще раз");

                    }

                }

            }
        }
        public void Wins1()
        {
            var num = new int[8];
            var rnd = new Random();
            int x = K * 3;
            Console.WriteLine($"Ваш выигрыш: {x}с");
            Console.WriteLine("Кнопка Double (Нажми Enter)\nЗабрать выигрыш (Нажми Пробел)");
            ConsoleKeyInfo key3 = Console.ReadKey();
            if (key3.Key == ConsoleKey.Enter)
            {

                var a = rnd.Next(1, num.Length);
                var b = rnd.Next(1, num.Length);
                var c = rnd.Next(1, num.Length);
                Console.WriteLine($"Результат: {a} {b} {c}");
                if (a == b && b == c && c == a)
                {
                    x = x * 3;
                    Console.WriteLine($"Вы выиграли {x}c");
                    PersonChek.money += x;
                    Play();
                }
                else if (a == b || b == c)
                {
                    x = x * 2;
                    Console.WriteLine($"Вы выиграли {x}c");
                    PersonChek.money += x;
                    Play();
                }
                else if (a == c)
                {
                    FreeSpin();
                }
                else
                {

                    PersonChek.money -= K;
                    Console.WriteLine($"Ваш баланс: {PersonChek.money}с");
                    Console.WriteLine("Попробуй еще раз");
                    Play();
                }

            }
            else if (key3.Key == ConsoleKey.Spacebar)
            {
                PersonChek.money += x;
                Console.WriteLine($"Ваш баланс: {PersonChek.money}с");
                Play();
            }
        }

        public void Wins2()
        {
            var num = new int[8];
            var rnd = new Random();
            var w = K * 2;
            Console.WriteLine($"Ваш выиграли: {w}с");
            Console.WriteLine("Кнопка Double (Нажми Enter)\nЗабрать выиграш (Нажми Пробел)");
            ConsoleKeyInfo key1 = Console.ReadKey();
            if (key1.Key == ConsoleKey.Enter)
            {

                var a = rnd.Next(1, num.Length);
                var b = rnd.Next(1, num.Length);
                var c = rnd.Next(1, num.Length);
                Console.WriteLine($"Результат: {a} {b} {c}");
                if (a == b && b == c && c == a)
                {
                    w = w * 3;
                    Console.WriteLine($"Вы выиграли {w}c");
                    PersonChek.money += w;
                    Play();
                }
                else if (a == b || b == c)
                {
                    w = w * 2;
                    Console.WriteLine($"Вы выиграли {w}c");
                    PersonChek.money += w;
                    Play();
                }
                else if (a == c)
                {
                    FreeSpin();
                }
                else
                {

                    PersonChek.money -= K;
                    Console.WriteLine($"Ваш баланс: {PersonChek.money}с");
                    Console.WriteLine("Попробуй еще раз");
                    Play();
                }

            }

            else if (key1.Key == ConsoleKey.Spacebar)
            {
                PersonChek.money += w;
                Console.WriteLine($"Ваш баланс: {PersonChek.money}с");
                Play();
            }
        }

        public void FreeSpin()

        {
            Console.WriteLine("Вы выиграли 1 бесплатный спин");
            var num = new int[8];
            var rnd = new Random();
            Console.WriteLine("Нажмите Enter");
            ConsoleKeyInfo Key = Console.ReadKey();
            if (Key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine($"Ваш текуший баланс {PersonChek.money}с"); Console.WriteLine($"Ваша ставка {K}c");
                var a = rnd.Next(1, num.Length);
                var b = rnd.Next(1, num.Length);
                var c = rnd.Next(1, num.Length);
                Console.WriteLine($"Результат: {a} {b} {c}");
                if (a == c)
                {
                    FreeSpin();
                }
                if (a == b && b == c && c == a)
                {
                    Wins1();
                }
                else if (a == b || c == b)
                {
                    Wins2();
                }
                else
                {
                    Play();
                }

            }

        }
    }
}
