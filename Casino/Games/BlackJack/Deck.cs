using System;
using System.Collections.Generic;

namespace MyBJ.Models
{
    public class Deck
    {
        private List<Card> DeckCards = new List<Card>();

        /// <summary>
        /// Конструктор создания колоды
        /// </summary>
        public Deck()
        {
            CreateDeck();
        }


        /// <summary>
        /// Меняет колоду на отсортированную колоду и тасует ее
        /// </summary>
        public void CreateDeck()
        {
            DeckCards = GetCleanDeck();
            Shuffle();
        }


        /// <summary>
        /// Возвращает колоду отсортированную по мастям и очкам.
        /// </summary>
        /// <returns>Возвращает новую колоду</returns>
        public List<Card> GetCleanDeck()
        {
            List<Card> cleanDeck = new List<Card>();
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cleanDeck.Add(new Card((Card.CardSuit)j, (Card.CardFace)i));
                }
            }
            return cleanDeck;
        }

        /// <summary>
        /// Тасует колоду карт
        /// </summary>
        public void Shuffle()
        {
            Random rnd = new Random();
            int n = DeckCards.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n - 1);
                Card temp = DeckCards[k];
                DeckCards[k] = DeckCards[n];
                DeckCards[n] = temp;

            }
        }

        /// <summary>
        /// Раздача карт. Убирает две верхние карты с колоды 
        /// </summary>
        /// <returns>Возвращает список из двух карт </returns>
        public List<Card> DealHand()
        {
            List<Card> hand = new List<Card>();
            hand.Add(DeckCards[0]);
            hand.Add(DeckCards[1]);
            DeckCards.RemoveRange(0, 2); //Убирает с колоды разданные карты на руки
            return hand;
        }

        /// <summary>
        /// Убирает одну верхнюю карты с колоды и раздает на руку
        /// </summary>
        /// <returns>Возвращает одну карту с колоды</returns>
        public Card TakeExtraCard()
        {
            Card card = DeckCards[0];
            DeckCards.Remove(card);
            return card;
        }
    }
}