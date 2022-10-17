using AnkiCopyBase.Controllers;
using AnkiCopyBase.Models;
using System.Text.Json;

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