namespace AnkiCopyBase.Views
{
    public static class Options
    {
        public static int Show(string? name)
        {
            MenuBuilder menu = new MenuBuilder();

            menu.AddLine($"Hi {name}! Choose an option:");
            menu.AddOption("Create deck");
            menu.AddOption("Check deck list");
            menu.AddOption("Logout");

            return menu.BuildMenu();
        }
    }
}