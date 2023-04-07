using Games;
using System;
using System.Collections.Generic;

namespace MyBJ.Models
{
    public class Player
    {
        public int Chips { get; set; } = 300;
        public double Bet { get; set; }
        public int Wins { get; set; }
        public int RoundsPlayed { get; set; } = 1;
        public List<Card> Hand { get; set; } = new List<Card>();


        /// <summary>
        /// Делает ставку из фишек и убирает равное количество сделанной ставки (фишки) из фишек игрока
        /// </summary>
        /// <param name="num"> Количество фишек для ставки</param>
        public void MakeBet(double num)
        {
            Bet += num;
            PersonChek.money -= num;
        }

        /// <summary>
        /// Ставка равна нулю
        /// </summary>
        public void ClearBet()
        {
            Bet = 0;
        }

        /// <summary>
        /// Отмняет сделанную ставку игроком. Используется когда ничья. 
        /// </summary>
        public void ReturnBet()
        {
            PersonChek.money += Bet;
            ClearBet();
        }

        /// <summary>
        /// Выигрыш игрока от сделанной ставки.
        /// </summary>
        /// <param name="blackjack">Если у игрока блекджек, выигрыш составляет 150% от сделанной ставки. </param>
        /// <returns></returns>
        public double WinBet(bool blackjack)
        {
            double chipsWon = 0;
            if (blackjack == true)
            {
                chipsWon = (int)Math.Floor(Bet * 1.5);
            }
            else
            {
                chipsWon = Bet * 2;
            }
            PersonChek.money += chipsWon;
            ClearBet();
            return chipsWon;
        }

        /// <summary>
        /// Рассчитывает очки карт на руках
        /// </summary>
        public int GetHandValue()
        {
            int value = 0;
            foreach (var card in Hand)
            {
                value += card.Value;
            }

            return value;
        }

        /// <summary>
        /// Выводит на консоль карты игрока и статистику игрока
        /// </summary>
        public void PrintInfo()
        {
            Console.Write("Ставка: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(Bet + "  ");
            BlackJack.ResetColor();
            Console.Write("Фишки: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(PersonChek.money + "  ");
            BlackJack.ResetColor();
            Console.Write("Выигрыш: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(Wins);
            BlackJack.ResetColor();
            Console.WriteLine($"Раунд №: {RoundsPlayed}");
            Console.WriteLine();
            Console.WriteLine($"Ваши очки: \"{GetHandValue()}\"");
            foreach (Card card in Hand)
            {
                card.PrintCardName();
            }
            Console.WriteLine();
        }
    }
}