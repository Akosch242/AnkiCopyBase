using System.Text;

namespace AnkiCopyBase.Views
{
    public class MenuBuilder
    {
        private StringBuilder _viewText;
        private int _optionsCount;

        public MenuBuilder()
        {
            _viewText = new StringBuilder();
            _optionsCount = 0;
        }

        public void AddLine(string text) =>
            _viewText.AppendLine(text);

        public void AddOption(string text) =>
            _viewText.AppendLine($"{++_optionsCount}. {text}");

        public int BuildMenu()
        {
            string? input;
            bool isInputAnOption = false;
            int chosenOption;
            _viewText.AppendLine($"Press 0 to close/return.");

            do
            {
                Console.Clear();
                Console.Write(_viewText.ToString());
                input = Console.ReadLine();

                if (int.TryParse(input, out chosenOption))
                    if (chosenOption >= 0 && chosenOption <= _optionsCount)
                        isInputAnOption = true;

            } while (!isInputAnOption);

            _viewText.Clear();
            _optionsCount = 0;
            Console.Clear();

            return chosenOption;
        }
    }
}