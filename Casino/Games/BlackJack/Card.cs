using Games;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace MyBJ.Models
{
    public class Card
    {
        public enum CardSuit
        {
            Clubs,
            Spades,
            Diamonds,
            Hearts
        }

        public enum CardFace
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        public CardSuit Suit { get; }
        public CardFace Face { get; }
        public int Value { get; set; }
        public string SuitName { get; }
        public string FaceName { get; }
        public char Symbol { get; }

        /// <summary>
        /// Конструктор который назначает название на русском, очко и масть карте 
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="face"></param>
        public Card(CardSuit suit, CardFace face)
        {
            Suit = suit;
            Face = face;

            switch (Suit)
            {
                case CardSuit.Diamonds:
                    SuitName = "Буби";
                    Symbol = '♦';
                    break;
                case CardSuit.Spades:
                    SuitName = "Пики";
                    Symbol = '♠';
                    break;
                case CardSuit.Clubs:
                    SuitName = "Крести";
                    Symbol = '♣';
                    break;
                case CardSuit.Hearts:
                    SuitName = "Черви";
                    Symbol = '♥';
                    break;
            }

            switch (Face)
            {
                case CardFace.Ace:
                    Value = 11;
                    FaceName = "Туз";
                    break;
                case CardFace.Two:
                    Value = 2;
                    FaceName = "Два";
                    break;
                case CardFace.Three:
                    Value = 3;
                    FaceName = "Три";
                    break;
                case CardFace.Four:
                    Value = 4;
                    FaceName = "Четыре";
                    break;
                case CardFace.Five:
                    Value = 5;
                    FaceName = "Пять";
                    break;
                case CardFace.Six:
                    Value = 6;
                    FaceName = "Шесть";
                    break;
                case CardFace.Seven:
                    Value = 7;
                    FaceName = "Семь";
                    break;
                case CardFace.Eight:
                    Value = 8;
                    FaceName = "Восемь";
                    break;
                case CardFace.Nine:
                    Value = 9;
                    FaceName = "Девять";
                    break;
                case CardFace.Ten:
                    Value = 10;
                    FaceName = "Десять";
                    break;
                case CardFace.Jack:
                    Value = 10;
                    FaceName = "Валет";
                    break;
                case CardFace.Queen:
                    Value = 10;
                    FaceName = "Дама";
                    break;
                case CardFace.King:
                    Value = 10;
                    FaceName = "Король";
                    break;
            }
        }


        /// <summary>
        /// Вывод на консоль информации о карте, в том числе вид Туза (малый и высший)
        /// </summary>
        public void PrintCardName()
        {
            if (Suit == CardSuit.Diamonds || Suit == CardSuit.Hearts)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }

            if (Face == CardFace.Ace)
            {
                if (Value == 11)
                {
                    Console.WriteLine($"Высший {FaceName}  {SuitName} \"{Symbol}\"");
                }
                else
                {
                    Console.WriteLine($"Малый {FaceName}  {SuitName} \"{Symbol}\"");
                }
            }
            else
            {
                Console.WriteLine($"{FaceName}  {SuitName} \"{Symbol}\"");
            }
            BlackJack.ResetColor();
        }
    }
}