#nullable enable
using Microsoft.Toolkit.Mvvm.ComponentModel;

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
    }
}
