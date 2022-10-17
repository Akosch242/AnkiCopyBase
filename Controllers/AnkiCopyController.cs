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

            Learn.ShowDeck(decks[choosen - 1].Shuffle());
        }

        private bool LoginProcess()
        {
            int choosen = 0;
            do
            {
                choosen = UserLogin.LoginOrRegister();
                UserData user;

                if (choosen == 1)
                {
                    user = UserLogin.Register(Valid.usernameDescription, Valid.passwordDescription);

                    while (!UserManager.TryRegister(user))
                    {
                        if (!UserLogin.AskRetry())
                            break;

                        user = UserLogin.Register(Valid.usernameDescription, Valid.passwordDescription);
                    }
                }
                else if (choosen == 2)
                {
                    user = UserLogin.Login(Valid.usernameDescription, Valid.passwordDescription);

                    while (!UserManager.TryLogin(user))
                    {
                        if (!UserLogin.AskRetry())
                            break;

                        user = UserLogin.Login(Valid.usernameDescription, Valid.passwordDescription);
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

            Deck deck = new Deck(DeckCreation.DeckName());

            do
            {
                deck.AddCard(DeckCreation.CardCreation());
            } while (DeckCreation.ContinueDeckCreation());

            while (!UserManager.SaveDeck(deck))
                deck.Name = DeckCreation.DeckName();
        }
    }
}