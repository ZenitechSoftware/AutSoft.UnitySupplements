#nullable enable
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Vitamins.Sample
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
            this.CheckSerializedField(x => x._inputField);
            this.CheckSerializedField(x => x._parsedValueText);
            this.CheckSerializedField(x => x._removeLastCharacterButton);
            this.CheckSerializedField(x => x._inputShowcase);
            this.CheckSerializedField(x => x._alphabetizeButton);

            _data.BindTwoWay
            (
                gameObject,
                _inputField.onValueChanged,
                x => x.Input,
                updateTarget: x => _inputField.text = x,
                updateSource: x => x
            );

            _data.BindOneWay
            (
                gameObject,
                x => x.Input,
                i => _inputShowcase.text = i
            );

            _data.BindOneWay
            (
                gameObject,
                x => x.Input,
                i => _parsedValueText.text = double.TryParse(i, out var result)
                    ? result.ToString()
                    : "Could not parse"
            );

            _removeLastCharacterButton.onClick.AddWeak(gameObject, _data.RemoveLastCharacter);

            _alphabetizeButton.onClick.AddWeak(gameObject, _data.Alphabetize);
        }
    }
}
