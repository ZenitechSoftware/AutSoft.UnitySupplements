using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    /// <summary>
    /// Implements <see cref="Clickable"/> script on a <see cref="Button"/>.
    /// </summary>
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(Button))]
    public class RegularClickable : Clickable, IDeselectHandler, ISelectHandler
    {
        private Button _button = default!;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(ClickHappened);
        }

        private void OnDestroy() => _button.onClick.RemoveListener(ClickHappened);
        protected override void InteractableChanged(bool enabled) => _button.interactable = enabled;
        public override void SetColor(Color color) => GetComponent<Image>().color = color;
        public override void SetIcon(char unicodeChar)
        {
            var text = GetComponentInChildren<TMP_Text>();
            if (text == null)
                return;

            text.text = unicodeChar.ToString();
        }

        public void OnSelect(BaseEventData eventData) => SelectHappened();
        public void OnDeselect(BaseEventData eventData) => DeselectHappened();

        public override void TriggerClick() => _button.onClick.Invoke();
    }
}
