using System.Collections;

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

        public void AddCard(Card card)
        {
            if(Cards != null)
                Cards.Add(card);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return ((IEnumerable<Card>)Cards).GetEnumerator();
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Cards).GetEnumerator();
        }
    }
}