using Games;
using System;

namespace Games
{
    class Casino
    {
        MainMenu mainMenu = new MainMenu();
        Random random = new Random();       

        enum Colors : byte
        {
            NotDefined,
            Red,
            Black,
        }
        Colors colorStake;
        Colors colorRolled;
        bool launch;
        double amountStake;
        int numberStake;
        void NextMove()
        {
            launch = false;
            amountStake = 0;
            numberStake = -1;
            colorStake = Colors.NotDefined;

            int numberRolled = random.Next(0, 37);
            int[] redNumbers = new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            int result = -1;
            foreach (var item in redNumbers)
            {
                if (item == numberRolled)
                {
                    result = item;
                }
            }
            if (numberRolled == 0)
            {
                colorRolled = Colors.NotDefined;
            }
            else if (result == numberRolled)
            {
                colorRolled = Colors.Red;
            }
            else
            {
                colorRolled = Colors.Black;
            }            
            
            while (!launch)
            {
                mainMenu.Exit();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n СДЕЛАЙТЕ свою ставку ... пожалуйста");                
                string? stake = Console.ReadLine();
                if (int.TryParse(stake, out _))
                    amountStake = Convert.ToInt32(stake);

                if (amountStake <= 0 || amountStake > PersonChek.money)
                {
                    Console.WriteLine("Введите сумму в пределах вашего бюджета.");
                    continue;
                }

                while (true)
                {
                    Console.WriteLine("Выберите на что поставить? \n");
                    Console.WriteLine("1) На цвет");
                    Console.WriteLine("2) На число");
                    Console.Write("\r\n Выберите вариант: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            ColorMenu();//Выбор цвета
                            break;
                        case "2":
                            Stake();
                            break;
                        default:
                            Console.WriteLine("Неверная команда \n");
                            continue;
                    }
                    break;
                }
                launch = true;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"На рулетке выпало число {numberRolled} {colorRolled} || Вы поставили на ({numberStake} {colorStake})");

            if (numberStake == numberRolled || colorStake == colorRolled)
            {
                amountStake = numberStake == numberRolled ? amountStake * 35 : amountStake;//если выпало выбранное число то умножаем на 36, если цвет то умножаем на 2
                PersonChek.money += amountStake;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Поздравляем! ВЫ ВЫИГРАЛИ {amountStake} !!! На вашем счету {PersonChek.money}");
            }
            else
            {
                PersonChek.money -= amountStake;
                if (PersonChek.money != 0)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ИЗВИНИТЕ! Вы проиграли. На вашем счету осталось {PersonChek.money}, чтобы продолжить");
            }
            Console.WriteLine($"У вас есть {PersonChek.money}. Продолжайте играть!!!");
        }
        bool Stake()
        {
            Console.WriteLine("Поставьте на число. Выберите от 0 до 36.");
            string? number = Console.ReadLine();
            if (int.TryParse(number, out _))
                numberStake = Convert.ToInt32(number);

            if (numberStake < 0 || numberStake > 36)//если меньше 0 или больше 36 то вводим заново 
            {
                Console.WriteLine("Вы не верно ввели! \n");
                return Stake();                
            }
            return true;
        }
        bool ColorMenu()//Выбор цвета
        {           
            Console.WriteLine("\n Выберите цвет: ");
            Console.WriteLine("1) Красный");
            Console.WriteLine("2) Черный");
            Console.Write("\r\n Выберите вариант: ");
            switch (Console.ReadLine())
            {
                case "1":
                    colorStake = Colors.Red;
                    return true;
                case "2":
                    colorStake = Colors.Black;
                    return true;
                default:
                    Console.WriteLine("Неверная команда\n");
                    return ColorMenu();
            }
        }       
        public void Start()
        {
            while (PersonChek.money > 0)//если счет не пуст
            {
                NextMove();
            }
            mainMenu.ZeroBalance();
        }
    }
}
