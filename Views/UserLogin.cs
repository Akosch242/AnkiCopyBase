using AnkiCopyBase.Models;

namespace AnkiCopyBase.Views
{
    public static class UserLogin
    {
        public static int LoginOrRegister()
        {
            ViewBuilder view = new ViewBuilder();

            view.ClearFull();
            view.AddLine("Welcome to AnkiCopy, please register or login!");
            view.AddOption("Register");
            view.AddOption("Login");
            return view.BuildMenu();
        }

        public static UserData Login(string usernameRules, string passwordRules)
        {
            UserData user = new UserData();
            ViewBuilder view = new ViewBuilder();

            view.ClearFull();
            view.AddLine("Log into your account:");

            view.AddText($"Username{usernameRules}: ");
            view.BuildText();
            user.Name = view.GetText();
            view.ClearText();

            view.AddText($"Password{passwordRules}: ");
            view.BuildText();
            user.Password = view.GetText();
            view.ClearText();

            return user;
        }

        public static UserData Register(string usernameRules, string passwordRules)
        {
            UserData user = new UserData();
            ViewBuilder view = new ViewBuilder();

            view.ClearFull();
            view.AddLine("Create an account:");

            view.AddText($"Username{usernameRules}: ");
            view.BuildText();
            user.Name = view.GetText();
            view.ClearText();

            view.AddText($"Password{passwordRules}: ");
            view.BuildText();
            user.Password = view.GetText();
            view.ClearText();

            return user;
        }

        public static bool AskRetry()
        {
            ViewBuilder view = new ViewBuilder();

            view.AddLine("Unsuccessful, do you want to retry?");
            view.AddOption("Retry");

            return view.BuildMenu() == 1 ? true : false;
        }
    }
}