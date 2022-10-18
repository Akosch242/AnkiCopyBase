using AnkiCopyBase.Models;

namespace AnkiCopyBase.Views
{
    public static class Creat
    {
        public static bool Continue()
        {
            MenuBuilder menu = new MenuBuilder();

            menu.AddLine("Are you sure you want to create a new deck?");
            menu.AddOption("Yes");

            return menu.BuildMenu() == 1 ? true : false;
        }

        public static string? Deck(string decknameRules)
        {
            Console.Write($"Deckname{decknameRules}: ");
            return Console.ReadLine();
        }

        public static bool ContinueAddingCards()
        {
            MenuBuilder menu = new MenuBuilder();

            menu.AddLine("Do you want to continue adding cards?");
            menu.AddOption("Yes");

            return menu.BuildMenu() == 1 ? true : false;
        }

        public static Card Card()
        {
            string? input;
            bool frontNullOrEmpty = true;
            bool backNullOrEmpty = true;
            Card card = new Card();

            Console.Clear();
            Console.WriteLine("Fill out card infos(only hint is optional and duplicates are not allowed):");

            while (frontNullOrEmpty)
            {
                Console.Write("Front: ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    card.front = input;
                    frontNullOrEmpty = false;
                }
            }

            while (backNullOrEmpty)
            {
                Console.Write("Back: ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    card.back = input;
                    backNullOrEmpty = false;
                }
            }

            Console.Write("Hint: ");
            input = Console.ReadLine();
            card.hint = input;

            return card;
        }
    }
}