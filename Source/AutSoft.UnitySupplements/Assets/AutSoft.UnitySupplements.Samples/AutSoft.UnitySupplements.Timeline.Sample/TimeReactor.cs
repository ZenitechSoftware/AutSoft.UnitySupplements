using AutSoft.UnitySupplements.EventBus;
using AutSoft.UnitySupplements.Timeline;
using AutSoft.UnitySupplements.Vitamins;
using Injecter;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples
{
    public class TimeReactor : MonoBehaviour
    {
        [Inject] private readonly IEventBus _eventBus = default!;

        [SerializeField] private TMP_Text _text = default!;

        private void Start()
        {
            this.CheckSerializedField(_text, nameof(_text));

            _eventBus.Subscribe<CurrentTimeChanged>(OnTimeChanged);
        }

        private void OnDestroy() => _eventBus.UnSubscribe<CurrentTimeChanged>(OnTimeChanged);

        private void OnTimeChanged(CurrentTimeChanged message) => _text.text = message.CurrentTime.ToString(CultureInfo.InvariantCulture);
    }
}
