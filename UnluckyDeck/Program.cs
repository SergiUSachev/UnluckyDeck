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
			Card card;
			Deck deck = new Deck();
			Player player = new Player();

			Console.WriteLine("Чтобы взять карту нажмите пробел, " +
			"нажмите любую другую клавишу, чтобы перестать доставать " +
			"карты из колоды и увидеть все карты в руке");

			player.Start(deck);

			while (Console.ReadKey().Key == ConsoleKey.Spacebar & deck.GetCards().Count()>0)
			{
				card = deck.GiveCard();
				player.TakeCard(card);
				card.Show();
				Console.ReadKey();
				Console.Clear();
			}

			Console.WriteLine("Игра завершена, ваши карты:");
			player.ShowCards();
		}

		public class Deck
		{
			private Card _card = new Card();
			private List<Card> _cards = new List<Card>();

			public Deck()
			{
				MakeDeck();
			}

			public void Shuffle()
			{
				Random random = new Random();

				for (int i = _cards.Count - 1; i >= 0; i--)
				{
					int randomCardNumber = random.Next(0, i + 1);
					Card shuffledCard = _cards[i];
					_cards[i] = _cards[randomCardNumber];
					_cards[randomCardNumber] = shuffledCard;
				}
			}

			public void Show()
			{
				foreach (Card card in _cards)
				{
					card.Show();
				}
			}

			public Card GiveCard()
			{
				if (_cards.Count == 0)
				{
					Console.WriteLine("Карты в колоде закончились!" +
						"\nКарты в руке:");

					return null;
				}
				else
				{
					int lastCardNumber = _cards.Count-1;
					_card = _cards[lastCardNumber];
					RemoveTakenCard(lastCardNumber);
					return _card;
				}
			}

			public List<Card> GetCards()
			{
				return _cards;
			}

			private void MakeDeck()
			{
				string[] _suits = new string[] { "Черви", "Пики", "Крести", "Буби" };
				string[] _values = new string[] { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

				for (int i = 0; i < _values.Length; i++)
				{
					for (int j = 0; j < _suits.Length; j++)
					{
						_cards.Add(new Card(_values[i], _suits[j]));
					}
				}
			}

			private void RemoveTakenCard(int index)
			{
				if (index >= 0 && index < _cards.Count)
				{
					_cards.RemoveAt(index);
				}
			}
		}

		public class Card
		{
			public Card(string value, string suit)
			{
				Suit=suit;
				Value=value;
			}

			public Card()
			{

			}

			public string Suit { get; private set; }
			public string Value { get; private set; }

			public void Show()
			{
				Console.WriteLine($"Достоинство карты: {Value}, Масть карты: {Suit}");
			}
		}

		public class Player
		{
			private List<Card> _cards = new List<Card>();
			private readonly List<Card> _cardsInHand = new List<Card>();

			public void Start(Deck deck)
			{
				deck.Shuffle();
				_cards = deck.GetCards();
			}

			public void TakeCard(Card card)
			{
				AddCardInHand(card);
			}

			public void ShowCards()
			{
				foreach (var card in _cardsInHand)
				{
					card.Show();
				}

				Console.WriteLine($"В руке {_cardsInHand.Count} карт\n" +
					$"В колоде осталось {_cards.Count}");
			}

			private void AddCardInHand(Card _card)
			{
				_cardsInHand.Add(_card);
			}
		}
	}
}