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
			Card _card = new Card();
			Deck deck = new Deck();
			Player player = new Player();
			List<Card> cards = new List<Card>();
			

			Console.WriteLine("Чтобы взять карту нажмите пробел, " +
			"нажмите любую другую клавишу, чтобы перестать доставать " +
			"карты из колоды и увидеть все карты в руке");

			Console.WriteLine("Карты в колоде после расклада:");

			deck.Shuffle();
			deck.Show();

			while (Console.ReadKey().Key == ConsoleKey.Spacebar)
			{
				_card = player.TakeCard();
				player.AddCardInHand(_card);
				deck.RemoveTakenCard(_card);
				Console.WriteLine("Карты в руке у Игрока");
				player.ShowCards();
				Console.WriteLine("Карты в колоде");
				deck.Show();
			}
		}

		public class Deck
		{
			private string[] _suits;
			private string[] _values;
			private List<Card> _cards = new List<Card>();
			private Card _card = new Card();


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

			public void Shuffle()
			{
				Random random = new Random();

				for (int n = _cards.Count - 1; n > 0; --n)
				{
					int k = random.Next(n+1);                           //NAMES!!!!!!
					Card temp = _cards[n];
					_cards[n] = _cards[k];
					_cards[k] = temp;
				}
			}

			public void Show()
			{
				foreach(Card card in _cards)
				{
					card.Show();
				}	
			}

			public List<Card> GetCards()
			{
				return _cards;
			}

			public void RemoveTakenCard(Card _card)
			{
				for(int i = 0; i < _cards.Count; i++)
				{
					if (_cards[i].Value==_card.Value & _cards[i].Suit==_card.Suit)
					{
						_cards.Remove(_cards[i]);
					}
				}
				 
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
			private Card _card = new Card();
			Deck deck = new Deck();
			List<Card> _cards = new List<Card>();
			List<Card> _cardsInHand = new List<Card>();

			
			public Card TakeCard()
			{
				_card = _cards[_cards.Count-1];
				return _card;
			}

			public void AddCardInHand(Card _card)
			{
				_cardsInHand.Add(_card);
			}

			public void ShowCards()
			{

				foreach (var card in _cardsInHand)
				{
					card.Show();
				}
				Console.WriteLine($"V ruke{_cardsInHand.Count} kart" +
					$"V kolode {_cards.Count}");
			}
		}
	}
}