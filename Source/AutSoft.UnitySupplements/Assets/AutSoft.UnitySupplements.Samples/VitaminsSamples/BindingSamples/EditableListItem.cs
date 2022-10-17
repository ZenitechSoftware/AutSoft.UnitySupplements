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

            Data.Bind
            (
                gameObject,
                _titleText.onValueChanged,
                x => x.Title,
                t => _titleText.text = t,
                i => i
            );

            Data.Bind
            (
                gameObject,
                _numberText.onValueChanged,
                x => x.Number,
                t => _numberText.text = t.ToString(),
                i => int.TryParse(i, out var result) ? result : null
            );
        }
    }
}
