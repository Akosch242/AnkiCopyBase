using AnkiCopyBase.Models;
using AnkiCopyBase.Views;

namespace AnkiCopyBase.Controllers
{
    public class AnkiCopy
    {
        public void Start()
        {
            if (!LoginProcess())
                return;

            while (true)
            {
                switch (Options.Show(UserManager.GetName()))
                {
                    case 1:
                        CreationProcess();
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
            do
            {
                int choosen = LoginOrRegister.Choose();
                UserData user;

                if (choosen == 1)
                {
                    user = LoginOrRegister.Register(Valid.usernameDescription, Valid.passwordDescription);

                    while (!UserManager.TryRegister(user))
                    {
                        if (!LoginOrRegister.Retry())
                            break;

                        user = LoginOrRegister.Register(Valid.usernameDescription, Valid.passwordDescription);
                    }
                }
                else if (choosen == 2)
                {
                    user = LoginOrRegister.Login(Valid.usernameDescription, Valid.passwordDescription);

                    while (!UserManager.TryLogin(user))
                    {
                        if (!LoginOrRegister.Retry())
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

        private void CreationProcess()
        {
            if (!Creat.Continue())
                return;

            Deck? deck = null;
            while (deck == null)
            {
                string? name = Creat.Deck(Valid.decknameDescription);
                deck = Deck.TryCreate(name);
            }

            do
            {
                Card created = Creat.Card();
                deck.AddCard(created);
            } while (Creat.ContinueAddingCards());

            UserManager.SaveDeck(deck);
        }
    }
}