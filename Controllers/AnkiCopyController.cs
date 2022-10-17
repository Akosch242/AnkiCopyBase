using AnkiCopyBase.Models;
using AnkiCopyBase.Views;

namespace AnkiCopyBase.Controllers
{
    public class AnkiCopyController
    {
        public void StartAnkiCopy()
        {
            if (!LoginProcess())
                return;

            while (true)
            {
                switch (Menu.ShowMain(UserManager.GetName()))
                {
                    case 1:
                        DeckCreationProcess();
                        break;
                    case 2:
                        TrainingProcess();
                        break;
                    case 3:
                        if (!LoginProcess())
                            return;
                        break;
                    default:
                        return;
                }
            }
        }

        private void TrainingProcess()
        {
            List<Deck> decks = UserManager.LoadDecks();
            int choosen = Learn.ShowDeckList(decks);

            if (choosen == 0)
                return;

            //decks indexing starts from 0, valid choosen values start from 1
            Learn.ShowDeck(decks[choosen - 1].Shuffle());
        }

        private bool LoginProcess()
        {
            int choosen = 0;
            do
            {
                choosen = LoginOrRegister.Entry();
                UserData user;

                if (choosen == 1)
                {
                    user = LoginOrRegister.Register(Valid.usernameDescription, Valid.passwordDescription);

                    while (!UserManager.TryRegister(user))
                    {
                        if (!LoginOrRegister.AskRetry())
                            break;

                        user = LoginOrRegister.Register(Valid.usernameDescription, Valid.passwordDescription);
                    }
                }
                else if (choosen == 2)
                {
                    user = LoginOrRegister.Login(Valid.usernameDescription, Valid.passwordDescription);

                    while (!UserManager.TryLogin(user))
                    {
                        if (!LoginOrRegister.AskRetry())
                            break;

                        user = LoginOrRegister.Login(Valid.usernameDescription, Valid.passwordDescription);
                    }
                }
                else
                {
                    return false;
                }

            } while (UserManager.GetName() == null);

            return true;
        }

        private void DeckCreationProcess()
        {
            if (!DeckCreation.Continue())
                return;

            Deck? deck = null;
            while (deck == null)
            {
                deck = Deck.TryCreateDeck(DeckCreation.DeckName(Valid.decknameDescription));
            }

            do
            {
                deck.Cards.Add(DeckCreation.CardCreation());
            } while (DeckCreation.ContinueDeckCreation());

            UserManager.SaveDeck(deck);
        }
    }
}