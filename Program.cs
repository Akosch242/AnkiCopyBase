using AnkiCopyBase.Controllers;

namespace AnkiCopyBase
{
    public class Program
    {
        static void Main(string[] args)
        {
            AnkiCopyController myApp = new AnkiCopyController();

            myApp.StartAnkiCopy();
        }
    }
}