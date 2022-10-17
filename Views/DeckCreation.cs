using AnkiCopyBase.Models;

namespace AnkiCopyBase.Views
{
    public static class DeckCreation
    {
        public static bool Continue()
        {
            ViewBuilder view = new ViewBuilder();
            view.AddLine("Are you sure you want to create a new deck?");
            view.AddOption("Yes");

            return view.BuildMenu() == 1 ? true : false;
        }

        public static string? DeckName(string decknameRules)
        {
            ViewBuilder view = new ViewBuilder();

            view.ClearFull();
            view.AddText($"Deckname{decknameRules}: ");
            view.BuildText();

            return view.GetText();
        }

        public static Card CardCreation()
        {
            ViewBuilder view = new ViewBuilder();
            string? temp = null;
            Card card = new Card();

            view.ClearFull();
            view.AddLine("Fill out card infos(only hint is optional and duplicates are not allowed):");

            while (true)
            {
                view.AddText("Front: ");
                view.BuildText();
                temp = view.GetText();
                if (!string.IsNullOrEmpty(temp))
                {
                    card.front = temp;
                    view.ClearText();
                    break;
                }
                view.ClearText();
            }
            while (true)
            {
                view.AddText("Back: ");
                view.BuildText();
                temp = view.GetText();
                if (!string.IsNullOrEmpty(temp))
                {
                    card.back = temp;
                    view.ClearText();
                    break;
                }
                view.ClearText();
            }
            view.AddText("Hint: ");
            view.BuildText();
            temp = view.GetText();
            card.hint = temp;
            view.ClearText();

            return card;
        }

        public static bool ContinueAddingCards()
        {
            ViewBuilder view = new ViewBuilder();
            view.AddLine("Do you want to continue adding cards?");
            view.AddOption("Yes");

            return view.BuildMenu() == 1 ? true : false;
        }
    }
}