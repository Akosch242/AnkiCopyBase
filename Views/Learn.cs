using AnkiCopyBase.Models;

namespace AnkiCopyBase.Views
{
    public static class Learn
    {
        public static int ShowDeckList(List<Deck> decks)
        {
            if (decks == null)
                return 0;

            MenuBuilder menu = new MenuBuilder();

            menu.AddLine("Your decks:");
            foreach (Deck deck in decks)
            {
                menu.AddOption($"{deck.Name}");
            }
            
            return menu.BuildMenu();
        }

        public static void ShowDeck(Deck deck)
        {
            MenuBuilder menu = new MenuBuilder();

            foreach (Card card in deck)
            {
                int choosen;
                bool backHidden = true;
                bool hintHidden = true;
                do
                {
                    menu.AddLine($"Currently training {deck.Name} deck:");
                    menu.AddLine(card.front);
                    menu.AddOption("Next card");

                    if (backHidden)
                        menu.AddOption("Show back");
                    else
                        menu.AddLine($"Back: {card.back}");

                    if (hintHidden && backHidden)
                        menu.AddOption("Show hint");
                    else if (hintHidden && !backHidden)
                        menu.AddLine("");
                    else
                        menu.AddLine($"Hint: {card.hint}");

                    choosen = menu.BuildMenu();

                    if (choosen == 1)
                        break;
                    else if (choosen == 2)
                        backHidden = false;
                    else if (choosen == 3)
                        hintHidden = false;
                    else
                        return;

                } while (choosen != 1);
            }
        }
    }
}