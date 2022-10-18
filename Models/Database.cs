using System.Text.Json;

namespace AnkiCopyBase.Models
{
    public static class Database
    {
        private const string UserDataFile = @".\users.txt";

        public static bool ExistsInFile(UserData userData, bool fullcheck)
        {
            if (!File.Exists(UserDataFile))
            {
                File.Create(UserDataFile);
                return false;
            }

            foreach (string line in File.ReadLines(UserDataFile))
            {
                if (!line.Contains(';'))
                    continue;

                string[] fileInputs = line.Split(';');

                if (!string.Equals(userData.Name, fileInputs[0]))
                    continue;

                if (!fullcheck)
                    return true;

                if (string.Equals(userData.Password,fileInputs[1]))
                    return true;
            }

            return false;
        }

        public static bool AddUser(UserData userData)
        {
            if (ExistsInFile(userData, false))
                return false;

            File.AppendAllText(UserDataFile, $"{userData.Name};{userData.Password}{Environment.NewLine}");
            Directory.CreateDirectory($@".\{userData.Name}");

            return true;
        }

        public static bool FindUser(UserData userData)
        {
            return ExistsInFile(userData, true);
        }

        public static bool SaveDeck(User user, Deck deck)
        {
            if (File.Exists($@".\{user.Name()}\{deck.Name}.json"))
                return false;

            string json = JsonSerializer.Serialize(deck);
            File.AppendAllText($@".\{user.Name()}\{deck.Name}.json", json);

            return true;
        }

        public static List<Deck> LoadDecks(User user)
        {
            List<Deck> decks = new List<Deck>();

            if (!Directory.Exists($@".\{user.Name()}"))
                throw new Exception();

            foreach (string file in Directory.EnumerateFiles($@".\{user.Name()}", "*.json"))
            {
                Deck deck = new Deck(Path.GetFileNameWithoutExtension(file));

                string json = File.ReadAllText(file);
                List<Card>? cards = JsonSerializer.Deserialize<List<Card>>(json);
                
                if (cards == null)
                    cards = new List<Card>();

                foreach (Card card in cards)
                {
                    deck.AddCard(card);
                }

                decks.Add(deck);
            }

            return decks;
        }
    }
}