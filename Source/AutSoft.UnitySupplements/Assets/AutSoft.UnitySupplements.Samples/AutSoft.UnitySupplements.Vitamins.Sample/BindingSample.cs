#nullable enable
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Vitamins.Sample
{
    public class BindingSample : MonoBehaviour
    {
        private readonly SampleData _data = new();

        [SerializeField] private TMP_InputField _inputField = default!;
        [SerializeField] private TMP_Text _inputShowcase = default!;
        [SerializeField] private Button _removeLastCharacterButton = default!;

        private void Awake()
        {
            this.CheckSerializedField(x => x._inputField);
            this.CheckSerializedField(x => x._removeLastCharacterButton);
            this.CheckSerializedField(x => x._inputShowcase);

            _data.BindTwoWay
            (
                gameObject,
                _inputField.onValueChanged,
                nameof(_data.Input),
                updateTarget: x => _inputField.text = x,
                updateSource: x => x
            );

            _data.BindOneWay<string>(gameObject, nameof(_data.Input), i => _inputShowcase.text = i);

            _removeLastCharacterButton.onClick.AddWeak(gameObject, () =>
            {
                if (_data.Input?.Length == 0) return;

                _data.Input = _data.Input?[..^1];
            });
        }
    }
}
