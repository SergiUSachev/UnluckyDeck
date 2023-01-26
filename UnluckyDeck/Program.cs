using System;
using System.Reflection;
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
			"нажмите любую другую клавишу, чтобы перестать доставать карты из колоды и увидеть все карты в руке");

			foreach (var item in deck.GetShuffledCards())
			{
				item.Show();
			}
			//Console.WriteLine(deck.GetShuffledCards());
			//while (Console.ReadKey().Key == ConsoleKey.Spacebar)
			//{
			//	hand.Fill();
			//}

			//Console.WriteLine($"Карты в вашей руке:");
			//hand.ShowCards();

			//Console.ReadKey();
		}

		public class Deck
		{
			private string[] _suits;
			private string[] _values;
			private List<Card> _cards = new List<Card>();

			public Deck()
			{
				_suits = new string[] { "Черви", "Пики", "Крести", "Буби" };
				_values = new string[] { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

				for (int i = 0; i < _values.Length; i++)
				{
					for (int j = 0; j < _suits.Length; j++)
					{
						_cards.Add(new Card(_values[i], _suits[j]));
					}
				}
			}

			public List<Card> Shuffle()
			{
				Random random = new Random();

				for (int n = _cards.Count - 1; n > 0; --n)
				{
					int k = random.Next(n+1);                           //NAMES!!!!!!
					Card temp = _cards[n];
					_cards[n] = _cards[k];
					_cards[k] = temp;
				}

				return _cards;
			}
			public void Show()
			{
				foreach(Card card in _cards)
				{
					card.Show();
				}	
			}

			public List<Card> GetShuffledCards()
			{
				_cards = Shuffle();
				return _cards;
			}
		}

		public class Card
		{
			public string Suit { get; private set; }

			public string Value { get; private set; }

			public Card(string suit, string value)
			{
				Suit=suit;
				Value=value;
			}

			public Card()
			{
			}

			public void Show()
			{
				Console.WriteLine($"Достоинство карты: {Value}, Масть карты: {Suit}");
			}
		}

		public class Player
		{

		}

		public class Hand : Player
		{
			Deck deck = new Deck();
			List<Card> _cards = new List<Card>();

			public void Fill()
			{

			}

			public void ShowCards()
			{
				foreach (var card in _cards)
				{
					card.Show();
				}
				Console.WriteLine($"V ruke{_cards.Count} kart");
			}

		}
	}
}