using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace AutSoft.UnitySupplements.MRTKExtras.UiComponents.DatePicker
{
    [RequireComponent(typeof(Clickable))]
    public class YearMonthBackgroundChangerXR : MonoBehaviour
    {
        [SerializeField] private Interactable _interactable = default!;
        [SerializeField] private Theme _selectedDateTheme = default!;
        [SerializeField] private GameObject _backgroundQuad = default!;
        private InteractableProfileItem _profileToAdd;
        private Clickable _clickable;

        private void Awake()
        {
            this.CheckSerializedFields();
            _profileToAdd = new InteractableProfileItem();
            _profileToAdd.Target = _backgroundQuad;
            _profileToAdd.Themes.Add(_selectedDateTheme);
            _clickable = GetComponent<Clickable>();
            this.Bind(_clickable.interactableChanged, Highlight);
        }

        private void Highlight(bool isOn)
        {
            _clickable.interactableChanged.RemoveListener(Highlight);
            _clickable.Interactable = true;
            _interactable.Profiles.Add(_profileToAdd);
            _interactable.RefreshSetup();
        }
    }
}
