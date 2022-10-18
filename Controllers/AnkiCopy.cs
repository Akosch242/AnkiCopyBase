using AnkiCopyBase.Models;
using AnkiCopyBase.Views;

namespace AnkiCopyBase.Controllers
{
    public class AnkiCopy
    {
        public void Start()
        {
            User user = new User();
            bool closeApp = false;

            if (!LoginProcess(user))
                return;

            while (!closeApp)
            {
                switch (Options.Show(user.Name()))
                {
                    case 1:
                        CreationProcess(user);
                        break;
                    case 2:
                        TrainingProcess(user);
                        break;
                    case 3:
                        if (!LoginProcess(user))
                            closeApp = true;
                        break;
                    default:
                        closeApp = true;
                        break;
                }
            }
        }

        private void TrainingProcess(User user)
        {
            List<Deck> decks = Database.LoadDecks(user);
            int choosen = Learn.ShowDecks(decks);

            if (choosen == 0)
                return;

            //decks indexing starts from 0, valid choosen values start from 1
            Learn.ShowDeck(decks[choosen - 1].Shuffle());
        }

        private bool LoginProcess(User user)
        {
            do
            {
                int choosen = LoginOrRegister.Choose();
                UserData userData;

                if (choosen == 1)
                {
                    userData = LoginOrRegister.Register(Valid.usernameDescription, Valid.passwordDescription);

                    while (!user.TryRegister(userData))
                    {
                        if (!LoginOrRegister.WantsToRetry())
                            break;

                        userData = LoginOrRegister.Register(Valid.usernameDescription, Valid.passwordDescription);
                    }
                }
                else if (choosen == 2)
                {
                    userData = LoginOrRegister.Login(Valid.usernameDescription, Valid.passwordDescription);

                    while (!user.TryLogin(userData))
                    {
                        if (!LoginOrRegister.WantsToRetry())
                            break;

                        userData = LoginOrRegister.Login(Valid.usernameDescription, Valid.passwordDescription);
                    }
                }
                else
                {
                    return false;
                }

            } while (!user.LoggedIn());

            return true;
        }

        private void CreationProcess(User user)
        {
            if (!Create.Continue())
                return;

            Deck? deck = null;
            while (deck == null)
            {
                string? name = Create.Deck(Valid.decknameDescription);
                deck = Deck.TryCreate(name);
            }

            do
            {
                Card created = Create.Card();
                deck.AddCard(created);
            } while (Create.ContinueAddingCards());

            Database.SaveDeck(user, deck);
        }
    }
}