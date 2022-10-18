using AnkiCopyBase.Controllers;

namespace AnkiCopyBase
{
    public class Program
    {
        static void Main(string[] args)
        {
            AnkiCopy myApp = new AnkiCopy();

            myApp.Start();
        }
    }
}