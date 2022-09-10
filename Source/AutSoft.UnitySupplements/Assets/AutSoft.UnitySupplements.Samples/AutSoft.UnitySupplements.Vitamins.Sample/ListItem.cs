#nullable enable
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins.Sample
{
    public class ListItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText = default!;
        [SerializeField] private TMP_Text _numberText = default!;

        private void Awake()
        {
            this.CheckSerializedField(x => x._titleText);
            this.CheckSerializedField(x => x._numberText);
        }

        public void Initialize(ListItemData data)
        {
            data.BindOneWay(gameObject, x => x.Title, t => _titleText.text = t);
            data.BindOneWay(gameObject, x => x.Number, n => _numberText.text = n.ToString());
        }
    }
}
