using AnkiCopyBase.Models;

namespace AnkiCopyBase.Views
{
    public class Learn
    {
        public static int ShowDeckList(List<Deck> decks)
        {
            if (decks == null)
                return 0;

            ViewBuilder view = new ViewBuilder();

            view.AddLine("Your decks:");
            foreach (Deck deck in decks)
            {
                view.AddOption($"{deck.Name}");
            }
            return view.BuildMenu();
        }

        public static void ShowDeck(Deck deck)
        {
            ViewBuilder view = new ViewBuilder();

            foreach (Card card in deck)
            {
                int choosen = 0;
                bool isBackHidden = true;
                bool isHintHidden = true;
                do
                {
                    view.ClearFull();
                    view.AddLine($"Currently training {deck.Name} deck:");
                    view.AddLine(card.front);
                    view.AddOption("Next card");

                    if (isBackHidden)
                        view.AddOption("Show back");
                    else
                        view.AddLine($"Back: {card.back}");

                    if (isHintHidden && isBackHidden)
                        view.AddOption("Show hint");
                    else if (isHintHidden && !isBackHidden)
                        view.AddLine("");
                    else
                        view.AddLine($"Hint: {card.hint}");

                    choosen = view.BuildMenu();

                    if (choosen == 1)
                        break;
                    else if (choosen == 2)
                        isBackHidden = false;
                    else if (choosen == 3)
                        isHintHidden = false;
                    else
                        return;

                } while (choosen != 1);
            }
        }
    }
}
