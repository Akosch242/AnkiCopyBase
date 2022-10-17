using System.Text;

namespace AnkiCopyBase.Views
{
    public class ViewBuilder
    {
        private const int _closeOrReturn = 0;
        private StringBuilder _viewText;
        private int _optionsCount;

        public ViewBuilder()
        {
            _viewText = new StringBuilder();
            _optionsCount = 0;
        }

        public void ClearFull()
        {
            Console.Clear();
            _viewText.Clear();
            _optionsCount = 0;
        }

        public void ClearText() =>
            _viewText.Clear();

        public void BuildText() =>
            Console.Write(_viewText.ToString());

        public void AddText(string? text) => 
            _viewText.Append(text);

        public void AddLine(string? text) =>
            _viewText.AppendLine(text);

        public void AddOption(string text) =>
            _viewText.AppendLine($"{++_optionsCount}. {text}");

        public string? GetText() =>
            Console.ReadLine();

        public int BuildMenu()
        {
            string? input;
            bool isInputAnOption = false;
            int chosenOption = 0;
            _viewText.AppendLine($"Press {_closeOrReturn} to close/return.");

            do
            {
                Console.Clear();
                Console.Write(_viewText.ToString());
                input = Console.ReadLine();

                if(int.TryParse(input, out chosenOption))
                    if (chosenOption >= 0 && chosenOption <= _optionsCount)
                        isInputAnOption = true;

            } while (!isInputAnOption);

            return chosenOption;
        }
    }
}