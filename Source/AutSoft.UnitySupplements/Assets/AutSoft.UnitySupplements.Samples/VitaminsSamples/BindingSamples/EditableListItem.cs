#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples.VitaminSamples.BindingSamples
{
    public class EditableListItem : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _titleText = default!;
        [SerializeField] private TMP_InputField _numberText = default!;

        public ListItemData Data { get; } = new() { Title = null, Number = null };

        private void Awake()
        {
            this.CheckSerializedFields();

            this.Bind
            (
                Data,
                x => x.Title,
                t => _titleText.text = t,
                _titleText.onValueChanged,
                i => i
            );

            this.Bind
            (
                Data,
                x => x.Number,
                t => _numberText.text = t.ToString(),
                _numberText.onValueChanged,
                i => int.TryParse(i, out var result) ? result : null
            );
        }
    }
}
