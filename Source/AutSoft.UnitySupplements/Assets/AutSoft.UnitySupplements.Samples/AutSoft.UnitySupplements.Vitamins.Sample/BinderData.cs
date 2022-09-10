#nullable enable
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;

namespace AutSoft.UnitySupplements.Vitamins.Sample
{
    public class SampleData : ObservableObject
    {
        private string? _input;

        public string? Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        public void Alphabetize() => Input = new string(Input.OrderBy(c => c).ToArray());

        public void RemoveLastCharacter()
        {
            if (Input?.Length == 0) return;

            Input = Input?[..^1];
        }
    }
}
