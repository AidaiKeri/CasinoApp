using System;
using System.Collections.Generic;
using Games;
using Stripe;

namespace MyBJ.Models
{
    public class Dealer
    {
        public List<Card> HiddenCard { get; set; } = new List<Card>();
        public List<Card> RevealedCards { get; set; } = new List<Card>();

        /// <summary>
        /// Берет самую первую скрытую карты дилера и раскрывает ее.
        /// Расрываемая карта удаляется из списка скрытых карт дилера.
        /// </summary>
        public void ReavelCard()
        {
            RevealedCards.Add(HiddenCard[0]);
            HiddenCard.Remove(HiddenCard[0]);
        }

        /// <summary>
        /// Рассчитывает очко раскрытой карты на руках дилера
        /// </summary>
        /// <returns></returns>
        public int GetHandValue()
        {
            int value = 0;
            foreach (Card card in RevealedCards)
            {
                value += card.Value;
            }

            return value;
        }


        /// <summary>
        /// Выводит на консоль информацию раскрытых карт дилера
        /// </summary>
        public void PrintInfo()
        {
            Console.WriteLine($"Очки Дилера: \"{GetHandValue()}\"");
            foreach (Card card in RevealedCards)
            {
                card.PrintCardName();
            }
            for (int i = 0; i < HiddenCard.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("<Скрытая карта>");
                BlackJack.ResetColor();
            }
            Console.WriteLine();
        }
    }
}