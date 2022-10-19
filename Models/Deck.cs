using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AnkiCopyBase.Models
{
    public class Deck : IEnumerable<Card>
    {
        public readonly string Name;
        private List<Card> Cards;

        public Deck(string name, List<Card>? cards = null)
        {
            Name = name;

            if (cards == null)
                Cards = new List<Card>();
            else
                Cards = cards;
        }

        public void AddCard(Card card) =>
            Cards.Add(card);

        public static Deck? TryCreate(string? name)
        {
            return Valid.DeckName(name) ? new Deck(name) : null;
        }

        public Deck Shuffle()
        {
            if (Cards == null)
                return this;

            Card[] cards = Cards.ToArray();
            int n = cards.Length;
            Card temp;
            Random rng = new Random();

            while (n > 1)
            {
                int k = rng.Next(n--);
                temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }

            Cards = cards.ToList();

            return this;
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return ((IEnumerable<Card>)Cards).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Cards).GetEnumerator();
        }
    }
}