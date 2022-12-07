using UnityEngine.EventSystems;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearSelectionHighlighter : BaseSelectionHighlighter
    {
        private bool _clicked;

        public override bool IsHighlighted
        {
            get => _clicked;

            protected set => _clicked = value;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _clicked = false;
            base.OnPointerUp(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _clicked = true;
            base.OnPointerDown(eventData);
        }
    }
}
