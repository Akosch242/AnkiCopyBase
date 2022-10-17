namespace AnkiCopyBase.Models
{
    public static class Valid
    {
        //Update string when changing conditions
        public static readonly string usernameDescription =
            "(Not null or empty, length between 4 and 16, consists of letters or digits!)";
        public static bool Username(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            if (name.Length < 4 || name.Length > 16)
                return false;

            if (!name.All(char.IsLetterOrDigit))
                return false;

            return true;
        }

        //Update string when changing conditions
        public static readonly string passwordDescription =
            "(Not null or empty, longer than 4!)";
        public static bool Password(string? password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < 4)
                return false;

            return true;
        }

        public static bool User(UserData user)
        {
            if (!Valid.Username(user.Name))
                return false;

            if (!Valid.Password(user.Password))
                return false;

            return true;
        }

        //Update string when changing conditions
        public static readonly string decknameDescription =
            "(Not null or empty, length should be between 4 and 16, can only contain letters, digits and spaces!)";
        public static bool DeckName(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            if (name.Length < 4 || name.Length > 16)
                return false;

            if (!name.All(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x)))
                return false;

            return true;
        }
    }
}
