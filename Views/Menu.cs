namespace AnkiCopyBase.Views
{
    public static class Menu
    {
        public static int ShowMain(string? name)
        {
            ViewBuilder view = new ViewBuilder();

            view.AddLine($"Hi {name}! Choose an option:");
            view.AddOption("Create deck");
            view.AddOption("Check deck list");
            view.AddOption("Logout");

            return view.BuildMenu();
        }
    }
}
