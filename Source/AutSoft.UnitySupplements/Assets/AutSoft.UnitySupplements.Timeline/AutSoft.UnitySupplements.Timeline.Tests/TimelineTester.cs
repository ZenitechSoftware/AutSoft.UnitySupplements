#nullable enable
using AutSoft.UnitySupplements.EventBus;
using AutSoft.UnitySupplements.Vitamins;
using Injecter;
using Microsoft.Extensions.Logging;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Timeline.AutSoft.UnitySupplements.Timeline.Tests
{
    public sealed class TimelineTester : MonoBehaviour
    {
        [Inject] private readonly IEventBus _eventBus = default!;
        [Inject] private readonly ILogger<TimelineTester> _logger = default!;

        [SerializeField] private TMP_Text _currentTimeText = default!;
        [SerializeField] private TMP_Text _isPlayingText = default!;

        [SerializeField] private Button _setTimesButton = default!;
        [SerializeField] private Button _updateTimesButton = default!;
        [SerializeField] private TMP_InputField _startInput = default!;
        [SerializeField] private TMP_InputField _endInput = default!;

        [SerializeField] private BasicTimelinePlayer _timelinePlayer = default!;

        private void Awake()
        {
            this.CheckSerializedField(_currentTimeText, nameof(_currentTimeText));
            this.CheckSerializedField(_isPlayingText, nameof(_isPlayingText));
            this.CheckSerializedField(_setTimesButton, nameof(_setTimesButton));
            this.CheckSerializedField(_startInput, nameof(_startInput));
            this.CheckSerializedField(_endInput, nameof(_endInput));
            this.CheckSerializedField(_timelinePlayer, nameof(_timelinePlayer));
            this.CheckSerializedField(_updateTimesButton, nameof(_updateTimesButton));
        }

        private void Start()
        {
            _eventBus.Subscribe<CurrentTimeChanged>(DisplayCurrentTime);
            _eventBus.Subscribe<TimelinePlayingChanged>(OnPlayingChanged);

            _setTimesButton.onClick.AddListener(OnSetTimesClicked);
            _updateTimesButton.onClick.AddListener(OnUpdateTimesClicked);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<CurrentTimeChanged>(DisplayCurrentTime);
            _eventBus.UnSubscribe<TimelinePlayingChanged>(OnPlayingChanged);

            _setTimesButton.onClick.RemoveListener(OnSetTimesClicked);
            _updateTimesButton.onClick.RemoveListener(OnUpdateTimesClicked);
        }

        private void DisplayCurrentTime(CurrentTimeChanged c)
        {
            _logger.LogInformation("Current time is {Time}", c.CurrentTime);
            _currentTimeText.text = c.CurrentTime.ToString();
        }

        private void OnPlayingChanged(TimelinePlayingChanged message) => _isPlayingText.text = message.IsPlaying.ToString();

        private void OnSetTimesClicked() => _timelinePlayer.Initialize(DateTimeOffset.Parse(_startInput.text), DateTimeOffset.Parse(_endInput.text));
        private void OnUpdateTimesClicked() => _timelinePlayer.ChangeCurrentTimeRange(DateTimeOffset.Parse(_startInput.text), DateTimeOffset.Parse(_endInput.text));
    }
}
