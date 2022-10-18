using AnkiCopyBase.Models;

namespace AnkiCopyBase.Views
{
    public static class LoginOrRegister
    {
        public static int Choose()
        {
            MenuBuilder menu = new MenuBuilder();

            menu.AddLine("Welcome to AnkiCopy, please register or login!");
            menu.AddOption("Register");
            menu.AddOption("Login");

            return menu.BuildMenu();
        }

        public static UserData Login(string usernameRules, string passwordRules)
        {
            UserData user = new UserData();

            Console.WriteLine("Log into your account:");

            Console.Write($"Username{usernameRules}: ");
            user.Name = Console.ReadLine();

            Console.Write($"Password{passwordRules}: ");
            user.Password = Console.ReadLine();

            return user;
        }

        public static UserData Register(string usernameRules, string passwordRules)
        {
            UserData user = new UserData();

            Console.WriteLine("Create an account:");

            Console.Write($"Username{usernameRules}: ");
            user.Name = Console.ReadLine();

            Console.Write($"Password{passwordRules}: ");
            user.Password = Console.ReadLine();

            return user;
        }

        public static bool Retry()
        {
            MenuBuilder menu = new MenuBuilder();

            menu.AddLine("Unsuccessful, do you want to retry?");
            menu.AddOption("Retry");

            return menu.BuildMenu() == 1 ? true : false;
        }
    }
}