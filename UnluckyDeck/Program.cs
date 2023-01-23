using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using static UnluckyDeck.Program;
//Есть колода с картами. Игрок достает карты, пока не решит, что 
//ему хватит карт (может быть как выбор пользователя, так и 
//количество сколько карт надо взять). После выводиться 
//вся информация о вытянутых картах. Возможные классы: Карта, Колода, Игрок.

namespace UnluckyDeck
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Deck deck = new Deck();

			Player player = new Player();
			Hand hand = new Hand();
			Card card = new Card();

			Console.WriteLine("Чтобы взять карту нажмите пробел, " +
			"нажмите любую другую клавишу, чтобы перестать доставать карты из колоды");

			while (Console.ReadKey().Key == ConsoleKey.Spacebar)
			{
				hand.Fill();
			}

			hand.ShowCards();
		}

		public class Deck
		{
			public List<string> _suits { get; private set; } = new List<string>() { "Черви", "Пики", "Крести", "Буби" };
			public List<string> _values { get; private set; } = new List<string>() { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
		}

		public class Card
		{
			public string _suit;

			public string _value;

			Card card;
			public void Show()
			{
				Console.WriteLine($"Достоинство карты: {_value}, Масть карты: {_suit}");
			}
		}

		public class Player
		{
			public Card TakeCard(Deck deck)
			{
				Card card = new Card();

				Random random = new Random();

				int randomNumberValues = random.Next(0, deck._values.Count-1);
				int randomNumberSuits = random.Next(0, deck._suits.Count-1);

				card._value = deck._values[randomNumberValues];
				card._suit = deck._suits[randomNumberSuits];

				card.Show();
				return card;
			}
		}

		public class Hand : Player
		{
			List<Card> _cards = new List<Card>();
			Deck deck = new Deck();

			private Card GetNotDuplicatedCard(Card card) // КАРТЫ ВСЁ РАВНО БЕРУТСЯ ДАЛЬШЕ НАДО СДЕЛАТЬ ОТДЕЛЬНЫЙ МЕТОД НА ВЗЯТЬ КАРТУ И НА ПОЛОЖИТЬ КАРТУ В РУКУ
			{
				bool isDuplicated = true;

				if(_cards.Count == 0)
				{
					isDuplicated = false;
				}

				while (isDuplicated == true)
				{
					foreach (Card cardInHand in _cards)
					{
						if (cardInHand._value == card._value & cardInHand._suit == card._suit)
						{
							card = TakeCard(deck);
							Console.WriteLine("ДУБЛЬ");
							break;
						}
						else
						{
							isDuplicated = false;
						}
					}
				}

				return card;
			}

			public void Fill()
			{
				Card _card = TakeCard(deck);
				_cards.Add(GetNotDuplicatedCard(_card));
			}

			public void ShowCards()
			{
				foreach (var card in _cards)
				{
					card.Show();
				}
			}

		}
	}
}