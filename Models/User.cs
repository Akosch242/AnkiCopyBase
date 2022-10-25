using System.Net.Http.Headers;

namespace AnkiCopyBase.Models
{
    public class User
    {
        private string _name = "";
        private bool _loggedIn;

        public bool IsLoggedIn
        {
            get { return _loggedIn; }
        }

        public string Name
        {
            get { return _name; }
        }


        public bool TryRegister(UserData userData)
        {
            if (!Valid.Username(userData.Name) || !Valid.Password(userData.Password))
                return false;

            if (!Database.AddUser(userData))
                return false;

            _name = userData.Name;
            _loggedIn = true;

            return true;
        }

        public bool TryLogin(UserData userData)
        {
            if (!Valid.Username(userData.Name) || !Valid.Password(userData.Password))
                return false;

            if (!Database.FindUser(userData))
                return false;

            _name = userData.Name;
            _loggedIn = true;

            return true;
        }
    }
}