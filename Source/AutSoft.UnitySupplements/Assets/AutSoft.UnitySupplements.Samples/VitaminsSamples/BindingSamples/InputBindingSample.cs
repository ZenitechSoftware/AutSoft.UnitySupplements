#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Samples.VitaminSamples.BindingSamples
{
    public class InputBindingSample : MonoBehaviour
    {
        private readonly InputData _data = new();

        [SerializeField] private TMP_InputField _inputField = default!;
        [SerializeField] private TMP_Text _inputShowcase = default!;
        [SerializeField] private TMP_Text _parsedValueText = default!;
        [SerializeField] private Button _removeLastCharacterButton = default!;
        [SerializeField] private Button _alphabetizeButton = default!;

        private void Awake()
        {
            this.CheckSerializedFields();

            this.Bind
            (
                _data,
                x => x.Input,
                sourceToTarget: x => _inputField.text = x,
                _inputField.onValueChanged,
                targetToSource: x => x
            );

            this.Bind
            (
                _data,
                x => x.Input,
                i => _inputShowcase.text = i
            );

            this.Bind
            (
                _data,
                x => x.Input,
                i => _parsedValueText.text = double.TryParse(i, out var result)
                    ? result.ToString()
                    : "Could not parse"
            );

            this.Bind(_removeLastCharacterButton.onClick, _data.RemoveLastCharacter);

            this.Bind(_alphabetizeButton.onClick, _data.Alphabetize);
        }
    }
}
