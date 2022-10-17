using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AnkiCopyBase.Models
{
    public record struct Card(string front, string back, string? hint = null);

    public class Deck : IEnumerable<Card>
    {
        public string Name;
        public List<Card> Cards;

        public Deck()
        {
            Name = "";
            Cards = new List<Card>();
        }

        public Deck(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }

        public static Deck? TryCreateDeck(string? name)
        {
            return Valid.DeckName(name) ? new Deck(name) : null;
        }

        public Deck Shuffle()
        {
            if (Cards == null)
                return this;

            Card[] cards = Cards.ToArray();
            Card temp;
            int n = cards.Length;
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