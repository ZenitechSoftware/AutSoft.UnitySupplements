#nullable enable
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;

namespace AutSoft.UnitySupplements.Samples.VitaminSamples.BindingSamples
{
    public class InputData : ObservableObject
    {
        private string? _input;

        public string? Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        public void Alphabetize()
        {
            if (Input is null) return;
            Input = new string(Input.OrderBy(c => c).ToArray());
        }

        public void RemoveLastCharacter()
        {
            if (Input?.Length == 0) return;

            Input = Input?[..^1];
        }
    }
}
