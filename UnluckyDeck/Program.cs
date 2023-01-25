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

			deck.Show();
			//Console.WriteLine("Чтобы взять карту нажмите пробел, " +
			//"нажмите любую другую клавишу, чтобы перестать доставать карты из колоды и увидеть все карты в руке");

			//while (Console.ReadKey().Key == ConsoleKey.Spacebar)
			//{
			//	hand.Fill();
			//}

			//Console.WriteLine($"Карты в вашей руке:");
			//hand.ShowCards();

			Console.ReadKey();
		}

		public class Deck
		{
			public List<string> _suits { get; private set; } = new List<string>() { "Черви", "Пики", "Крести", "Буби" };
			public List<string> _values { get; private set; } = new List<string>() { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
			public List<string[]> _cards { get; private set; } = new List<string[]>();

			private string[] _card = new string[2];

			private void GetNew()
			{
				for(int i = 0; i < _values.Count; i++)
				{

					_card[0] = _values[i];
					for (int j = 0; j < _suits.Count; j++)
					{
						_card[1] = _suits[j];
						_cards.Add(_card);
						//Console.WriteLine($"{_card[0]}:{_card[1]}");
						//Console.WriteLine($"{_cards[i][0]}");
						//Console.WriteLine($"{_cards[i][1]}");
					}
				}
			}

			public void Show()
			{
				GetNew();
				for (int i = 0; i < _cards.Count; i++)
				{
					for (int j = 0; j < _card.Length; j++)
					{
						Console.WriteLine($"{_cards[i][j]}");
					}
				}
			}
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


				card.Show();
				return card;
			}
		}

		public class Hand : Player
		{
			List<Card> _cards = new List<Card>();
			Deck deck = new Deck();

			public void Fill()
			{
				if(_cards.Count > 35)
				{
					Console.WriteLine("В колоде кончились карты");
				}
				else
				{
					Card _card = TakeCard(deck);
					_cards.Add(_card);
				}
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