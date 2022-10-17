using System.Text.Json;

namespace AnkiCopyBase.Models
{
    public record struct UserData(string? Name, string? Password);

    public static class UserManager
    {
        private const string UserDataFile = @".\users.txt";
        private static string? _name = null;

        public static bool ExistsInFile(UserData user)
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

                string[] userData = line.Split(';');

                if (!Valid.Username(userData[0]) ||
                    !Valid.Password(userData[1]))
                    continue;

                if (user.Name == userData[0] || user.Password == userData[1])
                    return true;
            }

            return false;
        }

        public static bool TryRegister(UserData user)
        {
            if (!Valid.User(user))
                return false;

            if (!ExistsInFile(user))
            {
                File.AppendAllText(UserDataFile, $"{user.Name};{user.Password}{Environment.NewLine}");
                Directory.CreateDirectory($@".\{user.Name}");
            }

            _name = user.Name;
            return true;
        }

        public static bool TryLogin(UserData user)
        {
            if (ExistsInFile(user))
            {
                _name = user.Name;
                return true;
            }

            return false;
        }

        public static string? GetName() =>
            _name;

        public static bool SaveDeck(Deck deck)
        {
            if (File.Exists($@".\{_name}\{deck.Name}.txt"))
                return false;

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(deck, options);
            File.AppendAllText($@".\{_name}\{deck.Name}.txt", json);
            return true;
        }

        public static List<Deck> LoadDecks()
        {
            List<Deck> decks = new List<Deck>();

            if (!Directory.Exists($@".\{_name}"))
                throw new Exception();

            foreach (string file in Directory.EnumerateFiles($@".\{_name}", "*.txt"))
            {
                Deck deck = new Deck();

                deck.Name = Path.GetFileNameWithoutExtension(file);

                string json = File.ReadAllText(file);
                var options = new JsonSerializerOptions { WriteIndented = true };
                List<Card>? cards = JsonSerializer.Deserialize<List<Card>>(json, options);
                if (cards == null) cards = new List<Card>();
                deck.Cards = cards;

                if (deck == null || string.IsNullOrEmpty(deck.Name))
                    continue;

                decks.Add(deck);
            }

            return decks;
        }
    }
}