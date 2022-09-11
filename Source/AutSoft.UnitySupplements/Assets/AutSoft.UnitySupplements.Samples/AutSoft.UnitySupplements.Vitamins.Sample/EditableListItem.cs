#nullable enable
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins.Sample
{
    public class EditableListItem : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _titleText = default!;
        [SerializeField] private TMP_InputField _numberText = default!;

        public ListItemData Data { get; } = new();

        private void Awake()
        {
            this.CheckSerializedField(x => x._titleText);
            this.CheckSerializedField(x => x._numberText);

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
